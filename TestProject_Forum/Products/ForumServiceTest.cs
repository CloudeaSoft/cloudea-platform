using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Forum;
using Cloudea.Service.Forum.Domain;
using Cloudea.Service.Forum.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject_Forum.Products;
public class ForumServiceTest
{
    private readonly ForumDomainService _forumDomainService;

    public ForumServiceTest()
    {
        var services = new ServiceCollection();
        services.AddDataBaseDefault(FreeSql.DataType.MySql, "Server=localhost;Port=3306;Database=test;Uid=root;Pwd=123456;");
        services.AddScoped<ForumDomainService, ForumDomainService>();
        var ini = new ModuleInitializer();
        ini.Initialize(services);
        var provider = services.BuildServiceProvider();
        _forumDomainService = provider.GetRequiredService<ForumDomainService>();
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

    [Fact(DisplayName = "创建Section时，参数错误（string.isnullorempty)")]
    public void Test_Create_When_Set_Name_Empty_String_Then_Throw_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => {
            Forum_Section.Create("", Guid.Parse("8fa2c964-c7e7-45f9-af89-acb914efc7d8"));
        });
    }

    [Fact(DisplayName = "创建Section时，参数错误（Guid.Empty)")]
    public void Test_Create_When_Set_Master_Empty_Guid_Then_Throw_ArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => {
            Forum_Section.Create("test1", Guid.Empty);
        });
    }

    [Fact(DisplayName = "创建Topic时，正常情况")]
    public async Task Test_CreateTopic_When_Right_Argument_Then_Return_Forum_Topic()
    {
        var request = new PostTopicRequest() {
            userId = Guid.Parse("8fa2c964-c7e7-45f9-af89-acb914efc7d8"),
            sectionId = Guid.Parse("660c062f-e523-4987-87f2-6e2e02556390"),
            title = "测试内容1",
            content = "测试内容"
        };
        var res = await _forumDomainService.CreateTopic(request);
        Assert.IsType<Forum_Topic>(res.Data);
    }

    /// <summary>
    /// 清理数据
    /// </summary>
    public void Dispose()
    {

    }
}


