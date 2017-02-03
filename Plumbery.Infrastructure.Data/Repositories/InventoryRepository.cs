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
            int[] counts = new int[2];
            int countAdded = 0;
            int countModified = 0;
            string conStr = "";
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
            //Bind Data to GridView
            bool finished = false;
            do {
                foreach (DataRow row in dt.Rows) {
                    string code = row["Stock Code"].ToString();
                    if (code != "") {
                        string description = row["Stock Description"].ToString();
                        string quantity = row["Level"].ToString();
                        string cost = row["Unit Cost Price"].ToString();
                        decimal newLevel = 0;
                        newLevel = Convert.ToDecimal(quantity, CultureInfo.InvariantCulture);

                        Inventory inv = GetInventoryByCode(code);
                        if (inv == null) {
                            Material m = new Material {
                                StockCode = code,
                                Cost = Convert.ToDecimal(cost, CultureInfo.InvariantCulture),
                                StockDescription = description
                            };
                            AddMaterial(m);
                            Inventory inventory = new Inventory {
                                DateAdded = DateTime.Now,
                                DateModified = DateTime.Now,
                                Material = m,
                                ModifiedBy = user.FullName,
                                Quantity = newLevel,
                                WarehouseId = p.WarehouseId
                            };
                            this.Add(inventory);
                            ++countAdded;
                        }
                        if(inv != null) {
                            if (inv.Quantity != newLevel) {
                                inv.Quantity = Convert.ToDecimal(quantity, CultureInfo.InvariantCulture);
                                inv.DateModified = DateTime.Now;
                                inv.ModifiedBy = user.FullName;
                                Edit(inv);
                                ++countModified;
                            }
                        }                        
                    } else {
                        finished = true;
                    }

                }
            } while (finished == false);

            counts[0] = countAdded;
            counts[1] = countModified;

            return counts;
        }
    }
}
