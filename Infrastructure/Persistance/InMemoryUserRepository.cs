using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoCqrs.Domains.Entities;
using ToDoCqrs.Domains.Repositories;

namespace ToDoCqrs.Infrastructure.Persistance
{
    public class InMemoryUserRepository : UserRepository
    {
        private List<UserEntity> _users;

        public InMemoryUserRepository()
        {
            _users = new List<UserEntity>();
        }

        public async Task Delete(string id)
        {
            _users = _users.Where(x => x.Id != id)
                           .ToList();

            await Task.CompletedTask;
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await Task.FromResult(_users);
        }

        public async Task<UserEntity> GetByEmail(string email)
        {
            UserEntity user = _users.Where(x => x.Email == email)
                                    .FirstOrDefault();

            return await Task.FromResult(user);
        }

        public async Task<UserEntity> GetById(string id)
        {
            UserEntity user = _users.Where(x => x.Id == id)
                                    .FirstOrDefault();

            return await Task.FromResult(user);
        }

        public async Task Save(UserEntity user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task Update(string id, UserEntity user)
        {
            int index = _users.FindIndex(m => m.Id == id);

            if (index >= 0)
                await Task.Run(() => _users[index] = user);
        }
    }
}