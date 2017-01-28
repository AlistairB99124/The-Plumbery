using Plumbery.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using static Plumbery.Infrastructure.Data.Configuration.EFConfiguration;

namespace Plumbery.Infrastructure.Data.Context {
    /// <summary>
    /// Primary Identity Context
    /// </summary>
    public class ContextBank:IdentityDbContext<User> {
        /// <summary>
        /// Constructor pointing to database configuration
        /// </summary>
        public ContextBank()
           : base("Plumbery") {
        }
        /// <summary>
        /// Create new instance of Context
        /// </summary>
        /// <returns></returns>
        public static ContextBank Create() {
            return new ContextBank();
        }

        // Add Tables to model
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Plumber> Plumbers { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        //public DbSet<Quote> Quotes { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<TimeSheetMaterialItem> TimeSheetMaterialItems { get; set; }
        public DbSet<TimeSheetCommentItem> TimeSheetCommentItems { get; set; }
        public DbSet<Site> Sites { get; set; }

        /// <summary>
        /// Dispose context
        /// </summary>
        /// <param name="disposing">Boolean disposing</param>
        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
        }

        
        /// <summary>
        /// Create the model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "Id").Configure(p => p.IsKey());
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

            modelBuilder.Properties<string>().Where(p => p.Name.Contains("Description")).Configure(p => p.HasMaxLength(400));

            // ToDo: Add mapping for entities
            modelBuilder.Configurations.Add(new InventoryMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new MaterialMap());
            modelBuilder.Configurations.Add(new PlumberMap());
            modelBuilder.Configurations.Add(new SiteMap());
            modelBuilder.Configurations.Add(new SupervisorMap());
            modelBuilder.Configurations.Add(new TimeSheetMap());
            modelBuilder.Configurations.Add(new TimeSheetMaterialItemMap());
            modelBuilder.Configurations.Add(new TimeSheetCommentItemMap());
            modelBuilder.Configurations.Add(new WarehouseMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
