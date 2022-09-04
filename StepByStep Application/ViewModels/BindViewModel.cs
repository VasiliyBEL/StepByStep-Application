using StepByStep_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepByStep_Application.ViewModels
{
    public class BindViewModel
    {
        public static List<BindModel> GetCollectionOfUsers(UserViewModel userViewModel)
        {
            var collectionOfUsers = new List<BindModel>();

            for (int i = 0; i < userViewModel.Names?.Count; i++)
            {
                collectionOfUsers.Add(new BindModel
                {
                    BindModelName = userViewModel.AverageSteps?.Keys.ToList()[i],
                    BindModelAverageSteps = userViewModel.AverageSteps.Values.ToList()[i],
                    BindModelHighestResults = userViewModel.HighestResults.Values.ToList()[i],
                    BindModelWorstResults = userViewModel.WorstResults.Values.ToList()[i]
                });
            }


            return collectionOfUsers;
        }
    }
}
