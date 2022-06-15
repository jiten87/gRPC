using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpClient
{
    internal class WriteKeyGen
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("usageMask")]
        public long UsageMask { get; set; }

        [JsonProperty("algorithm")]
        public string Algorithm { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("deactivationDate")]
        public DateTimeOffset DeactivationDate { get; set; }

        [JsonProperty("protectStopDate")]
        public DateTimeOffset ProtectStopDate { get; set; }

        [JsonProperty("aliases")]
        public Alias[] Aliases { get; set; }
    }

    public partial class Alias
    {
        [JsonProperty("alias")]
        public string AliasAlias { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("ownerId")]
        public string OwnerId { get; set; }
    }
}

    

