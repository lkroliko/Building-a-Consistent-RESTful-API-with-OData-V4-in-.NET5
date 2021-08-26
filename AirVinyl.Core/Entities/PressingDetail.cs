using AirVinyl.SharedKernel;

namespace AirVinyl.Entities
{
    public class PressingDetail : BaseEntity
    {
        public int Grams { get; set; }
        public int Inches { get; set; }
        public string Description { get; set; }
    }
}
