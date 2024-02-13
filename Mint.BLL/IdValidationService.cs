using Interfaces.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mint.BLL
{
    public class IdValidationService
    {
        private readonly IApiService _apiService;

        public IdValidationService(IApiService apiService)
        {
            _apiService = apiService;
        }

        // Get list of all coins.

        // Create dictionary of IDs from the list of all coins.

        // Verify if the received ID is in the dictionary.

    }
}
