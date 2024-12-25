using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretYonetimi
{
    public class ListBoxItem
    {
        public string DisplayName { get; set; }//title
        public string ProductID { get; set; }
        public string ProductName { get; set; }//marka

        public override string ToString()
        {
            return ProductName + " " + DisplayName; 
        }
    }

}
