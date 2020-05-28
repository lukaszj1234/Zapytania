namespace ViewModel.Data.Repositories
{
    public interface IIndsutryRepository
    {
        void Add(string name);
        void DeleteByIdAsync(int id);
        string GetIndustryNameByIdAsync(int? id);
    }
}