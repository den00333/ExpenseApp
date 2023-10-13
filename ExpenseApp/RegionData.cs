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
        public List<string> BarangayList { get; set; }
    }
}
