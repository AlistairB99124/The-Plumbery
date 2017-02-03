using Plumbery.Infrastructure.Data.Initialiser;

namespace Plumbery.Infrastructure.Data.Migrations {
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.ContextBank>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Context.ContextBank context)
        {
            //var users = DatabaseInitialiser.GetUsers();
            //foreach (var user in users) {
            //    var passwordHash = new PasswordHasher();
            //    string password = passwordHash.HashPassword("000000");
            //    user.PasswordHash = password;
            //    user.SecurityStamp = Guid.NewGuid().ToString();
            //    context.Users.AddOrUpdate(u => u.UserName, user);
            //}

            //DatabaseInitialiser.GetWarehouses().ForEach(x => context.Warehouses.Add(x));
            //DatabaseInitialiser.GetMaterials().ForEach(x => context.Materials.Add(x));
            //DatabaseInitialiser.GetInventory().ForEach(x => context.Inventories.Add(x));
            //DatabaseInitialiser.GetSupervisors().ForEach(x => context.Supervisors.Add(x));
            //DatabaseInitialiser.GetPlumbers().ForEach(x => context.Plumbers.Add(x));
            //DatabaseInitialiser.GetLocations().ForEach(x => context.Locations.Add(x));
            //DatabaseInitialiser.GetSites().ForEach(x => context.Sites.Add(x));

            //Delete all stored procs, views
            //foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", ""), "Sql\\Seed"), "*.sql")) {
            //    context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
            //}

            //Add Stored Procedures
            //foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", ""), "Sql\\StoredProcs"), "*.sql")) {
            //    context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
            //}
        }
    }
}
