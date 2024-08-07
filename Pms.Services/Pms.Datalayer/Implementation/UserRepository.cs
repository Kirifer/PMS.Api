using Pms.Datalayer.Entities;
using Pms.Datalayer.Interface;

namespace Pms.Datalayer.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(PmsDbContext context) : base(context)
        {

        }
    }
}
