using FluentValidation;

namespace Cloudea.Service.Forum.Domain.Models {
    public class PostSectionRequest_Validator:AbstractValidator<PostSectionRequest> {
        public PostSectionRequest_Validator()
        {
            RuleFor(x => x.SectionName).NotEmpty().Length(1, 10); ;
            RuleFor(x => x.Statement).NotEmpty();
        }
    }
}
