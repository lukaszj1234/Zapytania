using ProjektInzynierski.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViewModel.Data.Repositories.Documentation
{
    public interface IOutOfDateDrwaingRepository
    {
        void Add(DrawingOutOfDate file);
        void ChangePaths(List<Drawing> list);
        Task<DrawingOutOfDate> GetByIdAsync(int drawingId);
        string GetFilePathById(int fileid);
        List<DrawingOutOfDate> GettAllFilesByDrawingId(int drawingID);
        void Remove(DrawingOutOfDate model);
        void RemoveByDrawingId(int id);
        void RemoveById(int id);
    }
}