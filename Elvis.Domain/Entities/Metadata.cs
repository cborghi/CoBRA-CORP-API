using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elvis.Domain.Entities
{
    public class Metadata
    {
        public string imageSupplier { get; set; }
        public string category { get; set; }
        public string caption { get; set; }
        public string fileName { get; set; }
        public string credit { get; set; }
        public string usageFee { get; set; }
        public string status { get; set; }
        public string publicationName { get; set; }
        public string description { get; set; }
        public string licenseAgreement { get; set; }
        public string otherConstraints { get; set; }
    }
}
