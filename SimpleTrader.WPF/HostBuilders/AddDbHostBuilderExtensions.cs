﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.EntityFramework;
using SimpleTrader.FinancialModelingPrepAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddDbHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString("sqlite");
                Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite(connectionString);
                services.AddDbContext<SimpleTraderDbContext>(configureDbContext);
                services.AddSingleton<SimpleTraderDbContextFactory>(new SimpleTraderDbContextFactory(o => o.UseSqlite(connectionString)));
            });

            return host;
        }
    }
}
