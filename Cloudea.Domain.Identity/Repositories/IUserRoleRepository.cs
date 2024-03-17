namespace Cloudea.Domain.Identity.Repositories
{
    public interface IUserRoleRepository
    {
        Task<List<int>> GetRoleListByUserId(Guid userId);
    }
}
