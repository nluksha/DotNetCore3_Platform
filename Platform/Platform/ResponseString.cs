﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform
{
    public static class ResponseString
    {
        public static string DefaultResponse =
@"
<!DOCTYPE html>
<html>
<head>
    <link rel=""stylesheet"" href=""lib/twitter-bootstrap/css/bootstrap.min.css"" />
    <meta charset=""utf-8"" />
    <title>Error</title>
</head>
<body class=""text-center"">
    <h3 class=""p-2"">Error {0}</h3>
    <h6>You can go back to the <a href= ""/"" >homepage</a> and try again</h6>
</body>
</html>
";
    }
}
