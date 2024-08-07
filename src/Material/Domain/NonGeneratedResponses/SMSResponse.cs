﻿using System.Collections.Generic;

namespace Material.Domain.Responses
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public class SMSResponse : List<SMSMessage>
    { }

    public class SMSMessage
    {
        public long Date { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Creator { get; set; }
        public string Address { get; set; }
        public long DateSent { get; set; }
    }
}
