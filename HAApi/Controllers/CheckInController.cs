using HAApi.Library.DataAccess;
using HAApi.Library.Models;
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
    public class CheckInController : ControllerBase
    {
        private readonly ICheckInData _checkInData;

        public CheckInController(ICheckInData checkInData)
        {
            _checkInData = checkInData;
        }

        // POST api/<CheckInController>
        [HttpPost]
        [Authorize(Roles = "Cashier,Admin")]
        public Payment Post([FromBody] CheckIn checkInInfo)
        {
            return _checkInData.SaveCheckInInfo(checkInInfo);
        }

        // POST api/<CheckInController>/DeleteLastCheckIn
        /// <summary>
        /// Delete last CheckIn record which was made by the cashier. 
        /// Record cannot be older than 2 hours, becaouse of security reasons.
        /// </summary>
        /// <param name="cashierId"></param>
        [HttpPost("DeleteLastCheckIn")]
        [Authorize(Roles = "Cashier,Admin")]
        public void DeleteLastCheckIn([FromBody] string cashierId)
        {
            _checkInData.DeleteLastCheckInCashierMade(cashierId);
        }
    }
}
