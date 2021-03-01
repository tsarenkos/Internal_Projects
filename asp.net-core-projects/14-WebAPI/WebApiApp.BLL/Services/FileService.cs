using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApiApp.BLL.Interfaces;

namespace WebApiApp.BLL.Services
{
    public class FileService : IFileService
    {
        private const int pageSize = 5;
        public List<string> GetFilesList(string path)
        {
            if (path == null)
            {
                return null;
            }

            List<string> files = Directory.GetFiles(path).ToList<string>();            

            string[] subDirectories = Directory.GetDirectories(path);
            if (subDirectories != null)
            {
                foreach (string dir in subDirectories)
                {
                    List<string> filesInSubDir = GetFilesList(dir);
                    files.AddRange(filesInSubDir);
                }
            }
            return files;
        }

        public List<string> GetFilesOnPage(string path, int pageNumber)
        {
            if(path == null || pageNumber <= 0)
            {
                return null;
            }

            List<string> files = GetFilesList(path);

            if (files == null || pageNumber > (int)Math.Ceiling(files.Count/(double)pageSize))
            {
                return null;
            }
            
            return files.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
