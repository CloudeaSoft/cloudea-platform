namespace Cloudea.Infrastructure.Models
{
    public class PageRequest
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// 每页数量
        /// </summary>
        public int Limit { get; set; } = 15;

        /// <summary>
        /// 恢复默认
        /// </summary>
        public void SetDefault()
        {
            this.Page = 1;
            this.Limit = 15;
        }
    }
}
