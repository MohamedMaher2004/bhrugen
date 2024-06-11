using System.Data;
using bhrugen_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace bhrugen_webapi.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
    {
        
    }
    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumbers { get; set; }
    public DbSet<LocalUser> LocalUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //using when we want to update the name of the table in database to new name
        base.OnModelCreating((modelBuilder));
        modelBuilder.Entity<Villa>().ToTable("villatest");
        //inserted at database first time when using update-database
        modelBuilder.Entity<Villa>().HasData(
        
            new Villa
            {
                Id = 1,
                Name = "royal villa",
                Details = "on the sea",
                Occupancy = 3,
                Rate = 200,
                Sqft = 500,
                Amenity = "",
                ImageUrl = "",
                CreatedDate = DateTime.Now
            }
            
        );
    }
}