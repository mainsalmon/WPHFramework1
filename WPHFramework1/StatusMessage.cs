using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPHFramework1
{
    public enum Criticality
    {
        Critical,
        Warning,
        Informational
    }
    public class StatusMessage
    {
        public StatusMessage(Criticality criticality, string messageText){
            Criticality = criticality;
            Message = messageText;
        }

        public string Message { get; set; }
        public Criticality Criticality {get; set;}
    }
}
