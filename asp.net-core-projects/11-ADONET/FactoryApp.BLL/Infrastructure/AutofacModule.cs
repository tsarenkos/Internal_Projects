using Autofac;
using FactoryApp.DAL.Entities;
using FactoryApp.DAL.Interfaces;
using FactoryApp.DAL.Repositories;

namespace FactoryApp.BLL.Infrastructure
{
    public class AutofacModule:Module
    {
        private readonly string connectionString;
        public AutofacModule(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DelivererRepository(connectionString))
                .As<IRepository<DelivererEntity>>()
                .InstancePerLifetimeScope();
            builder.Register(c => new EmployeeRepository(connectionString))
                .As<IRepository<EmployeeEntity>>()
                .InstancePerLifetimeScope();
            builder.Register(c => new RequestRepository(connectionString))
                .As<IRepository<RequestEntity>>()
                .InstancePerLifetimeScope();
            builder.Register(c => new MachineRepository(connectionString))
                .As<IRepository<MachineEntity>>()
                .InstancePerLifetimeScope();
        }
    }
}
