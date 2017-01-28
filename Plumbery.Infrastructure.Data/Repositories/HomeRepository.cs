using Plumbery.Domain.Interfaces.Domain;
using Plumbery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plumbery.Domain.Entities;

namespace Plumbery.Infrastructure.Data.Repositories {
    public class HomeRepository : BaseRepository<Home>, IHomeRepository {
        public IEnumerable<Inventory> SearchInventory(List<string> keywords) {
            List<Inventory> results = new List<Inventory>();
            foreach(var key in keywords) {
                foreach(var inv in _context.Inventories) {
                    foreach(var p in inv.GetType().GetProperties()) {
                        if (p.ToString() == key) {
                            results.Add(inv);
                        }
                    }
                }
            }
            results = results.Distinct().ToList();
            return results;
        }

        public IEnumerable<Plumber> SearchPlumbers(List<string> keywords) {
            List<Plumber> results = new List<Plumber>();
            foreach (var key in keywords) {
                foreach (var inv in _context.Plumbers) {
                    foreach (var p in inv.GetType().GetProperties()) {
                        if (p.ToString() == key) {
                            results.Add(inv);
                        }
                    }
                }
            }
            results = results.Distinct().ToList();
            return results;
        }

        public IEnumerable<Site> SearchSites(List<string> keywords) {
            List<Site> results = new List<Site>();
            foreach (var key in keywords) {
                foreach (var inv in _context.Sites) {
                    foreach (var p in inv.GetType().GetProperties()) {
                        if (p.ToString() == key) {
                            results.Add(inv);
                        }
                    }
                }
            }
            results = results.Distinct().ToList();
            return results;
        }

        public IEnumerable<TimeSheet> SearchTimeSheets(List<string> keywords) {
            List<TimeSheet> results = new List<TimeSheet>();
            foreach (var key in keywords) {
                foreach (var inv in _context.TimeSheets) {
                    foreach (var p in inv.GetType().GetProperties()) {
                        if (p.ToString() == key) {
                            results.Add(inv);
                        }
                    }
                }
            }
            results = results.Distinct().ToList();
            return results;
        }

        public IEnumerable<Warehouse> SearchWarehouse(List<string> keywords) {
            List<Warehouse> results = new List<Warehouse>();
            foreach (var key in keywords) {
                foreach (var inv in _context.Warehouses) {
                    foreach (var p in inv.GetType().GetProperties()) {
                        if (p.ToString() == key) {
                            results.Add(inv);
                        }
                    }
                }
            }
            results = results.Distinct().ToList();
            return results;
        }

        public IEnumerable<Supervisor> SearchSupervisors(List<string> keywords) {
            List<Supervisor> results = new List<Supervisor>();
            foreach (var key in keywords) {
                foreach (var inv in _context.Supervisors) {
                    foreach (var p in inv.GetType().GetProperties()) {
                        if (p.ToString() == key) {
                            results.Add(inv);
                        }
                    }
                }
            }
            results = results.Distinct().ToList();
            return results;
        }
    }
}
