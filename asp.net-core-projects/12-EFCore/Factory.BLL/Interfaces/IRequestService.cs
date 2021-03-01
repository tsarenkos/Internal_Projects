using Factory.BLL.BusinessModels;

namespace Factory.BLL.Interfaces
{
    public interface IRequestService
    {        
        void AddRequest(RequestModel requestModel);
        void AddRequestHandler(int requestId, int employeeId);
    }
}
