using FactoryApp.BLL.BusinessModels;
using FactoryApp.BLL.Interfaces;
using FactoryApp.DAL.Entities;
using FactoryApp.DAL.Interfaces;
using System;


namespace FactoryApp.BLL.Services
{
    public class RequestService:IRequestService
    {
        private readonly IRepository<RequestEntity> repository;
        private readonly IRepository<EmployeeEntity> employeeRepository;

        public RequestService(IRepository<RequestEntity> repository, IRepository<EmployeeEntity> employeeRepository)
        {
            this.repository = repository;
            this.employeeRepository = employeeRepository;
        }

        public void AddRequest(RequestModel request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException();
                }
                RequestEntity requestEntity = new RequestEntity();
                requestEntity.RequestCreatorId = request.RequestCreatorId;
                requestEntity.RequestHandlerId = request.RequestHandlerId;
                requestEntity.MachineId = request.MachineId;
                requestEntity.RequestStatusId = 1;
                requestEntity.DateOfCreate = DateTime.Now;
                requestEntity.InnerRequestId = request.InnerRequestId;

                repository.Create(requestEntity);
            }
            catch (Exception exc)
            {
                throw exc;
            }            
        }

        public void AddRequestHandler(int requestId, int employeeId)
        {
            try
            {
                if(requestId<=0 || employeeId <= 0)
                {
                    throw new ArgumentException();
                }

                RequestEntity request = repository.GetById(requestId);
                EmployeeEntity employee = employeeRepository.GetById(employeeId);

                if(request!= null && employee != null)
                {
                    request.RequestHandlerId = employeeId;
                    repository.Update(request);
                }
            }
            catch (Exception exc )
            {
                throw exc;
            }
        }
    }
}
