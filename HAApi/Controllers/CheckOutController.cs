using HAApi.Library.DataAccess;
using HAApi.Library.Models;
using HAApi.Library.Models.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly ICheckOutData _checkOutData;

        public CheckOutController(ICheckOutData checkOutData)
        {
            _checkOutData = checkOutData;
        }

        // POST api/<CheckOutController>
        [HttpPost]
        [Authorize(Roles = "Cashier,Admin")]
        public void Post([FromBody] CheckOutDtoModel checkOutInfo)
        {
            _checkOutData.SaveCheckOutInfo(checkOutInfo);
        }
    }
}
