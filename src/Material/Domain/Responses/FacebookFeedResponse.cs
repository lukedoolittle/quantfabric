using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Material.Framework.Metadata;

namespace Material.Domain.Responses
{
    [GeneratedCode("T4Toolbox", "14.0")]
    [DataContract]
    public class FacebookFeedDatum
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "created_time")]
        public DateTime CreatedTime { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "story")]
        public string Story { get; set; }
    }

    [GeneratedCode("T4Toolbox", "14.0")]
    [DataContract]
    public class FacebookFeedPaging
    {
        [DataMember(Name = "previous")]
        public string Previous { get; set; }
        [DataMember(Name = "next")]
        public string Next { get; set; }
    }

    [GeneratedCode("T4Toolbox", "14.0")]
    [ClassDateTimeFormatter("yyyy-MM-ddTHH:mm:sszzz")]
    [DataContract]
    public class FacebookFeedResponse
    {
        [DataMember(Name = "data")]
        public IList<FacebookFeedDatum> Data { get; set; }
        [DataMember(Name = "paging")]
        public FacebookFeedPaging Paging { get; set; }
    }
}
