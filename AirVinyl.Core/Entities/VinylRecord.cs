using AirVinyl.SharedKernel;

namespace AirVinyl.Entities
{
    
    public class VinylRecord : BaseEntity
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string CatalogNumber { get; set; }
        public int? Year { get; set; }
        public virtual PressingDetail PressingDetail { get; set; }
        public virtual Person Person { get; set; }
    }
}
