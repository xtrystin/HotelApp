using HAApi.Library.Models;
using HAApi.Library.Models.DtoModels;

namespace HAApi.Library.DataAccess
{
    public interface ICheckOutData
    {
        void SaveCheckOutInfo(CheckOutDtoModel checkOutData);
    }
}