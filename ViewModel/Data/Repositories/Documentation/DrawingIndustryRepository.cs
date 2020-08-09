using ProjektInzynierski.DataAccess.Entities;
using ProjektInzynierski.DataAccess.InquiryContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Data.Repositories.Documentation
{
    public class DrawingIndustryRepository : IDrawingIndustryRepository
    {
        private InquiryContext _context;

        public DrawingIndustryRepository(InquiryContext context)
        {
            _context = context;
        }
        public void Add(DrawingIndustry file)
        {
            _context.DrawingIndustries.Add(file);
        }

        public void Add(string name, string folderPath)
        {
            DrawingIndustry industry = new DrawingIndustry() { Name = name, FolderPath = folderPath, LastUpdate = DateTime.Now.ToString() };
            _context.DrawingIndustries.Add(industry);
            _context.SaveChanges();
        }

        public List<DrawingIndustry> GetByNameAsync(string name)
        {
            return _context.DrawingIndustries.Where(i => i.Name == name).ToList();
        }

        public Task<List<DrawingIndustry>> GettAllAsync()
        {
            return  _context.DrawingIndustries.ToListAsync();
        }
        public async Task<DrawingIndustry> GetByIdAsync(int IndustryId)
        {
            return await _context.DrawingIndustries.FindAsync(IndustryId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(DrawingIndustry model)
        {
            _context.DrawingIndustries.Remove(model);
        }

        public void RemoveById(int id)
        {
            var file = _context.DrawingIndustries.Single(i => i.Id == id);
            _context.DrawingIndustries.Remove(file);
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

