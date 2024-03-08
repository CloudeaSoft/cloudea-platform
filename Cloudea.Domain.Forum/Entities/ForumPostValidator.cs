using FluentValidation;

namespace Cloudea.Service.Forum.Domain.Entities;
/// <summary>
/// ForumPost验证器
/// </summary>
public class ForumPostValidator : AbstractValidator<ForumPost>
{
    public ForumPostValidator()
    {
        RuleFor(x => x.Title).NotEmpty().Length(1, 15).WithMessage("标题长度至少1个字符，至多15个字符");
        RuleFor(x => x.Content).NotEmpty().WithMessage("内容不可为空");
    }
}