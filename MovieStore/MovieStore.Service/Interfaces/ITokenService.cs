using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Service.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(string username, string password);
    }
}
