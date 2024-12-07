using Cinematic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinematic.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Favorite>(f => f.HasKey(p => new { p.MovieId, p.AppUserId }));

            builder.Entity<Favorite>()
            .HasOne(u => u.Movie)
            .WithMany(u => u.Favorites)
            .HasForeignKey(u => u.MovieId);

            builder.Entity<Favorite>()
            .HasOne(u => u.AppUser)
            .WithMany(u => u.Favorites)
            .HasForeignKey(u => u.AppUserId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "33bc4ea6-3ddd-4987-aaa8-828f95b70bbf",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "e8548f30-f1e6-48f7-99fb-9b753a2ad020",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
