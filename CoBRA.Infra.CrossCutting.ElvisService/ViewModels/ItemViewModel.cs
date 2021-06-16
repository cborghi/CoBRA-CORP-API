using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoBRA.Infra.CrossCutting.ElvisService.ViewModels
{
    public class ItemViewModel
    {
        public int firstResult { get; set; }
        public int maxResultHits { get; set; }
        public int totalHits { get; set; }
        public List<HitsViewModel> hits { get; set; }
    }
}
