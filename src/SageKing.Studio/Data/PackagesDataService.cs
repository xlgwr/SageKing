using SageKingIceRpc;

namespace SageKing.Studio.Data
{
    public class PackagesDataService
    {
        public ConcurrentDictionary<string, List<StreamPackage[]>> dataDic;
        public PackagesDataService()
        {
            dataDic = new ConcurrentDictionary<string, List<StreamPackage[]>>();
        }
    }
}
