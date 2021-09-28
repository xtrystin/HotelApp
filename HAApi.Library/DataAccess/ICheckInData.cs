using HAApi.Library.Models;
using HAApi.Library.Models.EFModels;

namespace HAApi.Library.DataAccess
{
    public interface ICheckInData
    {
        Payment SaveCheckInInfo(CheckIn checkInInfo);
        void DeleteLastCheckInCashierMade(string cashierId);
    }
}