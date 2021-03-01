using Factory.BLL.BusinessModels;
using System.Collections.Generic;


namespace Factory.BLL.Interfaces
{
    public interface IRequestService
    {
        List<RequestModel> GetAllRequests();
        RequestModel GetById(int id);        
        void AddRequest(RequestModel requestModel);
        void AddRequestHandler(int requestId, int employeeId);
    }
}
