using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using TaskTracker.Models;
using TaskTracker.Shared.Interfaces;
using TaskTracker.Web.Interfaces;
using TaskTracker.Web.Models;
using TaskTracker.Web.Services;

namespace TaskTracker.UI.Controllers
{
    [Authorize(AuthenticationSchemes = "TokenAuthenticationScheme")]
    public class MyTaskController : Controller
    {
        private const int PageSize = 3;
        private readonly ILogger<MyTaskController> _logger;
        private readonly IAPIClient _client;
        private readonly IIdentityClient _identityClient;
        private readonly IMyTaskFilterService _filterService;

        public MyTaskController(ILogger<MyTaskController> logger, IAPIClient client, IMyTaskFilterService filterService, IIdentityClient identityClient)
        {
            _client = client;
            _logger = logger;
            _filterService = filterService;
            _identityClient = identityClient;
        }

        public async Task<IActionResult> Index(string start, string end, bool? delay, bool? completed, int categoryId, int priorityId, string pattern, int page = 1, string tagName = "")
        {
            IEnumerable<TaskModelBL> tasks = await _client.Get<List<TaskModelBL>>("api/mytask/list");

            List<MyTaskViewModel> viewModels = new List<MyTaskViewModel>();

            if (tasks != null)
            {
                foreach (var task in tasks)
                {
                    MyTaskViewModel viewModel = new MyTaskViewModel
                    {
                        Id = task.Id,
                        Name = task.Name,
                        StartDate = task.StartDate,
                        TargetDate = task.TargetDate,
                        EndDate = task.EndDate,
                        Details = task.Details,
                        IsRepeating = task.IsRepeating,
                        TaskСategoryId = task.TaskСategoryId,
                        TaskPriorityId = task.TaskPriorityId,
                        ParentTaskId = task.ParentTaskId,
                        files = null,
                        IsFriendTask = task.IsFriendTask
                    };
                    if (task.EndDate == null && completed != true)
                    {
                        viewModels.Add(viewModel);
                    }
                    if (task.EndDate != null && completed == true)
                    {
                        viewModels.Add(viewModel);
                    }

                    List<TaskTagModelBL> taskTag = await _client.Get<List<TaskTagModelBL>>("api/tasktag/fortask/" + task.Id.ToString());
                    if (taskTag != null)
                    {
                        viewModel.Tags = new List<TagInfoViewModel>();
                        foreach (TaskTagModelBL fl in taskTag)
                        {
                            viewModel.Tags.Add(new TagInfoViewModel()
                            {
                                Id = fl.Id,
                                Name = fl.Name
                            });
                        }
                    }
                }
            }
            if (delay == true)
            {
                viewModels = _filterService.FilterTasksByDelay(viewModels);
            }
            if (start != null)
            {
                viewModels = _filterService.FilterTasksByDate(viewModels, start, end);
            }
            if (pattern != null && !pattern.Contains("#"))
            {
                viewModels = _filterService.FilterTasksByName(viewModels, pattern);
            }
            if (categoryId != 0)
            {
                viewModels = _filterService.FilterTasksByCategory(viewModels, categoryId);
            }
            if (priorityId != 0)
            {
                viewModels = _filterService.FilterTasksByPriority(viewModels, priorityId);
            }
            if (!string.IsNullOrEmpty(tagName) || (pattern != null && pattern.Contains("#")))
            {
                viewModels = _filterService.FilterTasksByTagName(viewModels, string.IsNullOrEmpty(tagName) ? pattern.Replace("#", "") : tagName);
            }

            List<TaskCategoryModelBL> categories = await _client.Get<List<TaskCategoryModelBL>>("api/taskcategory");
            List<PriorityModelBL> priorities = await _client.Get<List<PriorityModelBL>>("api/priority");

            categories.Insert(0, new TaskCategoryModelBL { Name = "", Id = 0 });
            priorities.Insert(0, new PriorityModelBL { Name = "", Id = 0 });

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Priorities = new SelectList(priorities, "Id", "Name");

            PageMyTaskViewModel pageViewModel = new PageMyTaskViewModel(viewModels.Count, page, PageSize);

            IndexMyTaskViewModel indexViewModel = new IndexMyTaskViewModel()
            {
                MyTasks = viewModels.Skip((page - 1) * PageSize).Take(PageSize).ToList(),
                PageViewModel = pageViewModel
            };

            if (start != null)
            {
                indexViewModel.StartDate = Convert.ToDateTime(start);
            }

            if (end != null)
            {
                indexViewModel.EndDate = Convert.ToDateTime(end);
            }

            if (delay != null)
            {
                indexViewModel.Delay = delay;
            }

            if (completed != null)
            {
                indexViewModel.Completed = completed;
            }

            if (pattern != null && !pattern.Contains("#"))
            {
                indexViewModel.Pattern = pattern;
            }

            if (categoryId != 0)
            {
                indexViewModel.CategoryId = categoryId;
            }

            if (priorityId != 0)
            {
                indexViewModel.PriorityId = priorityId;
            }

            return View(indexViewModel);
        }

