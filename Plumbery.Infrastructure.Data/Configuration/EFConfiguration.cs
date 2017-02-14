using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Plumbery.Domain.Entities;

namespace Plumbery.Infrastructure.Data.Configuration {
    /// <summary>
    /// Class to map entities in database
    /// </summary>
    public class EFConfiguration {
        //To Do: Add mappings
        public class InventoryMap: EntityTypeConfiguration<Inventory> {
            public InventoryMap() {
                this.HasKey(t => t.Id);
                this.HasRequired(t => t.Material);
                this.ToTable("Inventory", "dbo");
                this.HasRequired(t => t.Warehouse);
            }
        }
        public class LocationMap : EntityTypeConfiguration<Location> {
            public LocationMap() {
                this.HasKey(t => t.Id);
                this.Property(t => t.Id).IsRequired();
                this.Property(t => t.Address1).IsRequired();
                this.Property(t => t.Address2).IsOptional();
                this.Property(t => t.Province).IsOptional();
                this.Property(t => t.Postal_Code).IsOptional();
                this.Property(t => t.City).IsRequired();
                this.Property(t => t.Country).IsRequired();
                this.Property(t => t.Postal_Code).IsOptional();
                this.ToTable("Location", "dbo");
            }
        }
        public class MaterialMap : EntityTypeConfiguration<Material> {
            public MaterialMap() {
                this.HasKey(t => t.Id);
                this.Property(t => t.Cost).IsRequired();
                this.Property(t => t.StockCode).IsRequired();
                this.Property(t => t.StockDescription).IsRequired();
                this.ToTable("Material", "dbo");
            }
        }
        public class PlumberMap : EntityTypeConfiguration<Plumber> {
            public PlumberMap() {
                this.HasKey(t => t.Id);
                this.HasRequired(t => t.Warehouse);
                this.HasRequired(t => t.User);
                this.ToTable("Plumber", "dbo");
            }
        }
        //public class QuoteMap : EntityTypeConfiguration<Quote> {
        //    public QuoteMap() {

        //    }
        //}
        public class SiteMap : EntityTypeConfiguration<Site> {
            public SiteMap() {
                this.HasKey(t => t.Id);
                this.Property(t => t.Abbr).IsRequired();
                this.Property(t => t.Name).IsRequired();
                this.HasRequired(t => t.Location);
                this.ToTable("Site", "dbo");
            }
        }
        public class SupervisorMap : EntityTypeConfiguration<Supervisor> {
            public SupervisorMap() {
                this.HasKey(t => t.Id);
                this.HasRequired(t => t.User);
                this.ToTable("Supervisor", "dbo");
            }
        }
        public class TimeSheetMap : EntityTypeConfiguration<TimeSheet> {
            public TimeSheetMap() {
                this.HasKey(t => t.Id);
                this.Property(t => t.AssistantTime).IsRequired();
                this.Property(t => t.Code).IsRequired();
                this.Property(t => t.DateCreated).IsRequired();
                this.Property(t => t.Description).IsOptional();
                this.Property(t => t.DetailedPoint).IsOptional();
                this.Property(t => t.OriginalQuote).IsRequired();
                this.Property(t => t.PlumberTime).IsRequired();
                this.Property(t => t.QuoteNo).IsOptional();
                this.Property(t => t.SheetStatus).IsRequired();
                this.Property(t => t.SINumber).IsOptional();
                this.Property(t => t.SpecificLocation).IsOptional();
                
                this.HasRequired(t => t.Site);
                this.HasRequired(t => t.Plumber);
                this.ToTable("TimeSheet", "dbo");
            }
        }
        public class TimeSheetCommentItemMap : EntityTypeConfiguration<TimeSheetCommentItem> {
            public TimeSheetCommentItemMap() {
                this.HasKey(t => t.Id);
                this.Property(t => t.Description).IsRequired();
                this.Property(t => t.Metric).IsOptional();
                this.HasRequired(t => t.TimeSheet);
                
                this.ToTable("CommentItem", "dbo");
            }
        }
        public class TimeSheetMaterialItemMap : EntityTypeConfiguration<TimeSheetMaterialItem> {
            public TimeSheetMaterialItemMap() {
                this.HasKey(t => t.Id);
                this.Property(t => t.BOM_No).IsOptional();
                this.Property(t => t.Quantity).IsRequired();
                this.Property(t => t.Supplier).IsOptional();

                this.HasRequired(t => t.Material);
                this.ToTable("MaterialItem", "dbo");
            }
        }
        public class WarehouseMap : EntityTypeConfiguration<Warehouse> {
            public WarehouseMap() {
                this.HasKey(t => t.Id);
                this.Property(t => t.Name).IsOptional();
                this.ToTable("Warehouse", "dbo");
            }
        }
    }
}
