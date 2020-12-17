using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinancialModelingPrepAPI;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Assets;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c =>
                {
                    c.AddJsonFile("appsettings.json");
                    c.AddEnvironmentVariables();
                })
                .ConfigureServices((context,services) =>
                {
                    string apikey = context.Configuration.GetValue<string>("FINANCE_API_KEY");
                    services.AddSingleton<FinancialModelingPrepHttpClientFactory>(new FinancialModelingPrepHttpClientFactory(apikey));

                    string connectionString = context.Configuration.GetConnectionString("sqlite");
                    Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite(connectionString);
                    services.AddDbContext<SimpleTraderDbContext>(configureDbContext);
                    services.AddSingleton<SimpleTraderDbContextFactory>(new SimpleTraderDbContextFactory(o => o.UseSqlite(connectionString)));
                    services.AddSingleton<IAuthenticationService, AuthenticationService>();
                    services.AddSingleton<IStockPriceService, StockPriceService>();
                    services.AddSingleton<IDataService<Account>, AccountDataService>();
                    services.AddSingleton<IAccountService, AccountDataService>();
                    services.AddSingleton<IBuyStockService, BuyStockService>();
                    services.AddSingleton<ISellStockService, SellStockService>();
                    services.AddSingleton<IMajorIndexService, MajorIndexService>();

                    services.AddSingleton<IPasswordHasher, PasswordHasher>();

                    services.AddSingleton<ISimpleTraderViewModelFactory, SimpleTraderViewModelFactory>();
                    services.AddSingleton<BuyViewModel>();
                    services.AddSingleton<PortfolioViewModel>();
                    services.AddSingleton<SellViewModel>();
                    services.AddSingleton<AssetSummaryViewModel>();
                    services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
                    services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
                    services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();

                    services.AddSingleton<HomeViewModel>
                        (s =>
                            new HomeViewModel(MajorIndexListingViewModel.LoadMajorIndexViewModel
                            (
                                s.GetRequiredService<IMajorIndexService>()), s.GetRequiredService<AssetSummaryViewModel>()
                            )
                        );

                    services.AddSingleton<CreateViewModel<HomeViewModel>>(s =>
                    {
                        return () => s.GetRequiredService<HomeViewModel>();
                    });

                    services.AddSingleton<CreateViewModel<BuyViewModel>>(s =>
                    {
                        return () => s.GetRequiredService<BuyViewModel>();
                    });

                    services.AddSingleton<CreateViewModel<PortfolioViewModel>>(s =>
                    {
                        return () => s.GetRequiredService<PortfolioViewModel>();
                    });

                    services.AddSingleton<CreateViewModel<RegisterViewModel>>(s =>
                    {
                        return () => new RegisterViewModel(
                            s.GetRequiredService<IAuthenticator>(),
                            s.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
                            s.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>()
                            );
                    });

                    services.AddSingleton<CreateViewModel<LoginViewModel>>(s =>
                    {
                        return () => new LoginViewModel(
                            s.GetRequiredService<IAuthenticator>(),
                            s.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
                            s.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>()
                            );
                    });

                    services.AddSingleton<CreateViewModel<SellViewModel>>(s =>
                    {
                        return () => s.GetRequiredService<SellViewModel>();
                    });

                    services.AddScoped<MainViewModel>();
                    services.AddSingleton<INavigator, Navigator>();
                    services.AddSingleton<IAuthenticator, Authenticator>();
                    services.AddSingleton<IAccountStore, AccountStore>();
                    services.AddSingleton<AssetStore>();

                    services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
                });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            host.Start();

            SimpleTraderDbContextFactory contextFactory = host.Services.GetRequiredService<SimpleTraderDbContextFactory>();
            using(SimpleTraderDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }

            Window window = host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync();
            host.Dispose();

            base.OnExit(e);
        }
    }
}
