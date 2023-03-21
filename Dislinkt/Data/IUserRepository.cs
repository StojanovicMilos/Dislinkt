using Dislinkt.Models;

namespace Dislinkt.Data
{
    public interface IUserRepository
    {
        public IEnumerable<ApplicationUser> GetAll();
    }
}