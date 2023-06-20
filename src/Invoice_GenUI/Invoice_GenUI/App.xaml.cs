using System;
using System.Windows;
using Invoice_GenUI.Models.Services;
using Invoice_GenUI.Models;
using Invoice_GenUI.ViewModels;
using Invoice_GenUI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice_GenUI
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            services.AddTransient<MainViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<InvoiceViewModel>();
            services.AddTransient<CreateClientViewModel>();
            services.AddTransient<AddLineItemViewModel>();
            services.AddTransient<ShowClientsViewModel>();
            services.AddTransient<IClientService, ClientService>();

            services.AddSingleton<INavigationService, NavigationService>();
            

            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType
            => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
