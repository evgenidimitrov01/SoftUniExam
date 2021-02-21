using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace API_Testing
{
    public class Contact
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }

        public Contact() { }
    }
}
