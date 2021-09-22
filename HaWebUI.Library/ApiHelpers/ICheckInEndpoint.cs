using HaWebUI.Library.Models;
using System.Threading.Tasks;

namespace HaWebUI.Library.ApiHelpers
{
    public interface ICheckInEndpoint
    {
        Task<PaymentModel> PostCheckInInfo(string token, CheckInModel checkInInfo);
        Task DeleteLastCheckInCashierMade(string token, string cashierId);
    }
}