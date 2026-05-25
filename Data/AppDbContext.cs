using Microsoft.EntityFrameworkCore;
using PcApi.Models;

namespace PcApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<PC> PCs { get; set; }

    public DbSet<Component> Components { get; set; }

    public DbSet<ComponentType> ComponentTypes { get; set; }

    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

    public DbSet<PCComponent> PCComponents { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Component PK
        modelBuilder.Entity<Component>()
            .HasKey(c => c.Code);


        // Composite Key
        modelBuilder.Entity<PCComponent>()
            .HasKey(pc => new
            {
                pc.PCId,
                pc.ComponentCode
            });


        // PC -> PCComponents
        modelBuilder.Entity<PCComponent>()
            .HasOne(pc => pc.PC)
            .WithMany(p => p.PCComponents)
            .HasForeignKey(pc => pc.PCId);


        // Component -> PCComponents
        modelBuilder.Entity<PCComponent>()
            .HasOne(pc => pc.Component)
            .WithMany(c => c.PCComponents)
            .HasForeignKey(pc => pc.ComponentCode);


        // Component -> Manufacturer
        modelBuilder.Entity<Component>()
            .HasOne(c => c.Manufacturer)
            .WithMany(m => m.Components)
            .HasForeignKey(c => c.ComponentManufacturersId);


        // Component -> Type
        modelBuilder.Entity<Component>()
            .HasOne(c => c.Type)
            .WithMany(t => t.Components)
            .HasForeignKey(c => c.ComponentTypesId);


        SeedData(modelBuilder);
    }


    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComponentManufacturer>()
            .HasData(

                new ComponentManufacturer
                {
                    Id = 1,
                    Abbreviation = "AMD",
                    FullName = "Advanced Micro Devices",
                    FoundationDate = new DateOnly(1969,5,1)
                },

                new ComponentManufacturer
                {
                    Id = 2,
                    Abbreviation = "NV",
                    FullName = "NVIDIA Corporation",
                    FoundationDate = new DateOnly(1993,4,5)
                }
            );


        modelBuilder.Entity<ComponentType>()
            .HasData(

                new ComponentType
                {
                    Id = 1,
                    Abbreviation = "CPU",
                    Name = "Processor"
                },

                new ComponentType
                {
                    Id = 2,
                    Abbreviation = "GPU",
                    Name = "Graphics Card"
                }
            );


        modelBuilder.Entity<Component>()
            .HasData(

                new Component
                {
                    Code = "CPU0000001",
                    Name = "Ryzen 7800X3D",
                    Description = "Gaming CPU",
                    ComponentManufacturersId = 1,
                    ComponentTypesId = 1
                },

                new Component
                {
                    Code = "GPU0000001",
                    Name = "RTX4080",
                    Description = "Gaming GPU",
                    ComponentManufacturersId = 2,
                    ComponentTypesId = 2
                }
            );


        modelBuilder.Entity<PC>()
            .HasData(

                new PC
                {
                    Id = 1,
                    Name = "Gaming Beast X",
                    Weight = 12.5,
                    Warranty = 36,
                    CreatedAt = DateTime.Parse("2026-05-08"),
                    Stock = 5
                }
            );


        modelBuilder.Entity<PCComponent>()
            .HasData(

                new PCComponent
                {
                    PCId = 1,
                    ComponentCode = "CPU0000001",
                    Amount = 1
                },

                new PCComponent
                {
                    PCId = 1,
                    ComponentCode = "GPU0000001",
                    Amount = 1
                }
            );
    }
}