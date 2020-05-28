using ProjektInzynierski.DataAccess.Entities;
using System.Collections.Generic;

namespace Model
{
    public interface IFileManager
    {
        List<AddedFile> AddFile(string dstPath, int id, List<AddedFile> existingFile);
        void CopyFiles(List<string> paths, string destPath);
        void DeleteFile(string path);
        ReferenceOffer AddReferenceOffer(string dstPath, int inquiryId);
        List<Offer> AddOffer(string dstPath, int inquiryId, List<Offer> existingFiles);
    }
}