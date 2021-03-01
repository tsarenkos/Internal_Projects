using System.Collections.Generic;
using WebApiApp.BLL.BusinessModels;

namespace WebApiApp.BLL.Interfaces
{
    public interface IFactoryService
    {
        List<EmployeeModel> GetAll();
        EmployeeModel Get(int id);
        void Create(EmployeeModel item);
        void Update(EmployeeModel item);
        void Delete(int id);
    }
}
