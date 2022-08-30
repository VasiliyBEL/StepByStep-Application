using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using StepByStep_Application.ViewModels;

namespace StepByStep_Application.Models
{
    public class UserProfile
    {
        public int Rank { get; set; }

        public string? User { get; set; }

        public string? Status { get; set; }

        public int Steps { get; set; }
    }
}
