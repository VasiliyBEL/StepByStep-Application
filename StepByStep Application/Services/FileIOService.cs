using Microsoft.Win32;
using StepByStep_Application.Models;
using StepByStep_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StepByStep_Application.Services
{
    public class FileIOService
    {
        private static BindModel? _data;
        public FileIOService(BindModel data)
        {
            _data = data;
        }

        public static void Save(SaveFileDialog file, UserViewModel userViewModel)
        {
            SavedModel userData = new SavedModel();

            userData.savedModelName = _data?.BindModelName;
            userData.savedModelAverageSteps = _data.BindModelAverageSteps;
            userData.savedModelHighestResults = _data.BindModelHighestResults;
            userData.savedModelWorstResults = _data.BindModelWorstResults;
            userData.SavedUserRanks = userViewModel?.UserRanks?[userData.savedModelName];
            userData.SavedUserStatuses = userViewModel?.UserStatuses?[userData.savedModelName];

            string json = JsonSerializer.Serialize<SavedModel>(userData);
            File.WriteAllText(file.FileName, Regex.Unescape(json), Encoding.UTF8);
        }
    }
}
