using System.Windows;
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
            services.AddSingleton<MainWindow>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<InvoiceViewModel>();
            

            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
