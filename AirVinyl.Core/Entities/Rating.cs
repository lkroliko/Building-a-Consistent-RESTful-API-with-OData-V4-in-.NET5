using AirVinyl.SharedKernel;

namespace AirVinyl.Entities
{
    public class Rating : BaseEntity
    {
        public int Value { get; set; }
        public virtual Person RatedBy { get; set; }
        public virtual RecordStore RecordStore {get;set;}
    }
}
