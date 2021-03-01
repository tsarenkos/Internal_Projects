using System.Collections.Generic;
using TaskTracker.BL.Interfaces;
using TaskTracker.DAL.Models;
using TaskTracker.Models;
using TaskTracker.Storage.Core.Entities;
using TaskTracker.Storage.Core.Interfaces;

namespace TaskTracker.BL.Services
{
    public class PeriodTypeService: IPeriodTypeService
    {
        private readonly IUnitOfWork context;

        public PeriodTypeService(IUnitOfWork context)
        {
            this.context = context;
        }
        public IEnumerable<PeriodTypeModelBL> GetAll()
        {
            IEnumerable<PeriodType> periodTypes = context.PeriodTypes.GetAll();
            List<PeriodTypeModelBL> periodTypesBL = new List<PeriodTypeModelBL>();

            foreach (var item in periodTypes)
            {
                PeriodTypeModelBL period = new PeriodTypeModelBL
                {
                    Id = item.Id,
                    Name = item.Name
                };
                periodTypesBL.Add(period);
            }

            return periodTypesBL;
        }

        public PeriodTypeModelBL GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            PeriodType periodType = context.PeriodTypes.GetById(id);

            if (periodType == null)
            {
                return null;
            }

            PeriodTypeModelBL periodTypeBL = new PeriodTypeModelBL
            {
                Id = periodType.Id,
                Name = periodType.Name
            };

            return periodTypeBL;
        }
    }
}
