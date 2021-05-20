using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Felixo.Library.Entities.Models.Context
{
    public class AppDataContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        IConfigurationRoot root;


        public DbSet<Book> Books { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Category> Category { get; set; }

        public AppDataContext()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            root = configurationBuilder.Build();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(root.GetSection("ConnectionStrings").GetSection("DbConnection").Value);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUserRole>().HasKey(m => new { m.UserId, m.RoleId });
            builder.Entity<ApplicationUserRole>()
                .HasOne(m => m.User)
                .WithMany(m => m.UserRoles).HasForeignKey(m => m.UserId);
            builder.Entity<ApplicationUserRole>()
                .HasOne(m => m.Role)
                .WithMany(m => m.UserRoles).HasForeignKey(m => m.RoleId);

            //builder.Entity<Book>()
            //    .HasOne(p => p.Category)
            //    .WithMany(p => p.Books);

            //builder.Entity<Request>()
            //    .HasOne(e => e.ApplicationUser)
            //    .WithOne(u => u.Request);
            //builder.Entity<Request>().HasOne(m => m.Book)
            //         .WithOne()
            //         .HasForeignKey<Book>(m => m.RequestId);
        }
    }
}
