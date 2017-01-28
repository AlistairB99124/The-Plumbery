﻿using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Infrastructure.Data.Repositories {
    public class TimeSheetRepository : BaseRepository<TimeSheet>, ITimeSheetRepository {
        public void AddCommentItem(TimeSheetCommentItem comment) {
            _context.TimeSheetCommentItems.Add(comment);
        }

        public void AddMaterialItem(TimeSheetMaterialItem material) => _context.TimeSheetMaterialItems.Add(material);

        public void DeductFromInventory(List<TimeSheetMaterialItem> items) {
            foreach(var item in items) {
                Inventory inventory = _context.Inventories.Where(x => x.MaterialId == item.MaterialId && x.WarehouseId == item.TimeSheet.Plumber.WarehouseId).FirstOrDefault();
                inventory.Quantity -= item.Quantity;
            }
        }

        public void DeleteCommentItem(int Id) {
            var item = _context.TimeSheetCommentItems.Find(Id);
            _context.TimeSheetCommentItems.Remove(item);
        }

        public void DeleteCommentItems(List<TimeSheetCommentItem> items) => _context.TimeSheetCommentItems.RemoveRange(items);

        public void DeleteMaterialItem(int Id) {
            var item = _context.TimeSheetMaterialItems.Find(Id);
            _context.TimeSheetMaterialItems.Remove(item);
        }

        public void DeleteMaterialItems(List<TimeSheetMaterialItem> items) => _context.TimeSheetMaterialItems.RemoveRange(items);

        public void EditCommentItem(TimeSheetCommentItem comment) => _context.Entry(comment).State = System.Data.Entity.EntityState.Modified;

        public void EditMaterialItem(TimeSheetMaterialItem material) => _context.Entry(material).State = System.Data.Entity.EntityState.Modified;

        public Plumber GetPlumber(int Id) => _context.Plumbers.Find(Id);

        public Plumber GetPlumber(string UserId) => _context.Plumbers.Where(x => x.UserId == UserId).FirstOrDefault();

        public IEnumerable<User> GetPlumberUsers() {
            var users = new List<User>();
            var plumbers = _context.Plumbers.ToList();
            foreach(var p in plumbers) {
                var user = _context.Users.Find(p.UserId);
                users.Add(user);
            }
            return users;
        }

        public Site GetSite(int Id) => _context.Sites.Find(Id);

        public TimeSheet GetTimeSheet(string Code) => _context.TimeSheets.Where(x => x.Code == Code).FirstOrDefault();

        public IEnumerable<TimeSheetCommentItem> ListCommentItems(int TimeSheetId) => _context.TimeSheetCommentItems.Where(x => x.TimeSheetId == TimeSheetId);

        public IEnumerable<TimeSheetMaterialItem> ListMaterialItems(int TimeSheetId) => _context.TimeSheetMaterialItems.Where(x => x.TimeSheetId == TimeSheetId);

        public IEnumerable<Material> ListMaterials(Plumber plumber) {
            var warehouse = _context.Warehouses.Where(x => x.Id == plumber.WarehouseId).FirstOrDefault();
            List<Inventory> inventory = _context.Inventories.Where(x => x.WarehouseId == warehouse.Id).ToList();
            List<Material> materials = new List<Material>();
            foreach(Inventory inv in inventory) {
                var material = _context.Materials.Where(x => x.Id == inv.MaterialId).FirstOrDefault();
                materials.Add(material);
            }
            return materials;
        }

        public IEnumerable<Plumber> ListPlumbers() => _context.Plumbers;
        public IEnumerable<Site> ListSites() => _context.Sites;
    }
}
