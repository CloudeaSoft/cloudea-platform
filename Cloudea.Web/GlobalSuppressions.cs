// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Minor Code Smell", "S3459:Unassigned members should be removed", Justification = "<挂起>", Scope = "member", Target = "~F:Cloudea.Web.Controllers.AuthServiceController._roleDbContext")]
[assembly: SuppressMessage("Critical Code Smell", "S4487:Unread \"private\" fields should be removed", Justification = "<挂起>", Scope = "member", Target = "~F:Cloudea.Web.Middlewares.GlobalExceptionHandlingMiddleware._logger")]
[assembly: SuppressMessage("Major Code Smell", "S2971:\"IEnumerable\" LINQs should be simplified", Justification = "<挂起>", Scope = "member", Target = "~M:Cloudea.Web.Controllers.DevController.SyncDatabase~Microsoft.AspNetCore.Mvc.IActionResult")]
[assembly: SuppressMessage("Minor Code Smell", "S3267:Loops should be simplified with \"LINQ\" expressions", Justification = "<挂起>", Scope = "member", Target = "~M:Cloudea.Web.Controllers.DevController.SyncDatabase~Microsoft.AspNetCore.Mvc.IActionResult")]
[assembly: SuppressMessage("Minor Code Smell", "S1125:Boolean literals should not be redundant", Justification = "<挂起>", Scope = "member", Target = "~M:Cloudea.Web.Controllers.GameHelper.ArkNightsController.Gacha(System.String,System.Int32)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("Major Bug", "S2583:Conditionally executed code should be reachable", Justification = "<挂起>", Scope = "member", Target = "~M:Cloudea.Web.Controllers.IdentityController.Login(Cloudea.Service.Auth.Domain.Models.UserLoginRequest)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]
[assembly: SuppressMessage("Minor Code Smell", "S3400:Methods should not return constants", Justification = "<挂起>", Scope = "member", Target = "~M:Cloudea.Web.Controllers.TestController.TestPut~System.String")]
[assembly: SuppressMessage("Major Code Smell", "S112:General exceptions should never be thrown", Justification = "<挂起>", Scope = "member", Target = "~M:Cloudea.Web.Controllers.TestController.Throw~Microsoft.AspNetCore.Mvc.IActionResult")]
[assembly: SuppressMessage("Minor Code Smell", "S1481:Unused local variables should be removed", Justification = "<挂起>", Scope = "member", Target = "~M:Cloudea.Web.Middlewares.GlobalExceptionHandlingMiddleware.HandleExceptionAsync(Microsoft.AspNetCore.Http.HttpContext,System.Exception)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Minor Code Smell", "S1125:Boolean literals should not be redundant", Justification = "<挂起>", Scope = "member", Target = "~M:Cloudea.Web.Middlewares.UserLoginGuidAuthenticationMiddleware.InvokeAsync(Microsoft.AspNetCore.Http.HttpContext,Cloudea.Service.Auth.Domain.Applications.UserDomainService)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Usage", "ASP0019:Suggest using IHeaderDictionary.Append or the indexer", Justification = "<挂起>", Scope = "member", Target = "~M:Cloudea.Web.Middlewares.UserLoginGuidAuthenticationMiddleware.InvokeAsync(Microsoft.AspNetCore.Http.HttpContext,Cloudea.Service.Auth.Domain.Applications.UserDomainService)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "<挂起>", Scope = "member", Target = "~M:Cloudea.Web.Program.Main(System.String[])")]
[assembly: SuppressMessage("Minor Code Smell", "S1199:Nested code blocks should not be used", Justification = "<挂起>", Scope = "member", Target = "~M:Cloudea.Web.Program.Main(System.String[])")]
[assembly: SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "<挂起>", Scope = "type", Target = "~T:Cloudea.Web.Program")]
