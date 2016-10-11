using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.ViewModels
{
    public class Message
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }

        public Message() { }
        public Message(string sender, string subject, string body,string to)
        {
            Sender = sender;
            Subject = subject;
            Body = body;
            To = to;
        }
    }
}