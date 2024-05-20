using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.DomainEvents;

namespace Cloudea.Domain.Forum.Entities;

/// <summary>
/// 论坛板块
/// </summary>
public sealed class ForumSection : AggregateRoot, IAuditableEntity
{
    private ForumSection(Guid id, string name, Guid masterId, string statement) : base(id)
    {
        MasterUserId = masterId;
        Name = name;
        Statement = statement;
        ClickCount = 0;
        TopicCount = 0;
    }

    private ForumSection() { }

    public Guid MasterUserId { get; private set; }

    public string Name { get; private set; } = string.Empty;
    public string Statement { get; private set; } = string.Empty;
    public long ClickCount { get; private set; }
    public long TopicCount { get; private set; }

    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    /// <summary>
    /// Create Factory
    /// </summary>
    /// <param name="name">板块名字</param>
    /// <param name="master">板块管理人id</param>
    /// <param name="statement">简介</param>
    /// <returns></returns>
    public static ForumSection? Create(string name, Guid master, string statement = "")
    {
        if (string.IsNullOrWhiteSpace(name)) {
            return null;
        }
        if (master == Guid.Empty) {
            return null;
        }

        var sectionId = Guid.NewGuid();
        return new ForumSection(sectionId, name, master, statement);
    }

    /// <summary>
    /// Update
    /// </summary>
    /// <param name="sectionName"></param>
    /// <param name="statement"></param>
    /// <param name="masterId"></param>
    public void Update(string sectionName, string statement, Guid? masterId)
    {
        if (string.IsNullOrWhiteSpace(sectionName)) {
            Name = sectionName;
        }
        if (string.IsNullOrWhiteSpace(statement)) {
            Statement = statement;
        }
        if (masterId is not null && masterId != Guid.Empty) {
            MasterUserId = (Guid)masterId;
        }
    }

    public void IncreaseTopicCount(int num = 1)
    {
        if (num < 0) {
            return;
        }

        TopicCount += num;
    }

    public void DecreaseTopicCount(int num = 1)
    {
        if (num < 0) {
            return;
        }

        TopicCount -= num;

        if (TopicCount < 0) {
            TopicCount = 0;
        }
    }

    /// <summary>
    /// Create a new post in this section
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public ForumPost? AddPost(Guid userId, string title, string content)
    {
        var res = ForumPost.Create(
            userId,
            this,
            title,
            content);

        if (res is null) {
            return null;
        }

        var eventId = Guid.NewGuid();

        RaiseDomainEvent(new PostCreatedDomainEvent(eventId, res.Id, Id));

        return res;
    }
}

public sealed class ForumSectionStatement(string value) : ValueObject
{
    public string Value { get; } = value;

    public static Result<ForumSectionStatement> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) {
            return Errors.Empty;
        }

        return new ForumSectionStatement(email);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public class Errors
    {
        public static readonly Error Empty = new(
            "ForumSection.Statement.Empty",
            "Statement is empty");
    }
}