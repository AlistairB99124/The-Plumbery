using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plumbery.Domain.Interfaces.Repositories;
using Plumbery.Domain.Entities;
using System.Web;
using System.Data.OleDb;
using System.Globalization;
using System.Data;
using System.Configuration;
using System.IO;

namespace Plumbery.Infrastructure.Data.Repositories {
    public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository {
        public void AddMaterial(Material material) => _context.Materials.Add(material);

        public IEnumerable<Inventory> GetDepletedStock() => _context.Inventories.Where(x => x.Quantity == 0 || x.Quantity < 0);

        public Inventory GetInventoryByCode(string code) => _context.Inventories.Where(x => x.Material.StockCode == code).FirstOrDefault();

        public IEnumerable<Inventory> GetInventoryByWarehouse(int warehouseId) => _context.Inventories.Where(x => x.WarehouseId == warehouseId);

        public IEnumerable<Inventory> GetLowInventory() => _context.Inventories.Where(x => x.Quantity <= 5 && x.Quantity > 0);

        public Plumber GetPlumber(int Id) => _context.Plumbers.Find(Id);

        public Plumber GetPlumber(string UserId) => _context.Plumbers.Where(x => x.UserId == UserId).FirstOrDefault();

        public IEnumerable<User> GetPlumberUsers() {
            var users = new List<User>();
            var plumbers = _context.Plumbers.ToList();
            foreach (var p in plumbers) {
                var user = _context.Users.Find(p.UserId);
                users.Add(user);
            }
            return users;
        }

        public Supervisor GetSupervisor(string UserId) => _context.Supervisors.Where(x => x.UserId == UserId).FirstOrDefault();

        public User GetUser(string Id) => _context.Users.Find(Id);

        public int[] ImportToDatabase(Plumber p, User user, string filePath, string extension, string firstRowHeader) {
            int[] counts = new int[3];
            int countAdded = 0;
            int countModified = 0;
            string conStr = "";
            #region Excel Access Code
            switch (extension) {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, filePath, firstRowHeader);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            #endregion
            List<Inventory> TodaysInventory = new List<Inventory>();

            bool finished = false;
            do {
                foreach (DataRow row in dt.Rows) {
                    string code = row["Stock Code"].ToString();
                    if (row != dt.Rows[dt.Rows.Count - 1]) {
                        if (code != "") {
                            var t = ReadRow(row, p, user);
                            countAdded += t.Item1;
                            countModified += t.Item2;
                            TodaysInventory.AddRange(t.Item3);
                        }
                    } else {
                        if (code != "") {
                            var t = ReadRow(row, p, user);
                            countAdded += t.Item1;
                            countModified += t.Item2;
                            TodaysInventory.AddRange(t.Item3);
                        }
                        finished = true;
                    }
                }

            } while (finished == false);

            counts[0] = countAdded;
            counts[1] = countModified;
            var inventorytoRemove = _context.Inventories.Where(x => x.WarehouseId == p.WarehouseId).ToList().Except(TodaysInventory);
            int inventoryRemoved = inventorytoRemove.Count();
            _context.Inventories.RemoveRange(inventorytoRemove);
            counts[2] = inventoryRemoved;
            return counts;
        }
        public decimal? CustomParse(string incomingValue) {
            decimal val;
            if (!decimal.TryParse(incomingValue.Replace(",", "").Replace(".", ""), NumberStyles.Number, CultureInfo.InvariantCulture, out val))
                return null;
            return val / 100;
        }


        private Tuple<int, int, List<Inventory>> ReadRow(DataRow row, Plumber p, User user) {
            List<Inventory> inSheet = new List<Inventory>();
            // array to store CRUD counts
            int[] counted = new int[2] { 0, 0 };
            // To items added to the database
            int Added = 0;
            // To Store items updated in the database
            int Updated = 0;
            // Get stock code of material
            string code = row["Stock Code"].ToString();
            // Get description of material
            string description = row["Stock Description"].ToString();
            // Get quantity of materials in warehouse
            string quantity = row["Level"].ToString().Replace(',', '.');
            // Get cost of that material
            string cost = row["Unit Cost Price"].ToString().Replace(',', '.');
            // Try parse quantity to decimal
            decimal newLevel = 0;
            if (!decimal.TryParse(quantity, out newLevel)) {
                decimal.TryParse(quantity.Replace('.', ','), out newLevel);
            }
            // try parse cost to decimal
            decimal newCost = 0;
            if (!decimal.TryParse(cost, out newCost)) {
                decimal.TryParse(cost.Replace('.', ','), out newCost);
            }
            // Check if material already exists in a warehouse
            Material existingMaterial = _context.Materials.Where(x => x.StockCode == code).FirstOrDefault();
            // If it does not, we will have to add both a material and an inventory in the warehouse
            if (existingMaterial == null) {
                // Create the new material
                Material material = new Material {
                    StockCode = code,
                    Cost = newCost,
                    StockDescription = description
                };
                // Add to the database
                AddMaterial(material);

                Inventory inventory = new Inventory {
                    DateAdded = DateTime.Now,
                    DateModified = DateTime.Now,
                    Material = material,
                    ModifiedBy = user.FullName,
                    Quantity = newLevel,
                    WarehouseId = p.WarehouseId
                };
                // Add inventory to warehouse
                this.Add(inventory);
                // Add this inventory to in sheet
                inSheet.Add(inventory);
                // Add to inventory add
                ++Added;
            } else {
                // If the material already exists, make sure the plumber does not already have it
                Inventory existingInventory = _context.Inventories.Where(x => x.WarehouseId == p.WarehouseId && x.MaterialId == existingMaterial.Id).FirstOrDefault();
                // if Exisiting Inventory already exists, we know this is only a question of updating the data in the plumbers warehouse
                if (existingInventory != null) {
                    if (existingInventory.Quantity != newLevel) {
                        existingInventory.Quantity = newLevel;
                        existingInventory.DateModified = DateTime.Now;
                        existingInventory.ModifiedBy = user.FullName;
                        Edit(existingInventory);
                        ++Updated;
                    }
                    inSheet.Add(existingInventory);
                } else {
                    // If existing inventory does not exist, and we know that the material does exist. Therefore, we add just an inventory but not a new material
                    Inventory newInventory = new Inventory {
                        DateAdded = DateTime.Now,
                        DateModified = DateTime.Now,
                        Material = existingMaterial,
                        ModifiedBy = user.FullName,
                        Quantity = newLevel,
                        WarehouseId = p.WarehouseId
                    };
                    // Add the new inventory to the plumbers warehouse
                    _context.Inventories.Add(newInventory);
                    // And add to the in sheet list
                    inSheet.Add(newInventory);
                    // Add to inventory add list
                    ++Added;
                }
            }
            counted[0] = Added;
            counted[1] = Updated;
            return Tuple.Create(counted[0], counted[1], inSheet);
        }
    }
}
