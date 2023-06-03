using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Invoice_Gen.Domain;

[ExcludeFromCodeCoverage]
public class DbContextFactory : IDesignTimeDbContextFactory<InvoiceGenDbContext>
{
    public InvoiceGenDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InvoiceGenDbContext>();

        optionsBuilder.UseSqlite("Data Source=invoiceDatabase.db");

        return new InvoiceGenDbContext(optionsBuilder.Options);
    }
}
