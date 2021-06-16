using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Infra.CrossCutting.OTRSService.ViewModel
{
    public class TicketOTRS
    {
        public string Title { get; set; }
        public string Queue { get; set; }
        public string Lock { get; set; }
        public string Type { get; set; }
        public string Service { get; set; }
        public string SLA { get; set; }
        public string State { get; set; }
        public string PriorityID { get; set; }
        public string CustomerUser { get; set; }
        public int TypeId { get; set; }
        public int Number { get; set; }
    }
}
