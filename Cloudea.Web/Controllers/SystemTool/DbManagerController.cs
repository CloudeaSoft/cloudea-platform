using Cloudea.SystemTool;
using Cloudea.Web.Utils.ApiBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers.SystemTool
{
    /// <summary>
    /// 控制数据库
    /// </summary>
    public class DbManagerController : NamespaceRouteControllerBase
    {
        private readonly DbManager dbManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbManager"></param>
        public DbManagerController(DbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        /// <summary>
        /// 同步数据库表结构
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string SyncDatabaseStructure()
        {
            return dbManager.SyncDatabaseStructure();
        }
    }
}
