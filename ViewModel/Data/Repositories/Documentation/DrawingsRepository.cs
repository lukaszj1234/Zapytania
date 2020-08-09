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
    public class DrawingsRepository : IDrawingsRepository
    {
        private InquiryContext _context;

        public DrawingsRepository(InquiryContext context)
        {
            _context = context;
        }
        public void Add(Drawing file)
        {
            file.AddDate = DateTime.Now.ToString();
            file.LastUpdateDate = DateTime.Now.ToString();
            _context.Drawings.Add(file);
            _context.SaveChanges();
        }

        public List<Drawing> GettAllFilesByIndustryId(int industryID)
        {
            return _context.Drawings.Where(f => f.IndustryId == industryID).ToList();
        }
        public async Task<Drawing> GetByIdAsync(int drawingId)
        {
            return await _context.Drawings.FindAsync(drawingId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(Drawing model)
        {
            _context.Drawings.Remove(model);
        }

        public void RemoveById(int id)
        {
            var file = _context.Drawings.Single(i => i.Id == id);
            _context.Drawings.Remove(file);
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public List<Drawing> RemoveByIndustryId(int id)
        {
            var list = _context.Drawings.Where(f => f.IndustryId == id).ToList();
            foreach (var item in list)
            {
                _context.Drawings.Remove(item);
            }
            _context.SaveChanges();
            return list;
        }
        public string GetFilePathById(int fileid)
        {
            var file = _context.Drawings.Single(i => i.Id == fileid);
            return file.Path;
        }
        public List<Drawing> ChangePath(string newPath, int id)
        {
            var list = _context.Drawings.Where(f => f.IndustryId == id).ToList();
            List<Drawing> returnList = new List<Drawing>();
            foreach (var item in list)
            { 
                var directorryName = Path.GetFileName(Path.GetDirectoryName(item.Path));
                item.Path = Path.Combine(newPath, directorryName, Path.GetFileName(item.Path));
                returnList.Add(item);
            }
            _context.SaveChanges();
            return returnList;
        }
    }
}

