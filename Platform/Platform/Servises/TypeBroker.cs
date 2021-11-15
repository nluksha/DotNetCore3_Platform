using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Servises
{
    public static class TypeBroker
    {
        private static IResponseFormatter formatter = new HtmlResponseFromatter();

        public static IResponseFormatter Formatter => formatter;
    }
}
