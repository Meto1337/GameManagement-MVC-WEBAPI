using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels.Game
{
    public class IndexVM
    {
        public FilterVM Filter { get; set; }
        public List<GameVM> Items { get; set; }
        public PagerVM Pager { get; set; }

    }
}