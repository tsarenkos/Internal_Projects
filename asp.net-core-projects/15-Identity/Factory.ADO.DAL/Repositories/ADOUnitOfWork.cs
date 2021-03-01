using Factory.Storage.Core.Entities;
using Factory.Storage.Core.Interfaces;

namespace Factory.ADO.DAL.Repositories
{
    public class ADOUnitOfWork:IUnitOfWork
    {
        private readonly ADONetContext context;
        public ADOUnitOfWork(string connectionString)
        {
            this.context = new ADONetContext(connectionString);
        }

        private IRepository<Employee> employeeRepository;
        private IRepository<Deliverer> delivererRepository;
        private IRepository<Machine> machineRepository;
        private IRepository<Request> requestRepository;

        public IRepository<Employee> Employees
        {
            get
            {
                if (employeeRepository == null)
                {
                    employeeRepository = new EmployeeRepository(context);
                }
                return employeeRepository;
            }
        }

        public IRepository<Deliverer> Deliverers
        {
            get
            {
                if (delivererRepository == null)
                {
                    delivererRepository = new DelivererRepository(context);
                }
                return delivererRepository;
            }
        }
        public IRepository<Machine> Machines
        {
            get
            {
                if (machineRepository == null)
                {
                    machineRepository = new MachineRepository(context);
                }
                return machineRepository;
            }
        }

        public IRepository<Request> Requests
        {
            get
            {
                if (requestRepository == null)
                {
                    requestRepository = new RequestRepository(context);
                }
                return requestRepository;
            }
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
