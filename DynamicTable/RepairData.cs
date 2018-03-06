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
        public string conditionInput { get; set; }
        public string damageTypeInput { get; set; }
        public string damageMeasurementInput { get; set; }
        public string damageFurtherCommentsInput { get; set; }
        public string SAPcomment { get; set; }

        public RepairData(RepairData rd, bool b)
        {
            this.headingNumber = rd.headingNumber;
            this.headingName = rd.headingName;
            this.useableLimits = rd.useableLimits;
            this.repairableLimits = rd.repairableLimits;
            this.correctiveAction = rd.correctiveAction;
            this.relatedFigures = rd.relatedFigures;
            this.checkComplete = b;
            this.conditionInput = rd.conditionInput;
            this.damageTypeInput = rd.damageTypeInput;
            this.damageMeasurementInput = rd.damageMeasurementInput;
            this.damageFurtherCommentsInput = rd.damageFurtherCommentsInput;
            this.SAPcomment = rd.SAPcomment;
        }
        public RepairData(RepairData rd, string condition, string damagetype, string measurement, string furthercomments, string SAPcomment)
        {
            this.headingNumber = rd.headingNumber;
            this.headingName = rd.headingName;
            this.useableLimits = rd.useableLimits;
            this.repairableLimits = rd.repairableLimits;
            this.correctiveAction = rd.correctiveAction;
            this.relatedFigures = rd.relatedFigures;
            this.checkComplete = rd.checkComplete;
            this.conditionInput = condition;
            this.damageTypeInput = damagetype;
            this.damageMeasurementInput = measurement;
            this.damageFurtherCommentsInput = furthercomments;
            this.SAPcomment = SAPcomment;
        }
    }


}
