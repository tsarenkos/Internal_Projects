using Factory.BLL.BusinessModels;
using Factory.BLL.Interfaces;
using Factory.Storage.Core.Entities;
using Factory.Storage.Core.Interfaces;
using System;
using System.Collections.Generic;


namespace Factory.BLL.Services
{
    public class RequestService: IRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        public RequestService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public List<RequestModel> GetAllRequests()
        {
            var requests = unitOfWork.Requests.GetAll();
            if (requests == null)
            {
                return null;
            }

            List<RequestModel> requestModels = new List<RequestModel>();            
            foreach (var item in requests)
            {
                RequestModel request = new RequestModel();
                request.RequestId = item.RequestId;
                request.RequestCreatorId = item.RequestCreatorId;
                request.RequestHandlerId = item.RequestHandlerId;
                request.DateOfCreate = item.DateOfCreate;
                request.MachineId = item.MachineId;
                request.RequestStatusId = item.RequestStatusId;
                request.InnerRequestId = item.InnerRequestId;
                requestModels.Add(request);
            }
            
            return requestModels;
        }        


        public void AddRequest(RequestModel requestModel)
        {
            try
            {
                if (requestModel == null)
                {
                    throw new ArgumentNullException();
                }

                Request request = new Request();
                request.RequestCreatorId = requestModel.RequestCreatorId;
                request.RequestHandlerId = requestModel.RequestHandlerId;
                request.MachineId = requestModel.MachineId;
                request.DateOfCreate = DateTime.Now;
                request.RequestStatusId = 1;
                request.InnerRequestId = requestModel.InnerRequestId;

                unitOfWork.Requests.Create(request);
                unitOfWork.SaveChanges();
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
                if (requestId <= 0 || employeeId <= 0)
                {
                    throw new ArgumentException();
                }
                Request request = unitOfWork.Requests.GetById(requestId);
                if (request != null)
                {
                    request.RequestHandlerId = employeeId;
                    unitOfWork.Requests.Update(request);
                    unitOfWork.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }                                         
        }

        public RequestModel GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            Request request = unitOfWork.Requests.GetById(id);
            if (request == null)
            {
                return null;
            }            
            RequestModel requestModel = new RequestModel
            {
                RequestId = request.RequestId,
                RequestCreatorId = request.RequestCreatorId,
                RequestHandlerId = request.RequestHandlerId,
                MachineId = request.MachineId,
                DateOfCreate = request.DateOfCreate,
                RequestStatusId=request.RequestStatusId,
                InnerRequestId = request.InnerRequestId
            };
            return requestModel;
        }
    }
}
