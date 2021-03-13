using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SubjectManagement.Application.System.Users;
using SubjectManagement.Data;
using SubjectManagement.Data.EF;

namespace SubjectManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            ServiceProvider.GetRequiredService<Db>();
            
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SubjectDbContext>(cs => cs.UseSqlServer(
                "Server=.\\SQLEXPRESS;Database=ComputerShopManager;Trusted_Connection=true;")
            );

            services.AddTransient(typeof(Db));

            services.AddTransient<IUserService, UserService>();
        }


    }
}
