using Exam.App.Domain;
using Exam.App.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Infrastructure.Database;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AnimalSpecies> AnimalSpecies { get; set; }
    public DbSet<Cage> Cages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Roles
        modelBuilder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int> { Id = 1, Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
            new IdentityRole<int> { Id = 2, Name = "User", NormalizedName = "USER" }
        );



        // Seed Buildings
        modelBuilder.Entity<Cage>(e =>
        {
            e.HasData(
              new Cage { Id = 1, Code = "A1-SAVANA" },
              new Cage { Id = 2, Code = "A2-DŽUNGLA" },
              new Cage { Id = 3, Code = "A3-PLANINA" },
              new Cage { Id = 4, Code = "B1-PTICE" },
              new Cage { Id = 5, Code = "B2-REPTILI" },
              new Cage { Id = 6, Code = "C1-VODENI" },
              new Cage { Id = 7, Code = "C2-NOĆNI" }
            );
        });

        // Seed Animals
        modelBuilder.Entity<AnimalSpecies>(e =>
        {
            e.HasData(
              new AnimalSpecies { Id = 1, Name = "Lav", Species = "Panthera leo", Mass = 190, CageId = 1 },
              new AnimalSpecies { Id = 2, Name = "Tigar", Species = "Panthera tigris", Mass = 220, CageId = 2 },
              new AnimalSpecies { Id = 3, Name = "Slon", Species = "Loxodonta africana", Mass = 6000, CageId = 3 },
              new AnimalSpecies { Id = 4, Name = "Žirafa", Species = "Giraffa camelopardalis", Mass = 800, CageId = 3 },
              new AnimalSpecies { Id = 5, Name = "Zebra", Species = "Equus quagga", Mass = 350, CageId = 1 },
              new AnimalSpecies { Id = 6, Name = "Nosorog", Species = "Rhinocerotidae", Mass = 2300, CageId = 3 },
              new AnimalSpecies { Id = 7, Name = "Gepard", Species = "Acinonyx jubatus", Mass = 72, CageId = 2 },
              new AnimalSpecies { Id = 8, Name = "Hijena", Species = "Crocuta crocuta", Mass = 60, CageId = 2 },
              new AnimalSpecies { Id = 9, Name = "Medved", Species = "Ursus arctos", Mass = 300, CageId = 3 },
              new AnimalSpecies { Id = 10, Name = "Vuk", Species = "Canis lupus", Mass = 50, CageId = 7 },
              new AnimalSpecies { Id = 11, Name = "Lisica", Species = "Vulpes vulpes", Mass = 8, CageId = 7 },
              new AnimalSpecies { Id = 12, Name = "Sova", Species = "Strix aluco", Mass = 1.2, CageId = 4 },
              new AnimalSpecies { Id = 13, Name = "Orao", Species = "Aquila chrysaetos", Mass = 6, CageId = 4 },
              new AnimalSpecies { Id = 14, Name = "Krokodil", Species = "Crocodylus niloticus", Mass = 500, CageId = 5 },
              new AnimalSpecies { Id = 15, Name = "Pingvin", Species = "Aptenodytes forsteri", Mass = 30, CageId = 6 },
              new AnimalSpecies { Id = 16, Name = "Flamingo", Species = "Phoenicopterus roseus", Mass = 3, CageId = 4 },
              new AnimalSpecies { Id = 17, Name = "Kengur", Species = "Macropus rufus", Mass = 85, CageId = 1 },
              new AnimalSpecies { Id = 18, Name = "Panda", Species = "Ailuropoda melanoleuca", Mass = 100, CageId = 3 },
              new AnimalSpecies { Id = 19, Name = "Lemur", Species = "Lemur catta", Mass = 3, CageId = 7 },
              new AnimalSpecies { Id = 20, Name = "Morski lav", Species = "Zalophus californianus", Mass = 200, CageId = 6 }
            );
        });




        modelBuilder.Entity<AnimalSpecies>().HasOne(a => a.Cage).WithMany(c => c.CagedAnimals).HasForeignKey(a => a.CageId).OnDelete(DeleteBehavior.Restrict);

    }
}