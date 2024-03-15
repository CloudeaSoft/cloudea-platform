using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Application.Forum.Contracts;

public sealed record CreateTopicRequest(Guid SectionId, string Title, string Content);
