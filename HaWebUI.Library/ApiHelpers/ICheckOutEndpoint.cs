using HaWebUI.Library.Models;
using System.Threading.Tasks;

namespace HaWebUI.Library.ApiHelpers
{
    public interface ICheckOutEndpoint
    {
        Task PostCheckOutInfo(CheckOutModel checkOutInfo);
    }
}