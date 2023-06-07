using System;
using System.Windows;
using Invoice_GenUI.Models;
using Invoice_GenUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice_GenUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services) // Add instance of a viewmodel
        {
            services.AddTransient<StartUpWindow>();
            services.AddTransient<CreateClientWindow>();
            services.AddTransient<CreateClientViewModel>();
            services.AddTransient<ClientService>();
            services.AddTransient<InvoiceWindow>();
            services.AddTransient<InvoiceViewModel>(); // Gives brand new instance of invoiceviewmodel every time
            services.AddTransient<IClientService, ClientService>();
        }
        private void OnStartUp(object sender, EventArgs e)
        {
            var startUpWindow = serviceProvider.GetService<StartUpWindow>();
            startUpWindow.Show();
        }
    }

}
