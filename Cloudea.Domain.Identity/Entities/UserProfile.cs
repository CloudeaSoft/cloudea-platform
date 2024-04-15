using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.Identity.DomainEvents;
using Cloudea.Domain.Identity.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Cloudea.Domain.Identity.Entities;

public class UserProfile : AggregateRoot, IAuditableEntity
{
    private UserProfile(
        Guid id,
        string userName,
        string displayName,
        string? signature,
        Uri? avatarUrl,
        Uri? coverImageUrl,
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
    public string UserName { get; private set; }

    [MaxLength(256)]
    public string DisplayName { get; private set; }

    [MaxLength(500)]
    public string? Signature { get; private set; }

    [MaxLength(256)]
    public Uri? AvatarUrl { get; private set; }

    [MaxLength(256)]
    public Uri? CoverImageUrl { get; private set; }

    public int Leaves { get; private set; }

    public DateTimeOffset CreatedOnUtc { get; private set; }

    public DateTimeOffset? ModifiedOnUtc { get; private set; }

    public static UserProfile Create(
        User user,
        DisplayName displayName,
        string? signature = null,
        Uri? avatarUrl = null,
        Uri? coverImageUrl = null,
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

    public UserProfile SetAvatar(Uri avatarUrl)
    {
        Guid eventId = Guid.NewGuid();
        RaiseDomainEvent(new UserProfileAvatarUpdatedDomainEvent(eventId, Id, AvatarUrl));

        AvatarUrl = avatarUrl;

        return this;
    }
}