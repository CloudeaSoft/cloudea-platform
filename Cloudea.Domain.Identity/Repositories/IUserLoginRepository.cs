namespace Cloudea.Domain.Identity.Repositories
{
    public interface IUserLoginRepository
    {
        void RecordLogin(Guid userId);
    }
}