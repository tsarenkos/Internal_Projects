using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Models;
using TaskTracker.BL.Interfaces;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.BL.Services
{
    public class PriorityService : IPriorityServiceBL
    {
        private readonly IUnitOfWork context;
        public PriorityService(IUnitOfWork context)
        {
            this.context = context;
        }

        public IEnumerable<PriorityModelBL> GetAll()
        {
            List<PriorityModelBL> priorityModels = new List<PriorityModelBL>();

            try
            {
                foreach (var priority in context.PriorityTypes.GetAll())
                {
                    priorityModels.Add(new PriorityModelBL()
                    {
                        Id = priority.Id,
                        Name = priority.Name
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

            return priorityModels;
        }

        public PriorityModelBL GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return null;
                }

                return GetAll().FirstOrDefault(p => p.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
