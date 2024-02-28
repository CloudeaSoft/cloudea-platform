using Cloudea.Application.Contracts;
using Cloudea.Infrastructure.Database;
using Cloudea.Persistence.Repositories;
using Cloudea.Service.Forum;
using Cloudea.Service.Forum.Domain.Entities;
using Cloudea.Service.Forum.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Xunit.Abstractions;

namespace TestProject_Forum.Products;
public class ForumService_Test
{
    private readonly ForumSectionRepository _forumSectionRepository;
    private readonly ITestOutputHelper _outputHelper;

    public ForumService_Test(ITestOutputHelper outputHelper)
    {
        var services = new ServiceCollection();
        services.AddDataBaseDefault(FreeSql.DataType.MySql, "Server=localhost;Port=3306;Database=test;Uid=root;Pwd=123456;");
        services.AddScoped<ForumSectionRepository, ForumSectionRepository>();
        var ini = new ModuleInitializer();
        ini.Initialize(services);
        var provider = services.BuildServiceProvider();
        _forumSectionRepository = provider.GetRequiredService<ForumSectionRepository>();
        _outputHelper = outputHelper;
    }

    /// <summary>
    /// BDD写法
    ///     Given   前提条件
    ///     When    当前操作
    ///     Then    期待结果
    /// </summary>
    /// 
    /// 测试账号的id => Guid.Parse("8fa2c964-c7e7-45f9-af89-acb914efc7d8") 
    /// 测试主题的id => Guid.Parse("660c062f-e523-4987-87f2-6e2e02556390") 

    [Fact(DisplayName = "创建Section时，若参数错误（String.IsNullOrEmpty)，则返回Null")]
    public void Test_Create_When_Set_Name_Empty_String_Then_Return_Null()
    {
        ForumSection? newSection = ForumSection.Create("", Guid.Parse("8fa2c964-c7e7-45f9-af89-acb914efc7d8"));
        Assert.Null(newSection);
    }

    [Fact(DisplayName = "创建Section时，若参数错误（Guid.Empty)，则返回Null")]
    public void Test_Create_When_Set_Master_Empty_Guid_Then_Return_Null()
    {
        ForumSection? newSection = ForumSection.Create("test1", Guid.Empty);
        Assert.Null(newSection);
    }

    [Fact(DisplayName = "更新Section时，正常完成")]
    public void Test_UpdateSection()
    {
        var section = ForumSection.Create("Old_Name", Guid.NewGuid(), "Old_Statement");
        if (section is null) {
            return;
        }
        _outputHelper.WriteLine(JsonSerializer.Serialize(section));

        var newMasterId = Guid.NewGuid();
        var updateRequest = new UpdateSectionRequest() {
            Id = section.Id,
            Name = "New_Name",
            MasterId = newMasterId,
            Statement = "New_Statement"
        };

        section.Update(
            updateRequest.Name,
            updateRequest.Statement,
            updateRequest.MasterId);
        _outputHelper.WriteLine(JsonSerializer.Serialize(section));

        var res = section.Id == section.Id &&
            section.Name == "New_Name" &&
            section.MasterUserId == newMasterId &&
            section.Statement == "New_Statement";
        Assert.True(res);
    }

    [Fact(DisplayName = "更新Section时，若两个参数的ID不同，则返回原值")]
    public void Test_UpdateSection_When_Id_Not_Equal_Then_No_Changes()
    {
        var section = ForumSection.Create("Old_Name", Guid.NewGuid(), "Old_Statement");
        if (section is null) {
            return;
        }
        var oldMasterId = section.MasterUserId;
        _outputHelper.WriteLine(JsonSerializer.Serialize(section));

        var newMasterId = Guid.NewGuid();
        var updateRequest = new UpdateSectionRequest() {
            Id = Guid.NewGuid(),
            Name = "New_Name",
            MasterId = newMasterId,
            Statement = "New_Statement"
        };

        section.Update(
            updateRequest.Name,
            updateRequest.Statement,
            updateRequest.MasterId);
        _outputHelper.WriteLine(JsonSerializer.Serialize(section));

        var res = section.Id == section.Id &&
            section.Name == "Old_Name" &&
            section.MasterUserId == oldMasterId &&
            section.Statement == "Old_Statement";
        Assert.True(res);
    }

    [Fact(DisplayName = "创建Topic时，正常完成")]
    public void Test_CreateTopic_When_Right_Argument_Then_Return_Forum_Topic()
    {
        var request = new CreateTopicRequest() {
            userId = Guid.Parse("8fa2c964-c7e7-45f9-af89-acb914efc7d8"),
            sectionId = Guid.Parse("660c062f-e523-4987-87f2-6e2e02556390"),
            title = "测试内容1",
            content = "测试内容"
        };
        var newTopic = ForumPost.Create(
                request.userId,
                request.sectionId,
                request.title,
                request.content);
        Assert.IsType<ForumSection>(newTopic);
    }
}


