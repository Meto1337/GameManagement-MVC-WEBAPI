using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels.Corporation
{
    public class IndexVM
    {
        public FilterVM Filter { get; set; }

        public List<CorporationVM> Items { get; set; }
        public PagerVM Pager { get; set; }
    }
}