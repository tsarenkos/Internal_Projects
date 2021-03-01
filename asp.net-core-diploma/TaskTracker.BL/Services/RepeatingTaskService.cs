using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.BL.Interfaces;
using TaskTracker.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.BL.Services
{
    public class RepeatingTaskService : IRepeatingTaskService
    {
        private readonly IUnitOfWork context;
        public RepeatingTaskService(IUnitOfWork context)
        {
            this.context = context;
        }
        public IEnumerable<RepeatingTaskModelBL> GetAll()
        {
            List<RepeatingTaskModelBL> repeatTasksModel = new List<RepeatingTaskModelBL>();

            try
            {
                foreach (var repeatTask in context.RepeatingTasks.GetAll())
                {
                    repeatTasksModel.Add(new RepeatingTaskModelBL()
                    {
                        Id = repeatTask.Id,
                        Multiplier = repeatTask.Multiplier,
                        PeriodCode = repeatTask.PeriodCode,
                        PeriodName = repeatTask.PeriodType.Name
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

            return repeatTasksModel;
        }

        public RepeatingTaskModelBL GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return null;
                }

                return GetAll().FirstOrDefault(t => t.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}
