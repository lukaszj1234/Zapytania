using ProjektInzynierski.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViewModel.Data.Repositories.Documentation
{
    public interface IDrawingIndustryRepository
    {
        void Add(DrawingIndustry file);
        void Add(string name,string path);
        Task<DrawingIndustry> GetByIdAsync(int IndustryId);
        Task<List<DrawingIndustry>> GettAllAsync();
        bool HasChanges();
        void Remove(DrawingIndustry model);
        void RemoveById(int id);
        Task SaveAsync();
        List<DrawingIndustry> GetByNameAsync(string name);
    }
}