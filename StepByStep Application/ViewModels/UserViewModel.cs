using Newtonsoft.Json;
using StepByStep_Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;

namespace StepByStep_Application.ViewModels
{
    public class UserViewModel
    {
        private User userModel;
        public List<User> Users { get; set; }

        public UserViewModel()
        {
            userModel = new User();
            GetJsonData();
        }

        private void GetJsonData()
        {
            string json = File.ReadAllText(@"day1.json");
            if (json is not null)
            Users = JsonConvert.DeserializeObject<List<User>>(json);
        }

    }
}
