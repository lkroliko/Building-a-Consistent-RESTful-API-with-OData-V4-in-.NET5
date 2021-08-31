using AirVinyl.SharedKernel;
using Newtonsoft.Json;
using System;
using System.Text.Json;

namespace AirVinyl.Core.Entities
{
    public class DynamicProperty : BaseEntity
    {
        public string Key { get; set; }
        public string SerializedValue { get; set; }
        public object Value
        {
            //get { return JsonSerializer.Deserialize(SerializedValue, typeof(object)); }
            //set { SerializedValue = JsonSerializer.Serialize(value, typeof(object)); }

            get { return JsonConvert.DeserializeObject(SerializedValue); }
            set { SerializedValue = JsonConvert.SerializeObject(value); }
        }
    }
}
