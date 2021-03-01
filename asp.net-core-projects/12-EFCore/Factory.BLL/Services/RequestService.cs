using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Factory.DAL;
using Factory.DAL.Entities;
using System;
using System.Linq;


namespace Factory.BLL.Services
{
    public class RequestService:IRequestService
    {        
        public void AddRequest(RequestModel requestModel)
        {
            if (requestModel != null)
            {
                using (FactoryContext context = new FactoryContext())
                {
                    Request request = new Request();
                    request.RequestCreatorId = requestModel.RequestCreatorId;
                    request.RequestHandlerId = requestModel.RequestHandlerId;
                    request.MachineId = requestModel.MachineId;
                    request.DateOfCreate = DateTime.Now;
                    request.RequestStatusId = 1;
                    context.Requests.Add(request);
                    context.SaveChanges();
                }
            }            
        }
        public void AddRequestHandler(int requestId, int employeeId)
        {
            if (requestId > 0 && employeeId > 0)
            {
                using (FactoryContext context = new FactoryContext())
                {
                    Request request = context.Requests.FirstOrDefault(r => r.RequestId == requestId);
                    if (request != null)
                    {
                        request.RequestHandlerId = employeeId;
                        context.SaveChanges();
                    }
                }
            }            
        }
    }
}
