using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.Book.Models
{
    public class CreateBookMetaRequest
    {
        public string title { get; set; }
        public string author { get; set; }
    }
}
