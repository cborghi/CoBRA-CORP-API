using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elvis.Domain.Entities
{
    public class Item
    {
        public int firstResult { get; set; }
        public int maxResultHits { get; set; }
        public int totalHits { get; set; }
        public List<Hits> hits { get; set; }
    }
}
