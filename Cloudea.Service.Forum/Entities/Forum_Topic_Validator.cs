using FluentValidation;

namespace Cloudea.Entity.Forum;
/// <summary>
/// Forum_Topic实体的验证器
/// </summary>
public class Forum_Topic_Validator : AbstractValidator<Forum_Topic> {
    public Forum_Topic_Validator() {
        RuleFor(x => x.Title).NotEmpty().Length(1, 15).WithMessage("标题长度至少1个字符，至多15个字符");
        RuleFor(x => x.Content).NotEmpty().WithMessage("内容不可为空");
    }
}