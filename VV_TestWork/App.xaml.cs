using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VV_TestWork.Domain;
using VV_TestWork.MVVM.ViewModels;
using VV_TestWork.MVVM.Views;

namespace VV_TestWork
{

    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();        

            Current.MainWindow = mainWindow;
            MainWindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer("server=localhost\\sqlexpress;database=vv_testwork;trusted_connection=true"));
            services.AddTransient(typeof(MainWindow));
        }

    }
}
