using System.Collections.Generic;


namespace WebApiApp.BLL.Interfaces
{
    public interface IFileService
    {
        List<string> GetFilesList(string path);
        List<string> GetFilesOnPage(string path, int pageNumber);
    }
}
