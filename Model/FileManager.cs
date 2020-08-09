using ProjektInzynierski.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Model
{
    public class FileManager : IFileManager
    {
        public List<AddedFile> AddFile(string dstPath, int inquiryId, List<AddedFile> existingFiles)
        {
            List<AddedFile> returnList = new List<AddedFile>();
            List<String> filePaths = new List<string>();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Wybierz plik";
            fileDialog.InitialDirectory = "";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            fileDialog.Multiselect = true;
            var openDialogCheck = fileDialog.ShowDialog();
            if (openDialogCheck == DialogResult.OK)
            {
                foreach (String filePath in fileDialog.FileNames)
                {
                    var newFilePath = Path.Combine(dstPath, Path.GetFileName(filePath));
                    if (!existingFiles.Exists(i => i.Path == newFilePath))
                    {
                        returnList.Add(new AddedFile() { InquiryId = inquiryId, Path = newFilePath, Name = Path.GetFileName(filePath) });
                        filePaths.Add(filePath);
                    }
                }
            }
            try
            {
                CopyFiles(filePaths, dstPath);
            }
            catch (Exception e)
            {
            }
            return returnList;
        }

        public List<String> GetAddedDrawingsPath()
        {
            List<string> filePaths = new List<string>();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Wybierz plik";
            fileDialog.InitialDirectory = "";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            fileDialog.Multiselect = true;
            var openDialogCheck = fileDialog.ShowDialog();
            if (openDialogCheck == DialogResult.OK)
            {
                foreach (String filePath in fileDialog.FileNames)
                {
                    filePaths.Add(filePath);
                }
            }
            return filePaths;
        }

        public string GetUpdateDrawingPath()
        {
            string filePath = null;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Wybierz plik";
            fileDialog.InitialDirectory = "";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            fileDialog.Multiselect = false;
            var openDialogCheck = fileDialog.ShowDialog();
            if (openDialogCheck == DialogResult.OK)
            {
                foreach (String item in fileDialog.FileNames)
                {
                    filePath= item;
                }
            }
            return filePath;
        }
        public void CopyFiles(List<string> paths, string destPath)
        {
            foreach (string path in paths)
            {
                string tmpPath = System.IO.Path.Combine(destPath, Path.GetFileName(path));
                    File.Copy(path, tmpPath);  
            }
        }

        public string CopyFiles(string path, string destPath)
        {
                string tmpPath = System.IO.Path.Combine(destPath, Path.GetFileName(path));
                File.Copy(path, tmpPath);
                return tmpPath;
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public ReferenceOffer AddReferenceOffer(string dstPath, int inquiryId)
        {
            ReferenceOffer newReferenceOffer= new ReferenceOffer();
            string newFilePath="";
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Wybierz plik";
            fileDialog.InitialDirectory = "";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Arkusze Kalkulacyjne|*.xls; *xlsx";
            var openDialogCheck = fileDialog.ShowDialog();
            if (fileDialog.FileNames.Count() != 0)
            {
                if (openDialogCheck == DialogResult.OK)
                {
                    foreach (String filePath in fileDialog.FileNames)
                    {
                        newFilePath = Path.Combine(dstPath, Path.GetFileName(filePath));
                        CopyFiles(filePath, dstPath);
                    }
                }
            }
            else
            {
                return null;
            }
            newReferenceOffer.Path = newFilePath;
            newReferenceOffer.Name = Path.GetFileName(newFilePath);
            newReferenceOffer.InquiryId = inquiryId;
            return newReferenceOffer;
        }

        public List<Offer> AddOffer(string dstPath, int inquiryId, List<Offer> existingFiles)
        {
            List<Offer> returnList = new List<Offer>();
            List<String> filePaths = new List<string>();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Wybierz plik";
            fileDialog.InitialDirectory = "";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Arkusze Kalkulacyjne|*.xls; *xlsx";
            var openDialogCheck = fileDialog.ShowDialog();
            if (fileDialog.FileNames.Count() != 0)
            {
                if (openDialogCheck == DialogResult.OK)
                {
                    foreach (String filePath in fileDialog.FileNames)
                    {
                        var newFilePath = Path.Combine(dstPath, Path.GetFileName(filePath));
                        if (!existingFiles.Exists(i => i.Path == newFilePath))
                        {
                            returnList.Add(new Offer() { InquiryId = inquiryId, Path = newFilePath, Name = Path.GetFileName(filePath) });
                            filePaths.Add(filePath);
                        }
                    }
                }
                CopyFiles(filePaths, dstPath);
            }
            return returnList;
        }
    }
}
