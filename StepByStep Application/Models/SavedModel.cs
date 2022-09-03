using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepByStep_Application.Models
{
    public class SavedModel
    {
        public string? savedModelName { get; set; }

        public int savedModelAverageSteps { get; set; }

        public int savedModelHighestResults { get; set; }

        public int savedModelWorstResults { get; set; }

        public List<string>? savedUserStatuses { get; set; }

        public List<int>? savedUserRanks { get; set; }
    }
}
