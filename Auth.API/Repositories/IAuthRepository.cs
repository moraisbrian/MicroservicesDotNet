using Auth.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Authenticate(string email, string password);
    }
}
