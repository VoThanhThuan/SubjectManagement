using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SubjectManagement.Data.EF;

namespace SubjectManagement.Data
{
    public class Db
    {
        public SubjectDbContext Context { get; set; }

        public Db()
        {
            connection();
        }

        public IServiceProvider ServiceProvider { get; private set; }

        public void connection()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            Context = ServiceProvider.GetRequiredService<SubjectDbContext>();
        }

        private void ConfigureServices(IServiceCollection services)
        {

            //services.AddDbContext<SubjectDbContext>(cs => cs.UseSqlServer(
            //    @"Server =.\SQLEXPRESS; Database=SubjectDatabase; Trusted_Connection=True;")
            //);

            services.AddDbContext<SubjectDbContext>(cs => cs.UseSqlServer(MyConnect.ConnectString));

            services.AddTransient(typeof(SubjectDbContext));

        }

    }
}
