using System;
using System.Collections.Generic;
using System.Text;

namespace CoBRA.Infra.CrossCutting.OTRSService.ViewModel
{
    public class ArticleOTRS
    {
        public string CommunicationChannel { get; set; }
        public int IsVisibleForCustomer { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string MimeType { get; set; }
        public string Charset { get; set; }
        public string HistoryType { get; set; }
        public string HistoryComment { get; set; }
        public int TimeUnit { get; set; }
    }
}
