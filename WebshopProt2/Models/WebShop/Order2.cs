using System;
using System.Runtime.Serialization;

namespace Acme.Shared.Contracts
{
    [DataContract]
    public class Order2
    {
        public Order2()
        {
        }

        [DataMember(IsRequired = true)]
        public int OrderID { get; set; }

        [DataMember(IsRequired = true)]
        public DateTime SubmittedOn { get; set; }

        [DataMember(IsRequired = true)]
        public string ShipToAddress { get; set; }

        [DataMember(IsRequired = true)]
        public string ShipToCity { get; set; }

        [DataMember(IsRequired = true)]
        public string ShipToCountry { get; set; }

        [DataMember(IsRequired = true)]
        public string ShipToZipCode { get; set; }

    }
}