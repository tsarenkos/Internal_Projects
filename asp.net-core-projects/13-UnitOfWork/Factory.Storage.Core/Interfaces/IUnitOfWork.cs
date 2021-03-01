using Factory.Storage.Core.Entities;

namespace Factory.Storage.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Employee> Employees { get; }
        IRepository<Deliverer> Deliverers { get; }
        IRepository<Machine> Machines { get; }
        IRepository<Request> Requests { get; }
        void SaveChanges();
    }
}
