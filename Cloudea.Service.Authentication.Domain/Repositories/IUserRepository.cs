using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Auth.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cloudea.Service.Auth.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CheckUserRegisteredAsync([EmailAddress] string email);

        Task<bool> CheckPassword(Guid id, string password);
        Task<Guid> GetUserIdByUserName(string username);
        Task<Guid> GetUserIdByEmail([EmailAddress] string email);
        Task<Result<User>> GetUser(Guid id);
        Task<bool> CheckUserUnenableOrDeleted(Guid id);
    }
}
