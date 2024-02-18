using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain.Models
{
    public class PostTopicRequest
    {
        public Guid userId { get; set; }
        public Guid sectionId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }
}
