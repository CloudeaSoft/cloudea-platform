using Cloudea.Infrastructure.Database;
using Cloudea.Service.Forum.Domain.Models;
using MySqlX.XDevAPI.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cloudea.Entity.Forum {
    /// <summary>
    /// 论坛板块
    /// </summary>
    [AutoGenerateTable]
    public record Forum_Section : BaseDataEntity {
        /// <summary>
        /// 板块名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 板块主
        /// </summary>
        public Guid MasterId { get; set; }
        /// <summary>
        /// 板块简介
        /// </summary>
        public string? Statement { get; set; }
        /// <summary>
        /// 板块点击次数
        /// </summary>
        public long ClickCount { get; set; }
        /// <summary>
        /// 板块主题帖数
        /// </summary>
        public long TopicCount { get; private set; }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">板块名字</param>
        /// <param name="masterId">板块管理人id</param>
        /// <param name="statement">简介</param>
        /// <returns></returns>
        public static Forum_Section? Create(string name, Guid masterId, string? statement = null) {
            if (string.IsNullOrEmpty(name)) return null;
            if (masterId == Guid.Empty) return null;
            return new Forum_Section() {
                Id = Guid.NewGuid(),
                Name = name,
                MasterId = masterId,
                Statement = statement,
                ClickCount = 0,
                TopicCount = 0
            };
        }

        public Forum_Section UpdateSection(UpdateSectionRequest request) {
            if (Id != request.Id) {
                return this;
            }
            if (request.Name != null) {
                Name = request.Name;
            }
            if (request.Statement != null) {
                Statement = request.Statement;
            }
            if (request.MasterId != Guid.Empty) {
                MasterId = request.MasterId;
            }
            return this;
        }

        public void IncreaseTopicCount(int num = 1) {
            TopicCount += num;
        }

        public void DecreaseTopicCount(int num = 1) {
            TopicCount -= num;
            if (TopicCount < 0) {
                TopicCount = 0;
            }
        }
    }
}