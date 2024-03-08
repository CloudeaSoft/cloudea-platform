namespace Cloudea.Infrastructure.Shared
{
    public class PageRequest
    {
        public PageRequest() { }

        public PageRequest(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; } = 15;

        /// <summary>
        /// 恢复默认
        /// </summary>
        public void SetDefault()
        {
            PageIndex = 1;
            PageSize = 15;
        }
    }
}
