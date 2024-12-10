using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretYonetimi
{
    public class ListBoxItem
    {
        public string DisplayName { get; set; }
        public string ProductID { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }

}
