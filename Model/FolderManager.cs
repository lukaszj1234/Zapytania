using ProjektInzynierski.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    public class FolderManager : IFolderManager
    {
        private string GetFilePath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();
        }

        public string CreateNewInquiryFolder(string name)
        {
            string pathString = System.IO.Path.Combine(GetFilePath(), "Zapytania Ofertowe",name);
                System.IO.Directory.CreateDirectory(pathString);
            return pathString;
        }

        public string CreateMainDocumentationFolder()
        {
            string pathString = System.IO.Path.Combine(GetFilePath(), "Dokumentacja");
            if (Directory.Exists(pathString) == false)
            {
                System.IO.Directory.CreateDirectory(pathString);
            }
            return pathString;
        }

        public string CreateNewFolder(string path, string name)
        {
            string pathString = System.IO.Path.Combine(path, name);
            System.IO.Directory.CreateDirectory(pathString);
            return pathString;
        }

        public string CreateMainFolder()
        {
            string pathString = System.IO.Path.Combine(GetFilePath(), "Zapytania Ofertowe");
            if (Directory.Exists(pathString) == true)
            {
                return pathString;
            }
            else
            {
                System.IO.Directory.CreateDirectory(pathString);
                return pathString;
            }
        }

        public void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        public string RenameFolder(string directory, string newName)
        {
            var parentDir = Directory.GetParent(directory).ToString();
            var newPath = Path.Combine(parentDir, newName);
            Directory.Move(directory, newPath);
            return newPath;
        }
        public void AddToZipAndSend(string InquiryPath,List<AddedFile> paths)
        {
            var folderName = Path.GetFileName(InquiryPath);
            folderName += ".zip";
            string newFilePath = Path.Combine(InquiryPath, folderName);
            if (File.Exists(newFilePath))
            {
                File.Delete(newFilePath);
            }
            if (paths.Count != 0)
            {
                using (ZipArchive archive = ZipFile.Open(newFilePath, ZipArchiveMode.Create))
                {
                    foreach (var item in paths)
                    {
                        archive.CreateEntryFromFile(item.Path, Path.GetFileName(item.Path));
                    }
                }
            } 
        }
        
    }
}
