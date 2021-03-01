using Autofac;
using Factory.ADO.DAL.Repositories;
using Factory.Storage.Core.Interfaces;
using Factory.EF.DAL.Repositories;

namespace Factory.BLL.Infrastructure
{
    public class AutofacModule:Module
    {
        private const string AdoNet = "ADO.NET";
        private const string EntityFramework = "EF";

        private readonly string selectedDAL;
        private readonly string connectionString;

        public AutofacModule(string selectedDAL, string connectionString)
        {
            this.selectedDAL = selectedDAL;
            this.connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (selectedDAL == AdoNet)
            {
                builder.Register(c => new ADOUnitOfWork(connectionString))
                            .As<IUnitOfWork>()
                            .InstancePerLifetimeScope();
            }
            else if (selectedDAL == EntityFramework)
            {
                builder.Register(c => new EFUnitOfWork(connectionString))
                            .As<IUnitOfWork>()
                            .InstancePerLifetimeScope();
            }           
        }
    }
}
