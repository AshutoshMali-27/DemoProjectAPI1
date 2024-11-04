using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class MenuItemViewModel
    {
        public int MenuItemID { get; set; }
        public string Label { get; set; }
        public string Route { get; set; }
        public bool Collapsed { get; set; }
        public int ParentID { get; set; }
        //public List<MenuItemViewModel> Children { get; set; } = new List<MenuItemViewModel>();
    }
}
