using AirVinyl.Core.Entities;
using AirVinyl.SharedKernel;
using System.Collections.Generic;

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
        public virtual ICollection<DynamicProperty> DynamicProperties { get; set; } = new List<DynamicProperty>();
        private IDictionary<string, object> _properties;
        public IDictionary<string, object> Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new Dictionary<string, object>();
                    foreach (var dynamicProperty in DynamicProperties)
                        _properties.Add(dynamicProperty.Key, dynamicProperty.Value);
                }
                return _properties;
            }
            set { _properties = value; }
        }
    }
}
