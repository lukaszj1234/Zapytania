using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Event
{
    public class AfterNameChangedEvent:PubSubEvent<AfterNameChangedEventArgs>
    {
    }

    public class AfterNameChangedEventArgs
    {
        public int Id { get; set; }
        public string DisplayInquiry { get; set; }
        public string DisplayIndustry { get; set; }
    }
}
