using Auth.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            return await _dbContext.Users.Where(x => x.Email.Equals(email) && x.Password.Equals(password)).FirstOrDefaultAsync();
        }
    }
}
