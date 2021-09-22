using HAApi.Library.Models;

namespace HAApi.Library.DataAccess
{
    public interface ICheckInData
    {
        Payment SaveCheckInInfo(CheckIn checkInInfo);
        void DeleteLastCheckInCashierMade(string cashierId);
    }
}