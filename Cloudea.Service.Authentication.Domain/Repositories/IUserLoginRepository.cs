namespace Cloudea.Service.Auth.Domain.Repositories
{
    public interface IUserLoginRepository
    {
        void RecordLogin(Guid userId);
    }
}