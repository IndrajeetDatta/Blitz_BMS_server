using BMS.Sql.Library;
using BMS.Sql.Library.Models;

namespace BMS.Sql.Library.Services
{
    public class NetWorkService: ModelServiceBase
    {
        public NetWorkService(BMSDbContext bmsDbContext) : base(bmsDbContext) { }
        
        public Network Get(int id)
        {
            return BMSDbContext.Networks.SingleOrDefault(x => x.Id == id);
        }
        public List<Network> GetByChargingStation(int chargingStationId)
        {
            List<Network> networkList = BMSDbContext.Networks.Where(x => x.ChargingStation.Id == chargingStationId).ToList();
            networkList = SortNetworks(networkList);
            return networkList;
        }

        public List<Network> SortNetworks(List<Network> networkList) 
        {
            if (networkList.Count >= 2 && networkList[0].Name == "eth1")
            {
                Network network = networkList[0];
                networkList[0] = networkList[1];
                networkList[1] = network;
            }
            if (networkList.Count < 2)
            {
                networkList.Add(new Network());
                if (networkList.Count < 2)
                    networkList.Add(new Network());
            }

            return networkList;
        }
    }
}
