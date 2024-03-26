using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.Identity.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Cloudea.Domain.Identity.Entities;

public class UserProfile : Entity, IAuditableEntity
{
    private UserProfile(
        Guid id,
        string userName,
        string displayName,
        string? signature,
        string? avatarUrl,
        string? coverImageUrl,
        int leaves)
        : base(id)
    {
        UserName = userName;
        DisplayName = displayName;
        Signature = signature;
        AvatarUrl = avatarUrl;
        CoverImageUrl = coverImageUrl;
        Leaves = leaves;
    }

    private UserProfile() { }

    [MaxLength(100)]
    public string UserName { get; set; }

    [MaxLength(256)]
    public string DisplayName { get; set; }

    [MaxLength(500)]
    public string? Signature { get; set; }

    [MaxLength(256)]
    public string? AvatarUrl { get; set; }

    [MaxLength(256)]
    public string? CoverImageUrl { get; set; }

    public int Leaves { get; set; }

    public DateTimeOffset CreatedOnUtc { get; set; }

    public DateTimeOffset? ModifiedOnUtc { get; set; }

    public static UserProfile Create(
        User user,
        DisplayName displayName,
        string? signature = null,
        string? avatarUrl = null,
        string? coverImageUrl = null,
        int leaves = 100)
    {
        return new UserProfile(
            user.Id,
            user.UserName,
            displayName.Value,
            signature,
            avatarUrl,
            coverImageUrl,
            leaves
        );
    }
}