namespace Cloudea.Service.Auth.Domain.Abstractions
{
    public interface IUserLoginRepository
    {
        void RecordLogin(Guid userId);
    }
}