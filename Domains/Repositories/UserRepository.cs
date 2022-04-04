using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoCqrs.Domains.Entities;

namespace ToDoCqrs.Domains.Repositories
{
    public interface UserRepository
    {
        Task Save(UserEntity user);
        Task Update(string id, UserEntity user);
        Task Delete(string id);
        Task<UserEntity> GetById(string id);
        Task<UserEntity> GetByEmail(string email);
        Task<IEnumerable<UserEntity>> GetAll();
        
        
    }
}