        public async Task<IActionResult> Create()
        {
            List<PeriodTypeModelBL> periodTypes = await _client.Get<List<PeriodTypeModelBL>>("api/periodtype");
            List<TaskCategoryModelBL> categories = await _client.Get<List<TaskCategoryModelBL>>("api/taskcategory");
            List<PriorityModelBL> priorities = await _client.Get<List<PriorityModelBL>>("api/priority");

            categories.Insert(0, new TaskCategoryModelBL { Name = "", Id = 0 });
            priorities.Insert(0, new PriorityModelBL { Name = "", Id = 0 });

            ViewBag.Repeat = new SelectList(periodTypes, "Id", "Name");
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Priorities = new SelectList(priorities, "Id", "Name");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(MyTaskViewModel model, IFormFile[] uploadedFiles)
        {
            if (ModelState.IsValid)
            {
                TaskModelBL task = new TaskModelBL
                {
                    Name = model.Name,
                    TargetDate = model.TargetDate,
                    Details = model.Details,
                    IsRepeating = model.IsRepeating,
                    Multiplier = model.Multiplier,
                    PeriodCode = model.PeriodCode,
                    ParentTaskId = model.ParentTaskId,
                    files = new List<FileModelBL>()
                };

                if (model.TaskСategoryId != 0)
                {
                    task.TaskСategoryId = model.TaskСategoryId;
                }

                if (model.TaskPriorityId != 0)
                {
                    task.TaskPriorityId = model.TaskPriorityId;
                }

                if(task.IsRepeating==true && task.Multiplier == null)
                {
                    task.Multiplier = 1;
                }

                foreach (IFormFile uploadedFile in uploadedFiles)
                {
                    using (var ms = new MemoryStream())
                    {
                        uploadedFile.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        FileModelBL fl = new FileModelBL
                        {
                            FileName = uploadedFile.FileName,
                            ContentType = uploadedFile.ContentType,
                            Data = fileBytes
                        };
                        task.files.Add(fl);
                    }
                }

                await _client.Post("api/mytask/create", task);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            TaskModelBL task = await _client.Get<TaskModelBL>("api/mytask/details/" + id.ToString());

            if (task == null)
            {
                return NotFound();
            }
            MyTaskViewModel viewModel = new MyTaskViewModel
            {
                Id = task.Id,
                Name = task.Name,
                StartDate = task.StartDate,
                TargetDate = task.TargetDate,
                Details = task.Details,
                IsRepeating = task.IsRepeating,
                TaskСategoryId = task.TaskСategoryId,
                TaskPriorityId = task.TaskPriorityId,
                Multiplier = task.Multiplier,
                PeriodCode = task.PeriodCode,
                ParentTaskId = task.ParentTaskId,
                TaskEditGrant = task.EditGrant,
                IsFriendTask = task.IsFriendTask
            };
            viewModel.files = new List<FileInfoViewModel>();
            foreach (FileModelBL fl in task.files)
            {
                viewModel.files.Add(new FileInfoViewModel()
                {
                    Id = fl.Id,
                    FileName = fl.FileName
                });
            }

            TaskTagModelBL taskTag = await _client.Get<TaskTagModelBL>("api/tasktag/details/" + id.ToString());
            if (taskTag != null && task.Tags!=null)
            {
                viewModel.Tags = new List<TagInfoViewModel>();
                foreach (TaskTagModelBL fl in task.Tags)
                {
                    viewModel.Tags.Add(new TagInfoViewModel()
                    {
                        Id = fl.Id,
                        Name = fl.Name
                    });
                }
            }

            List<PeriodTypeModelBL> periodTypes = await _client.Get<List<PeriodTypeModelBL>>("api/periodtype");
            List<TaskCategoryModelBL> categories = await _client.Get<List<TaskCategoryModelBL>>("api/taskcategory");
            List<PriorityModelBL> priorities = await _client.Get<List<PriorityModelBL>>("api/priority");
            List<TaskModelBL> tasks = await _client.Get<List<TaskModelBL>>("api/mytask/list");

            viewModel.SubTasks = tasks.Where(t => t.ParentTaskId == task.Id)?.ToList();

            IEnumerable<UserFriendBL> friends = await _identityClient.Get<List<UserFriendBL>>("api/friends");
            IEnumerable<string> UserIds = task.UserIds;
            ViewBag.friendslist = friends.Where(fr => UserIds.Any(uid => uid == fr.UserId)).Select(f => f.Friend).ToList();

            categories.Insert(0, new TaskCategoryModelBL { Name = "", Id = 0 });
            priorities.Insert(0, new PriorityModelBL { Name = "", Id = 0 });


            ViewBag.Repeat = new SelectList(periodTypes, "Id", "Name");
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Priorities = new SelectList(priorities, "Id", "Name");

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, IFormFile[] uploadedFiles, [FromForm] MyTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                TaskModelBL task = new TaskModelBL()
                {
                    Id = id,
                    Name = model.Name,
                    TargetDate = model.TargetDate,
                    Details = model.Details,
                    IsRepeating = model.IsRepeating,
                    ParentTaskId = model.ParentTaskId,
                    Multiplier = model.Multiplier,
                    PeriodCode = model.PeriodCode
                };
                task.files = new List<FileModelBL>();

                if (model.files != null)
                {
                    foreach (FileInfoViewModel fl in model.files)
                    {
                        if (fl.Deleted == 0)
                        {
                            task.files.Add(new FileModelBL()
                            {
                                Id = fl.Id
                            });
                        }
                    }
                }

                foreach (IFormFile uploadedFile in uploadedFiles)
                {
                    using (var ms = new MemoryStream())
                    {
                        uploadedFile.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        FileModelBL fl = new FileModelBL
                        {
                            FileName = uploadedFile.FileName,
                            ContentType = uploadedFile.ContentType,
                            Data = fileBytes
                        };
                        task.files.Add(fl);
                    }
                }

                if (model.TaskСategoryId != 0)
                {
                    task.TaskСategoryId = model.TaskСategoryId;
                }

                if (model.TaskPriorityId != 0)
                {
                    task.TaskPriorityId = model.TaskPriorityId;
                }

                await _client.Put("api/mytask/update", id, task);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public async Task<IActionResult> Complete(int id, bool? all)
        {
            TaskModelBL task = await _client.Get<TaskModelBL>("api/mytask/details/" + id.ToString());

            if (task == null)
            {
                return NotFound();
            }

            if (task.IsRepeating == false || (task.IsRepeating == true && all == true))
            {
                task.EndDate = DateTime.Now;
            }
            else
            {
                RepeatingTaskModelBL repeatTask = await _client.Get<RepeatingTaskModelBL>("api/repeatingtask/" + id.ToString());
                if (repeatTask != null)
                {
                    if(repeatTask.PeriodCode == 1)
                    {
                        task.TargetDate = task.TargetDate.AddDays(repeatTask.Multiplier);
                    }
                    if (repeatTask.PeriodCode == 2)
                    {
                        task.TargetDate = task.TargetDate.AddDays(7 * repeatTask.Multiplier);
                    }
                    if (repeatTask.PeriodCode == 3)
                    {
                        task.TargetDate = task.TargetDate.AddMonths(repeatTask.Multiplier);
                    }
                    if (repeatTask.PeriodCode == 4)
                    {
                        task.TargetDate = task.TargetDate.AddYears(repeatTask.Multiplier);
                    }
                }
            }

            await _client.Put("api/mytask/update", id, task);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddSubTask(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException();
            }

            SubTaskViewModel model = new SubTaskViewModel { ParentTaskId = id };

            IEnumerable<TaskModelBL> subTasks = await _client.Get<List<TaskModelBL>>("api/subtasks/" + id.ToString());
            ViewBag.SubTasks = new SelectList(subTasks, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubTask(SubTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                TaskModelBL task = await _client.Get<TaskModelBL>("api/mytask/details/" + model.SubTaskId.ToString());

                if (task == null)
                {
                    return NotFound();
                }

                task.ParentTaskId = model.ParentTaskId;

                await _client.Put("api/mytask/update", task.Id, task);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _client.Delete("api/mytask/delete", id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddFriendToTask(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException();
            }

            MyTaskWithFriendViewModel model = new MyTaskWithFriendViewModel { TaskId = id };
            IEnumerable<UserFriendBL> friends = await _identityClient.Get<List<UserFriendBL>>("api/friends");
            TaskModelBL task = await _client.Get<TaskModelBL>("api/mytask/details/" + id.ToString());
            IEnumerable<string> UserIds = task.UserIds;
            var friendslist = friends.Where(fr => !UserIds.Any(uid => uid == fr.UserId));
            ViewBag.UserFriends = new SelectList(friendslist, "FriendId", "Friend.UserName");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddFriendToTask(MyTaskWithFriendViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                TaskModelBL task = await _client.Get<TaskModelBL>("api/mytask/details/" + viewModel.TaskId.ToString());
                IEnumerable<UserFriendBL> friends = await _identityClient.Get<List<UserFriendBL>>("api/friends");
                UserFriendBL friend = friends.FirstOrDefault(fr => fr.FriendId == viewModel.FriendId.ToString());

                if (task == null || friend == null)
                {
                    return NotFound();
                }

                task.Friends = new List<UserFriendBL> { friend };

                await _client.Put("api/mytask/update", task.Id, task);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> AddEditGrantRequest(int id)
        {
            TaskModelBL task = await _client.Get<TaskModelBL>("api/mytask/details/" + id.ToString());

            TaskEditGrantsBL editGrand = new TaskEditGrantsBL
            {
                TaskId = id,
                Task = task,
                date = DateTime.Now,
                FriendId = task.UserId,
                IsGranted = false
            };

            await _client.Post("api/mytaskfriendgrant/create", editGrand);

            return RedirectToAction(nameof(Index));
        }

    }
}
