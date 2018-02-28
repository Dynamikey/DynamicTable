using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicTable
{
    public struct RepairData
    {
        public string headingNumber { get; set; }
        public string headingName { get; set; }
        public string useableLimits { get; set; }
        public string repairableLimits { get; set; }
        public string correctiveAction { get; set; }
        public string relatedFigures { get; set; }
        public bool checkComplete { get; set; }
        public string condition { get; set; }
    }
}
