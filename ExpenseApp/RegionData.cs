using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp
{
    public class RegionData
    {
        [JsonProperty("province_list")]
        public Dictionary<String, ProvinceData> provinceList { get; set; }
    }

    public class ProvinceData
    {
        [JsonProperty("municipality_list")]
        public Dictionary<String, MunicipalData> MunicipalList { get; set; }
    }

    public class MunicipalData
    {
        [JsonProperty("barangay_list")]
        public List<String> BarangayList { get; set; }
    }

    
    public class locationData
    {
        public int R { get; set; } = 2;
        public int M { get; set; } = 2;
        public int P { get; set; } = 2;
        public int B {get; set; } = 2;
    }

}
