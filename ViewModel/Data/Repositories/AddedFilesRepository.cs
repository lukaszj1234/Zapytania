using ProjektInzynierski.DataAccess.Entities;
using ProjektInzynierski.DataAccess.InquiryContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Data.Repositories;

namespace ViewModel.Data.Repositories
{
    public class AddedFilesRepository : IAddedFilesRepository
    {
        private InquiryContext _context;

        public AddedFilesRepository(InquiryContext context)
        {
            _context = context;
        }

        public void Add(AddedFile file)
        {
            _context.Files.Add(file);
        }

        public List<AddedFile> GettAllFilesByInquiryId(int inquiryID)
        {
            return _context.Files.Where(f => f.InquiryId == inquiryID).ToList();
        }
        public async Task<AddedFile> GetByIdAsync(int fileId)
        {
            return await _context.Files.FindAsync(fileId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(AddedFile model)
        {
            _context.Files.Remove(model);
        }

        public void RemoveByFileId(int fileId)
        {
            var file = _context.Files.Single(i => i.Id == fileId);
            _context.Files.Remove(file);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void RemoveByInquiryId(int id)
        { 
            var list= _context.Files.Where(f => f.InquiryId == id).ToList();
            foreach (var item in list)
            {
                _context.Files.Remove(item);
            }
            _context.SaveChanges();
        }
        public string GetFilePathById(int fileid)
        {
            var file = _context.Files.Single(i => i.Id == fileid);
            return file.Path;
        }

        public void ChangePath(string newPath, int id)
        {
           var list = _context.Files.Where(f => f.InquiryId == id).ToList();
            foreach (var item in list)
            {
                item.Path = Path.Combine(newPath, "Pliki", item.Name);
            }
            _context.SaveChanges();
        }
    }
}

