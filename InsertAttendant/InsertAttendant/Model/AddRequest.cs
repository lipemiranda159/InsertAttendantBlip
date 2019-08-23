using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsertAttendant.Model
{
    [JsonObject("resource")]
    public class Resource
    {
        public string identity { get; set; }
        public List<string> teams { get; set; }
    }

    [JsonObject("resource")]
    public class ResourceDelegation
    {
        public string target { get; set; }
        public List<string> envelopeTypes { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("blip_portal.email")]
        public string portal_email { get; set; }
        [JsonProperty("server.shouldStore")]
        public string server_shouldStore { get; set; }
    }

    public class DelegationRequest
    {
        public string method { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public ResourceDelegation resource { get; set; }
        public string id { get; set; }

    }

    public class AddRequest
    {
        public string method { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public Resource resource { get; set; }
        public string id { get; set; }

    }
}