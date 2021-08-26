using AirVinyl.Core.ValueObjects;
using AirVinyl.SharedKernel;
using System.Collections.Generic;

namespace AirVinyl.Entities
{
    public class RecordStore : BaseEntity
    {
        public string Name { get; set; }
        public Address StoreAddress { get; set; } 
        //public List<string> Tags { get; set; } = new List<string>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();         
    }
}
