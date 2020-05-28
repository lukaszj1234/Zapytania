using System.Collections.Generic;

namespace Model
{
    public interface IXlsManager
    {
        string CompareOffers(string referenceOfferPath, List<string> offersPaths, string MainFolderPath,
             List<int> collumns);
    }
}