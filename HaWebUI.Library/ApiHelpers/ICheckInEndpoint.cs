using HaWebUI.Library.Models;
using System.Threading.Tasks;

namespace HaWebUI.Library.ApiHelpers
{
    public interface ICheckInEndpoint
    {
        Task<PaymentModel> PostCheckInInfo(CheckInModel checkInInfo);
        Task DeleteLastCheckInCashierMade(string cashierId);
    }
}