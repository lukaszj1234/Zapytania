using System.Collections.Generic;
using System.Threading.Tasks;
using ProjektInzynierski.DataAccess.Entities;

namespace ViewModel.Data.Repositories.Inqury
{
   public interface IAddedFilesRepository
    {
        void Add(AddedFile file);
        Task<AddedFile> GetByIdAsync(int fileId);
        bool HasChanges();
        void Remove(AddedFile model);
        List<AddedFile> GettAllFilesByInquiryId(int inquiryID);
        Task SaveAsync();
        void RemoveByFileId(int fileId);
        string GetFilePathById(int id);
        void RemoveByInquiryId(int id);
        void ChangePath(string newPath, int id);
    }
}