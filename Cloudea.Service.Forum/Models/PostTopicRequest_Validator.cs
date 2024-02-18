using FluentValidation;

namespace Cloudea.Service.Forum.Domain.Models
{
    public class PostTopicRequest_Validator : AbstractValidator<PostTopicRequest>
    {
        public PostTopicRequest_Validator()
        {
            RuleFor(x => x.title).NotEmpty().Length(1, 20);
            RuleFor(x => x.content).NotEmpty();
        }
    }
}
