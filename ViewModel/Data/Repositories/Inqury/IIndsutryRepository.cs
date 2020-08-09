namespace ViewModel.Data.Repositories.Inqury
{
    public interface IIndsutryRepository
    {
        void Add(string name);
        void DeleteByIdAsync(int id);
        string GetIndustryNameByIdAsync(int? id);
    }
}