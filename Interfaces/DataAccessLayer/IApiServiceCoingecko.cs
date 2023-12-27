using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DataAccessLayer
{
    public interface IApiServiceCoingecko
    {
        Task<List<Coin>> GetAllCoinsAsync();
    }
}
