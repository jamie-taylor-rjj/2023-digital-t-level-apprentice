using System.Diagnostics.CodeAnalysis;
using Invoice_Gen.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Gen.Domain;

[ExcludeFromCodeCoverage]
public class InvoiceGenDbContext : DbContext, IDbContext
{
    public InvoiceGenDbContext(DbContextOptions<InvoiceGenDbContext> options) : base(options) { }

    public InvoiceGenDbContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data
        modelBuilder.Entity<Client>().HasData(
            new
            {
                ClientId = 1,
                ClientName = "Muller Inc",
                ContactName = "Bradley S Crooks",
                ClientAddress = "4509 Clement Street, Atlanta, GA 30331",
                ContactEmail = "AaronDBaker@dayrep.com"
            },
            new Client
            {
                ClientId = 2,
                ClientName = "Gutkowski Inc",
                ContactName = "Anna L. Witt",
                ClientAddress = "2545 James Street, Fairport, NY 14450",
                ContactEmail = "AnnaLWitt@teleworm.us"
            },
            new Client
            {
                ClientId = 3,
                ClientName = "Hoeger - Gislason",
                ContactName = "Susan Railey",
                ClientAddress = "923 Euclid Avenue, San Luis Obispo, CA 93401",
                ContactEmail = "SusanMRailey@armyspy.com"
            },
            new Client
            {
                ClientId = 4,
                ClientName = "Toy Group",
                ContactName = "Stanley D. Rogers",
                ClientAddress = "2607 Goldcliff Circle, Washington, DC 20005",
                ContactEmail = "StanleyDRogers@dayrep.com"
            },
            new Client
            {
                ClientId = 5,
                ClientName = "Upton, Gleason and Cronin",
                ContactName = "Tammy W. Finley",
                ClientAddress = "1381 Monroe Street, Houston, TX 77030",
                ContactEmail = "TammyWFinley@dayrep.com"
            }
        );

        modelBuilder.Entity<Invoice>().HasData(
            new Invoice
            {
                InvoiceId = 1,
                ClientId = 1,
                DueDate = new DateTime(2024, 01, 01),
                IssueDate = new DateTime(2023, 06, 23),
                VatRate = 25
            }
        );

        modelBuilder.Entity<LineItem>().HasData(
            new LineItem
            {
                LineItemId = 1,
                InvoiceId = 1,
                Cost = 80.00,
                Description = "Time spent building API (per hour)",
                Quantity = 1
            }
        );

        // Tables
        modelBuilder.Entity<Client>().ToTable("Clients").HasKey(c => c.ClientId);
        modelBuilder.Entity<Invoice>().ToTable("Invoices").HasKey(i => i.InvoiceId);
        modelBuilder.Entity<LineItem>().ToTable("LineItems").HasKey(l => l.LineItemId);

        // Mappings
        modelBuilder.Entity<Client>()
            .HasMany(c => c.Invoices)
            .WithOne(i => i.Client)
            .HasForeignKey(i => i.ClientId)
            .IsRequired();

        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.LineItems)
            .WithOne(l => l.Invoice)
            .HasForeignKey(l => l.InvoiceId)
            .IsRequired();

        // Navigation Properties
        modelBuilder.Entity<Invoice>().Navigation(i => i.LineItems).AutoInclude();
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<LineItem> LineItems => Set<LineItem>();
}
