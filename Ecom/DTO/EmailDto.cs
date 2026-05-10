using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.DTO
{
    public class EmailDto
    {
        public EmailDto(string to , string from, string subject, string content)
        {
            To = to;
            From = from;
            Subject = subject;
            Contant = content;
        }
        public  string To { get; set; }

        public string From { get; set; }
        public string Subject { get; set; }

        public string Contant { get; set; }
    }
}
