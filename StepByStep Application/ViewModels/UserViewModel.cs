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
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace StepByStep_Application.ViewModels
{
    public class UserViewModel
    {
        public List<List<UserProfile>>? Users { get; set; }

        public HashSet<string>? Names { get; set; }

        public Dictionary<string, List<int>>? UserSteps { get; set; }

        public Dictionary<string, int>? HighestResults { get; set; }

        public Dictionary<string, int>? WorstResults { get; set; }

        public Dictionary<string, int>? AverageSteps { get; set; }

        public Dictionary<string, List<string>>? UserStatuses { get; set; }

        public Dictionary<string, List<int>>? UserRanks { get; set; }

        public UserViewModel()
        {
            GetJsonData();
        }

        private void GetJsonData()
        {
            string path = Path.GetFullPath(@"..\..\..\Content\DAYS");           
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
            UserStatuses = GetStatuses(Users);
            UserRanks = GetRanks(Users);
        }

        private Dictionary<string, List<int>> GetUserStepsResult(List<List<UserProfile>>? users)
        {
            if (users == null)
                throw new ArgumentNullException(nameof(users));

            Dictionary<string, List<int>> userStepResult = new Dictionary<string, List<int>>();

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

        private Dictionary<string, List<int>> GetRanks(List<List<UserProfile>> usersRanks)
        {
            if (usersRanks == null)
                throw new ArgumentNullException(nameof(usersRanks));

            Dictionary<string, List<int>> userRanksResult = new Dictionary<string, List<int>>();

            for (int j = 0; j < usersRanks[16].Count; j++)
            {
                userRanksResult.Add(usersRanks[16][j].User, new List<int>());
            }

            for (int i = 0; i < usersRanks.Count; i++)
            {
                for (int j = 0; j < usersRanks[i].Count; j++)
                {
                    if (userRanksResult.ContainsKey(usersRanks[i][j].User))
                        userRanksResult[usersRanks[i][j].User].Add(usersRanks[i][j].Rank);
                }
            }

            return userRanksResult;
        }

        private Dictionary<string, List<string>> GetStatuses(List<List<UserProfile>>? usersStatuses)
        {
            if (usersStatuses == null)
                throw new ArgumentNullException(nameof(usersStatuses));

            Dictionary<string, List<string>> userStatusesResult = new Dictionary<string, List<string>>();

            for (int j = 0; j < usersStatuses[16].Count; j++)
            {
                userStatusesResult.Add(usersStatuses[16][j].User, new List<string>());
            }

            for (int i = 0; i < usersStatuses.Count; i++)
            {
                for (int j = 0; j < usersStatuses[i].Count; j++)
                {
                    if (userStatusesResult.ContainsKey(usersStatuses[i][j].User))
                        userStatusesResult[usersStatuses[i][j].User].Add(usersStatuses[i][j].Status);
                }
            }

            return userStatusesResult;
        }

        public ObservableCollection<RectangleModel>? RectItems { get; set; }

        public ObservableCollection<RectangleModel> Draw(string? name, UserViewModel user)
        {
            ObservableCollection<RectangleModel> rectItems = new ObservableCollection<RectangleModel>();
            foreach (var userSteps in user.UserSteps)
            {
                if (userSteps.Key == name)
                {
                    double startX = 0;
                    double startY = 0;
                    for (int i = 0; i < userSteps.Value.Count; i++)
                    {
                        rectItems.Add(new RectangleModel()
                        {
                            X = startX,
                            Y = startY,
                            Width = 5,
                            Height = userSteps.Value[i] / 250,
                        });
                        startX += 10;
                    }
                }
            }

            return rectItems;
        }
    }
}
