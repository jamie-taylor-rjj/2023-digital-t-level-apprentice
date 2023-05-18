using System;
using System.Windows;
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
            services.AddTransient<InvoiceWindow>();
            services.AddTransient<InvoiceViewModel>(); // Gives brand new instance of invoiceviewmodel every time
        }
        private void OnStartUp(object sender, EventArgs e)
        { 
            var startUpWindow = serviceProvider.GetService<StartUpWindow>();
            startUpWindow.Show();
        }
    }

}
