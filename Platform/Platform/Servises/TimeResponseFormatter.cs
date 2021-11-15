﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Servises
{
    public class TimeResponseFormatter : IResponseFormatter
    {
        private ITimeStamper stamper;

        public TimeResponseFormatter(ITimeStamper timeStamper)
        {
            stamper = timeStamper;
        }

        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"{stamper.TimeStamp}: {content}");
        }
    }
}
