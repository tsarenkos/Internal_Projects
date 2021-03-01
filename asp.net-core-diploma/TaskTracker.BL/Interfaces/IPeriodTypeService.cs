using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.Models;

namespace TaskTracker.BL.Interfaces
{
    public interface IPeriodTypeService
    {
        IEnumerable<PeriodTypeModelBL> GetAll();
        PeriodTypeModelBL GetById(int id);
    }
}
