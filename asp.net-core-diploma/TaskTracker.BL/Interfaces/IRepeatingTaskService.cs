using System;
using System.Collections.Generic;
using System.Text;
using TaskTracker.Models;

namespace TaskTracker.BL.Interfaces
{
   public interface IRepeatingTaskService
    {
        IEnumerable<RepeatingTaskModelBL> GetAll();
        RepeatingTaskModelBL GetById(int id);
    }
}
