﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Podfilter.Models.PodcastActions
{
    public interface IPodcastAction
    {
        XDocument PerformAction(XDocument podcast);
    }
}
