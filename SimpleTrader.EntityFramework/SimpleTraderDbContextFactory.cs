using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.EntityFramework
{
    public class SimpleTraderDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> configurationDbContext;

        public SimpleTraderDbContextFactory(Action<DbContextOptionsBuilder> configurationDbContext)
        {
            this.configurationDbContext = configurationDbContext;
        }

        public SimpleTraderDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<SimpleTraderDbContext>();
            configurationDbContext(options);

            return new SimpleTraderDbContext(options.Options);
        }
    }
}
