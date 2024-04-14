using Cloudea.Domain.Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Application.Forum.Contracts.Response
{
    public class ListSectionResponse : PageResponse<ListSectionResponse.Section>
    {
        public class Section
        {
            public Guid Id { get; set; }
            public Guid MasterUserId { get; private set; }

            public string Name { get; private set; } = string.Empty;
            public string Statement { get; private set; } = string.Empty;
            public long ClickCount { get; private set; }
            public long TopicCount { get; private set; }

            public DateTimeOffset CreatedOnUtc { get; set; }
            public DateTimeOffset? ModifiedOnUtc { get; set; }
        }
    }
}
