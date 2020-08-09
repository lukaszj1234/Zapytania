using ProjektInzynierski.DataAccess.Entities;
using ProjektInzynierski.DataAccess.InquiryContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Data.Repositories.Documentation
{
    public class OutOfDateDrwaingRepository : IOutOfDateDrwaingRepository
    {
        private InquiryContext _context;

        public OutOfDateDrwaingRepository(InquiryContext context)
        {
            _context = context;
        }
        public void Add(DrawingOutOfDate file)
        {
            _context.DrawingsOutOfDate.Add(file);
            _context.SaveChanges();
        }

        public List<DrawingOutOfDate> GettAllFilesByDrawingId(int drawingID)
        {
            return _context.DrawingsOutOfDate.Where(f => f.DrawingId == drawingID).ToList();
        }
        public async Task<DrawingOutOfDate> GetByIdAsync(int drawingId)
        {
            return await _context.DrawingsOutOfDate.FindAsync(drawingId);
        }

        public void Remove(DrawingOutOfDate model)
        {
            _context.DrawingsOutOfDate.Remove(model);
        }

        public void RemoveById(int id)
        {
            var file = _context.DrawingsOutOfDate.Single(i => i.Id == id);
            _context.DrawingsOutOfDate.Remove(file);
            _context.SaveChanges();
        }

        public void RemoveByDrawingId(int id)
        {
            var list = _context.DrawingsOutOfDate.Where(f => f.DrawingId == id).ToList();
            foreach (var item in list)
            {
                _context.DrawingsOutOfDate.Remove(item);
            }
            _context.SaveChanges();
        }
        public string GetFilePathById(int fileid)
        {
            var file = _context.DrawingsOutOfDate.Single(i => i.Id == fileid);
            return file.Path;
        }
        public void ChangePaths(List<Drawing> list)
        {
            foreach (var item in list)
            {
                var outOfDateDrawingsList = _context.DrawingsOutOfDate.Where(f => f.DrawingId == item.Id).ToList();
                foreach (var i in outOfDateDrawingsList)
                {
                    string newPath = Path.Combine(Path.GetDirectoryName(item.Path),"Archiwum",i.Name);
                    i.Path = newPath;
                }
            }
            _context.SaveChanges();
        }
    }
}
