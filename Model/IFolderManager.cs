using ProjektInzynierski.DataAccess.Entities;
using System.Collections.Generic;

namespace Model
{
    public interface IFolderManager
    {
        string CreateNewInquiryFolder(string name);
        string CreateMainFolder();
        void DeleteFolder(string path);
        string CreateNewFolder(string path, string name);
        string RenameFolder(string directory, string newName);
        void AddToZipAndSend(string InquiryPath, List<AddedFile> paths);
    }
}