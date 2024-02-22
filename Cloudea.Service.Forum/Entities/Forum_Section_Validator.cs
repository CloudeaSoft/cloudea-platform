using FluentValidation;

namespace Cloudea.Entity.Forum {
    public class Forum_Section_Validator : AbstractValidator<Forum_Section> {
        public Forum_Section_Validator() {
            RuleFor(x => x.Name).NotEmpty().Length(1, 10).WithMessage("标题长度至少1个字符，至多10个字符");
            RuleFor(x => x.Statement).NotEmpty().WithMessage("内容不可为空");
        }
    }
}
