using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAApi.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        private readonly IConfiguration _config;

        public ConfigHelper(IConfiguration config)
        {
            _config = config;
        }
        public decimal GetTaxRate()
        {
            string taxRate = _config["TaxRate"];

            bool isValidTaxRate = Decimal.TryParse(taxRate, out decimal output);

            if (isValidTaxRate == false)
            {
                throw new Exception("The tax rate is not set up properly");
            }

            output = output / 100M;

            return output;
        }
    }
}
