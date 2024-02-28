using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain.Models;

public sealed record CreateTopicRequest(Guid SectionId, string Title, string Content);
