using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleCarDb.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int BrandId { get; set; }

        [JsonIgnore] //megint elfelejtettem...
        public Brand? Brand { get; set; }
    }
}
