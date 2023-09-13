using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.MiscTool {
    public class RegionClock {
        public DateTime getDate() {
            return DateTime.Now;
        }

        public Result getTimeToUnix(DateTime dateTime) {
            string timeStamp;
            try {
                timeStamp = dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString("F0");
            }
            catch (Exception ex) {
                return Result.Fail("出错了：", ex);
            }
            return Result.Success(timeStamp);
        }

        public Result getUnixToTime(string timeStamp) {
            DateTime dateTime;
            try {
                dateTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(timeStamp)).DateTime.ToLocalTime();
            }
            catch (Exception ex) {
                return Result.Fail("出错了：", ex);
            }
            return Result.Success(dateTime);
        }
    }
}
