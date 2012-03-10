﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox
{
    using Simple.Web;

    [UriTemplate("/")]
    public class IndexEndpoint : GetEndpoint<string>
    {
        protected override string Get()
        {
            return "Simple.Web has entered the building.";
        }
    }
}