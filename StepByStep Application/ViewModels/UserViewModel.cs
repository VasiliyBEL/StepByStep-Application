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
using System.Reflection.Metadata;

namespace StepByStep_Application.ViewModels
{
    public class UserViewModel
    {
        public List<List<UserProfile>>? Users { get; set; }

        public HashSet<string>? Names { get; set; }

        public Dictionary<string, List<int>> UserSteps { get; set; }

        public Dictionary<string, int> HighestResults { get; set; }

        public Dictionary<string, int> WorstResults { get; set; }

        public Dictionary<string, int> AverageSteps { get; set; }

        public UserViewModel()
        {
            GetJsonData();
        }

        private void GetJsonData()
        {
            string path = @"C:\Users\vaspn\source\repos\StepByStep Application\StepByStep Application\Content\DAYS";
            var directory = new DirectoryInfo(path);
            List<List<UserProfile>> members = new List<List<UserProfile>>();
            for (int index = 1; index <= directory.GetFiles().Length; index++)
            {
                string json = File.ReadAllText($@"{path}\day{index}.json");
                if (json is not null)
                    members.Add(JsonConvert.DeserializeObject<List<UserProfile>>(json));
            }

            Users = members;

            if (Users is null)
                throw new ArgumentNullException(nameof(Users));

            Names = GetNames(Users);
            UserSteps = GetUserStepsResult(Users);
            HighestResults = GetBestResults(UserSteps);
            WorstResults = GetWorstResults(UserSteps);
            AverageSteps = GetAverageSteps(UserSteps);
        }

        private Dictionary<string, List<int>> GetUserStepsResult(List<List<UserProfile>>? users)
        {
            if (users == null)
                throw new ArgumentNullException(nameof(users));

            Dictionary<string, List<int>> userStepResult = new Dictionary<string, List<int>>();
            List<int> steps = new List<int>();

            for (int j = 0; j < users[16].Count; j++)
            {
                userStepResult.Add(users[16][j].User, new List<int>());
            }

            for (int i = 0; i < users.Count; i++)
            {
                for (int j = 0; j < users[i].Count; j++)
                {
                    if (userStepResult.ContainsKey(users[i][j].User))
                    userStepResult[users[i][j].User].Add(users[i][j].Steps);
                }
            }

            return userStepResult;
        }

        private HashSet<string> GetNames(List<List<UserProfile>>? users)
        {
            HashSet<string> names = new HashSet<string>();

            if (users == null)
                throw new ArgumentNullException(nameof(users));

            for (int i = 0; i < users.Count; i++)
            {
                foreach (var user in Users[i])
                    names.Add(user.User);
            }
            return names;
        }

        private Dictionary<string, int> GetBestResults(Dictionary<string, List<int>> usersSteps)
        {
            if (usersSteps == null)
                throw new ArgumentNullException(nameof(usersSteps));

            Dictionary<string, int> bestResultForUser = new Dictionary<string, int>();

            foreach (var userStep in usersSteps)
            {
                bestResultForUser.Add(userStep.Key, Convert.ToInt32(userStep.Value.Max()));
            }

            return bestResultForUser;
        }

        private Dictionary<string, int> GetWorstResults(Dictionary<string, List<int>> usersSteps)
        {
            if (usersSteps == null)
                throw new ArgumentNullException(nameof(usersSteps));

            Dictionary<string, int> worstResultForUser = new Dictionary<string, int>();

            foreach (var userStep in usersSteps)
            {
                worstResultForUser.Add(userStep.Key, Convert.ToInt32(userStep.Value.Min()));
            }

            return worstResultForUser;
        }

        private Dictionary<string, int> GetAverageSteps(Dictionary<string, List<int>> usersSteps)
        {
            if (usersSteps == null)
                throw new ArgumentNullException(nameof(usersSteps));

            Dictionary<string, int> averageForUser = new Dictionary<string, int>();

            foreach (var userStep in usersSteps)
            {
                averageForUser.Add(userStep.Key, Convert.ToInt32(userStep.Value.Average()));
            }

            return averageForUser;
        }
    }
}
