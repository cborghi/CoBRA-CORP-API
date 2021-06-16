using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elvis.Domain.Entities
{
    public class Hits
    {
        public string id { get; set; }
        public string thumbnailUrl { get; set; }
        public string previewUrl { get; set; }
        public string originalUrl { get; set; }
        public int contador { get; set; }
        public string image { get; set; }
        public int? requisicao { get; set; }
        public Metadata metadata { get; set; }
    }
}
