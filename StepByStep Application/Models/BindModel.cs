using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepByStep_Application.Models
{
    public class BindModel
    {
        public string? BindModelName { get; set; }

        public int BindModelAverageSteps { get; set; }

        public int BindModelHighestResults { get; set; }

        public int BindModelWorstResults { get; set; }
    }
}
