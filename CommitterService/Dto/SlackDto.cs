using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommitterService.Dto
{
    public class SlackDto
    {
        public class Attachment
        {
            public string pretext { get; set; } = "";

            public string color { get; set; } = "danger";
            public string title { get; set; }
            public string title_link { get; set; }
            public List<Field> fields { get; set; }
            public long ts { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public class Field
        {
            public string title { get; set; }
            public string value { get; set; }
            [JsonProperty("short")]
            public bool Short { get; set; } = false;
        }
    }
}
