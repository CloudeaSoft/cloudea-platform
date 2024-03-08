using Cloudea.Domain.Common.Shared;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Primitives;
using Cloudea.Infrastructure.Shared;

namespace Cloudea.Service.Forum.Domain.Entities;

/// <summary>
/// 论坛板块
/// </summary>
public sealed class ForumSection : BaseDataEntity, IAuditableEntity
{
    private ForumSection(string name, Guid masterId, string statement)
    {
        Id = Guid.NewGuid();
        MasterUserId = masterId;
        Name = name;
        Statement = statement;
        ClickCount = 0;
        TopicCount = 0;
    }

    private ForumSection() { }

    public Guid MasterUserId { get; private set; }

    public string Name { get; private set; }
    public string Statement { get; private set; }
    public long ClickCount { get; private set; }
    public long TopicCount { get; private set; }

    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    /// <summary>
    /// 实例化
    /// </summary>
    /// <param name="name">板块名字</param>
    /// <param name="masterId">板块管理人id</param>
    /// <param name="statement">简介</param>
    /// <returns></returns>
    public static ForumSection? Create(string name, Guid master, string statement = "")
    {
        if (string.IsNullOrEmpty(name)) return null;
        return new ForumSection(name, master, statement);
    }

    public void Update(string sectionName, string statement, Guid? masterId)
    {
        if (string.IsNullOrEmpty(sectionName)) {
            Name = sectionName;
        }
        if (string.IsNullOrEmpty(statement)) {
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