using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cloudea.Service.Forum.Domain.Models {
    public class PostSectionRequest {
        public string SectionName { get; set; }
        public Guid MasterId { get; set; }
        public string Statement { get; set; }
    }
}
