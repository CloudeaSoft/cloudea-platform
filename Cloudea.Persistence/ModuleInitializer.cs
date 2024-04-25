using Cloudea.Domain.Common;
using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.File.Repositories;
using Cloudea.Domain.Forum.Repositories;
using Cloudea.Domain.Forum.Repositories.Recommend;
using Cloudea.Domain.Identity.Repositories;
using Cloudea.Persistence.Repositories.File;
using Cloudea.Persistence.Repositories.Forum;
using Cloudea.Persistence.Repositories.Forum.Recommend;
using Cloudea.Persistence.Repositories.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Persistence
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IFileRepository, FileRepository>();

            services.AddScoped<IForumSectionRepository, ForumSectionRepository>();
            services.AddScoped<IForumPostRepository, ForumPostRepository>();
            services.AddScoped<IForumPostUserLikeRepository, ForumPostUserLikeRepository>();
            services.AddScoped<IForumPostUserHistoryRepository, ForumPostUserHistoryRepository>();
            services.AddScoped<IForumPostUserFavoriteRepository, ForumPostUserFavoriteRepository>();
            services.AddScoped<IForumReplyRepository, ForumReplyRepository>();
            services.AddScoped<IForumCommentRepository, ForumCommentRepository>();
            services.AddScoped<IUserPostInterestRepository, UserPostInterestRepository>();
            services.AddScoped<IUserSimilarityRepository, UserSimilarityRepository>();
            services.AddScoped<IPostSimilarityRepository, PostSimilarityRepository>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserLoginRepository, UserLoginRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
        }
    }
}
