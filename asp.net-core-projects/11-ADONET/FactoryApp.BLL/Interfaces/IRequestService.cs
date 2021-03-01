using FactoryApp.BLL.BusinessModels;

namespace FactoryApp.BLL.Interfaces
{
    public interface IRequestService
    {
        void AddRequest(RequestModel requestModel);
        void AddRequestHandler(int requestId, int employeeId);
    }
}
