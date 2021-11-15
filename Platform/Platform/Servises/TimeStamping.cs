using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Servises
{
    public interface ITimeStamper
    {
        string TimeStamp { get; }
    }

    public class DefaultTimeStamper: ITimeStamper
    {
        public string TimeStamp
        {
            get => DateTime.Now.ToShortTimeString();
        }
    }
}
