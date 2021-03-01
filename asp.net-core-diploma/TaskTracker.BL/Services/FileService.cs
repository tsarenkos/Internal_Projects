using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Models;
using TaskTracker.BL.Interfaces;
using TaskTracker.DAL.Models;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using TaskTracker.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Storage.Core.Entities;

namespace TaskTracker.BL.Services
{
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly OptionsForUploadFiles optionsForUploadFiles;
        private readonly ICheckUserService checkUserService;
        private readonly IHttpContextAccessor httpContext;
        public FileService(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, IOptions<OptionsForUploadFiles> optionsForUploadFiles, ICheckUserService checkUserService, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.optionsForUploadFiles = optionsForUploadFiles.Value;
            this.checkUserService = checkUserService;
            this.httpContext = httpContext;
        }

        public IEnumerable<FileModelBL> GetAll(int TaskId)
        {
            if (!HttpContextExtensions.IsAdmin(httpContext) && !checkUserService.AccessGrantedForUser(TaskId)) return null;

            var idlist = context.TasksFiles.Where(tf => tf.TaskId == TaskId).Select(tf=>tf.FileId);
            var list = context.Files.Where(t => idlist.Contains(t.Id));

            return list.Select(file => new FileModelBL()
            {
                Id = file.Id,
                FileName = file.FileName,
                ContentType = file.ContentType,
                Data = null // тело файла для списка не нужно
            });
        }
        private bool CheckAccessToFile(int id, out MyFile fl, out int TaskId)
        {
            fl = null;
            TaskId = -1;

            var TaskFile = context.TasksFiles.Where(tf => tf.FileId == id).FirstOrDefault();

            if (TaskFile == null) return false;
            TaskId = TaskFile.TaskId;

            if (!checkUserService.AccessGrantedForUser(TaskId)) return false;

            fl = context.Files.Find(id);

            return true;
        }

        public FileModelBL GetByFileName(int taskid, string filename)
        {
            if (!checkUserService.AccessGrantedForUser(taskid)) return null;

            var list = context.TasksFiles.Where(tf => tf.TaskId == taskid);
            if (!list.Any()) return null;

            var fl = context.Files.FirstOrDefault(fl =>fl.FileName==filename && list.Any(tf => tf.FileId == fl.Id));
            
            string path = $"\\{optionsForUploadFiles.FilderForFiles}\\{taskid}\\{filename}";

            return new FileModelBL()
            {
                Id = fl.Id,
                FileName = fl.FileName,
                ContentType = fl.ContentType,
                Data = File.ReadAllBytes(hostingEnvironment.ContentRootPath + path)
            };
        }

        public bool Create(int taskId, FileModelBL model)
        {
            string SafeFileName = SaveFile(taskId, model);

            MyFile fileForTask = new MyFile() { 
                FileName = SafeFileName, 
                ContentType = model.ContentType, 
                TaskFiles = new List<TaskFile>() { 
                    new TaskFile() { TaskId = taskId } } };

            context.Files.Add(fileForTask);
            context.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            if (!CheckAccessToFile(id, out MyFile fl, out int TaskId)) return false;

            string path = $"{hostingEnvironment.ContentRootPath}\\{optionsForUploadFiles.FilderForFiles}\\{TaskId}\\{fl.FileName}";

            File.Delete(path);

            context.Files.Remove(fl);
            context.SaveChanges();

            return true;
        }

        public bool DeleteForTask(int taskId)
        {
            if (!HttpContextExtensions.IsAdmin(httpContext) && !checkUserService.AccessGrantedForUser(taskId)) return false;

            var list = context.TasksFiles.Where(tf => tf.TaskId == taskId).Select(tf => tf.FileId).ToList();

            foreach (int id in list)
                context.Files.Remove(context.Files.Find(id));

            string path = $"{hostingEnvironment.ContentRootPath}\\{optionsForUploadFiles.FilderForFiles}\\{taskId}";
            if (Directory.Exists(path))
                Directory.Delete(path, true);

            return true;
        }

        private string SaveFile(int taskId, FileModelBL model)
        {
            string fullPath;

            string fileName = $"{Regex.Replace(Path.GetFileNameWithoutExtension(model.FileName), @"[^\u0000-\u007F]+", string.Empty)}{DateTime.Now.ToString(optionsForUploadFiles.FileNameMask)}{Path.GetExtension(model.FileName)}";
            string path = $"{hostingEnvironment.ContentRootPath}\\{optionsForUploadFiles.FilderForFiles}\\{taskId}\\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            fullPath = path + fileName;

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                fileStream.Write(model.Data, 0, model.Data.Length);
            }

            return fileName;
        }

    }
}
