using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.EntityFramework
{
    public class SimpleTraderDbContextFactory
    {
        private readonly string connectionString;

        public SimpleTraderDbContextFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SimpleTraderDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<SimpleTraderDbContext>();
            options.UseSqlServer(connectionString);

            return new SimpleTraderDbContext(options.Options);
        }
    }
}
