using Newtonsoft.Json;
using StepByStep_Application.Models;
using StepByStep_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StepByStep_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly UserViewModel _userViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _userViewModel = new UserViewModel();
            List<BindModel> users = BindViewModel.GetCollectionOfUsers(_userViewModel);

            DescriprtionOfUsers.ItemsSource = users;
        }
    }
}
