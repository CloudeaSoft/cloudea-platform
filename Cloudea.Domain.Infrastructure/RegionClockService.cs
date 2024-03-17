using Cloudea.Domain.Common.Shared;

namespace Cloudea.Infrastructure
{
    public class RegionClockService
    {
        public DateTime GetDate()
        {
            return DateTime.Now;
        }

        public Result GetTimeToUnix(DateTime dateTime)
        {
            string timeStamp;
            try {
                timeStamp = dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString("F0");
            }
            catch (Exception ex) {
                return Result.Failure(new Error("出错了：" + ex));
            }
            return Result.Success(timeStamp);
        }

        public Result GetUnixToTime(string timeStamp)
        {
            DateTime dateTime;
            try {
                dateTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(timeStamp)).DateTime.ToLocalTime();
            }
            catch (Exception ex) {
                return Result.Failure(new Error("出错了：" + ex));
            }
            return Result.Success(dateTime);
        }
    }
}
