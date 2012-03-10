﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox
{
    using Simple.Web;

    [UriTemplate("/form")]
    public class FormEndpoint : GetEndpoint<string>
    {
        protected override string Get()
        {
            return @"<html><body><form action=""/submit"" method=""POST""><input type=""text"" name=""Text"" /><input type=""submit"" /></form></body></html>";
        }
    }

    public class Form
    {
        public string Text { get; set; }
    }
}