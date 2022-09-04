using StepByStep_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StepByStep_Application.Services
{
    public class BrushService
    {
        public static void BrushColumns(DataGrid data, BindModel selectedUser, List<BindModel> bindingData)
        {
            for (int i = 0; i < data.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)data.ItemContainerGenerator.ContainerFromIndex(i);

                if (row != null)
                {
                        if (selectedUser.BindModelAverageSteps * 0.8 > bindingData[i].BindModelWorstResults)
                        {
                            row.Style = (Style)data.Resources["SelectedRowStyle"];
                        }
                }
            }
        }

        public static void Clear(DataGrid data, BindModel selectedUser, List<BindModel> bindingData)
        {
            for (int i = 0; i < data.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)data.ItemContainerGenerator.ContainerFromIndex(i);

                if (row != null)
                {
                    if (selectedUser.BindModelAverageSteps * 0.8 > bindingData[i].BindModelWorstResults)
                    {
                        row.Style = null;
                    }
                }
            }
        }
    }
}
