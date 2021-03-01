using System;
using System.Collections.Generic;
using TaskTracker.Web.Models;

namespace TaskTracker.Web.Services
{
    public interface IMyTaskFilterService
    {
        List<MyTaskViewModel> FilterTasksByDate(List<MyTaskViewModel> startList, string start, string end);
        List<MyTaskViewModel> FilterTasksByDelay(List<MyTaskViewModel> currentList);
        List<MyTaskViewModel> FilterTasksByName(List<MyTaskViewModel> currentList, string pattern);
        List<MyTaskViewModel> FilterTasksByCategory(List<MyTaskViewModel> currentList, int categoryId);
        List<MyTaskViewModel> FilterTasksByPriority(List<MyTaskViewModel> currentList, int priorityId);
        List<MyTaskViewModel> FilterTasksByTagName(List<MyTaskViewModel> currentList, string tagName);
    }

    public class MyTaskFilterService: IMyTaskFilterService
    {

        public List<MyTaskViewModel> FilterTasksByDate(List<MyTaskViewModel> startList, string start, string end)
        {
            if (start == null)
                return startList;

            List<MyTaskViewModel> resultList = new List<MyTaskViewModel>();
            DateTime startDate = Convert.ToDateTime(start);

            if (end == null)
            {
                foreach (MyTaskViewModel model in startList)
                {
                    if (model.TargetDate.Date == startDate.Date)
                        resultList.Add(model);
                }

                return resultList;
            }
            else
            {
                DateTime endDate = Convert.ToDateTime(end);
                foreach (MyTaskViewModel model in startList)
                {
                    if (model.TargetDate.Date >= startDate.Date && model.TargetDate.Date <= endDate.Date)
                        resultList.Add(model);
                }
                return resultList;
            }
        }

        public List<MyTaskViewModel> FilterTasksByDelay(List<MyTaskViewModel> currentList)
        {
            List<MyTaskViewModel> resultList = new List<MyTaskViewModel>();

            foreach (MyTaskViewModel model in currentList)
            {
                if (model.TargetDate.Date < DateTime.Today && model.EndDate == null)
                {
                    resultList.Add(model);
                }
            }
            return resultList;
        }

        public List<MyTaskViewModel> FilterTasksByCategory(List<MyTaskViewModel> currentList, int categoryId)
        {
            List<MyTaskViewModel> resultList = new List<MyTaskViewModel>();

            foreach (MyTaskViewModel model in currentList)
            {
                if (model.TaskСategoryId == categoryId)
                {
                    resultList.Add(model);
                }
            }
            return resultList;
        }

        public List<MyTaskViewModel> FilterTasksByPriority(List<MyTaskViewModel> currentList, int priorityId)
        {
            List<MyTaskViewModel> resultList = new List<MyTaskViewModel>();

            foreach (MyTaskViewModel model in currentList)
            {
                if (model.TaskPriorityId == priorityId)
                {
                    resultList.Add(model);
                }
            }
            return resultList;
        }

        public List<MyTaskViewModel> FilterTasksByTagName(List<MyTaskViewModel> currentList, string tagName)
        {
            List<MyTaskViewModel> resultList = new List<MyTaskViewModel>();

            foreach (MyTaskViewModel model in currentList)
            {
                if (model.Tags !=null)
                {
                    foreach (TagInfoViewModel tag in model.Tags)
                    {
                        if (tag.Name.ToUpper() == tagName.ToUpper())
                        {
                            resultList.Add(model);
                        }

                    }
                }
            }
            return resultList;
        }

        public List<MyTaskViewModel> FilterTasksByName(List<MyTaskViewModel> currentList, string pattern)
        {
            string[] words = pattern.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            List<MyTaskViewModel> resultList = new List<MyTaskViewModel>();

            foreach (MyTaskViewModel model in currentList)
            {
                foreach(string word in words)
                {
                    if (model.Name.ToLower().Contains(word.ToLower()))
                    {
                        resultList.Add(model);
                        break;
                    }
                }                
            }
            return resultList;
        }        
    }
}
