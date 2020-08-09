using ProjektInzynierski.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViewModel.Data.Repositories.Documentation
{
    public interface IDrawingsRepository
    {
        void Add(Drawing file);
        Task<Drawing> GetByIdAsync(int drawingId);
        string GetFilePathById(int fileid);
        List<Drawing> GettAllFilesByIndustryId(int industryID);
        bool HasChanges();
        void Remove(Drawing model);
        void RemoveById(int id);
        List<Drawing> RemoveByIndustryId(int id);
        Task SaveAsync();
        List<Drawing> ChangePath(string newPath, int id);
    }
}