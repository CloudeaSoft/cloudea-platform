using Cloudea.Domain.Forum.Entities.Recommend;

namespace Cloudea.Domain.Forum.Repositories.Recommend
{
    public interface IUserPostInterestRepository
    {
        void Add(UserPostInterest userPostInterest);
    }
}
