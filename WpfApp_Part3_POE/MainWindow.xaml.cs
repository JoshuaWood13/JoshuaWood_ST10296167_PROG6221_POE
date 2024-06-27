// Name: Joshua Wood
// Student number: ST10296167
// Group: 2

using System;
using System.Collections.Generic;
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
using WpfApp_Part3_POE.Classes;
using WpfApp_Part3_POE.ViewModels;
using WpfApp_Part3_POE.Views;

namespace WpfApp_Part3_POE
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;
        private int previousTabIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            // Sets the window to fullscreen mode
            this.WindowState = WindowState.Maximized;
            // Sets the DataContext to the MainWindowViewModel
            viewModel = new MainWindowViewModel();
            DataContext = viewModel;

            MainTabControl.SelectionChanged += MainTabControl_SelectionChanged;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------//
        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabControl.SelectedIndex != previousTabIndex)
            {
                switch (MainTabControl.SelectedIndex)
                {
                    case 0: // Create Recipe tab
                        if (e.OriginalSource == MainTabControl)
                        {
                            // Check if user is switching back to the Create Recipe tab from a different tab
                            if (e.RemovedItems.Count > 0 && e.RemovedItems[0] is TabItem removedTab && removedTab.Header.ToString() != "Create Recipe")
                            {
                                viewModel.ResetCreateRecipeViewModel();
                            }
                            var createRecipeView = (Views.CreateRecipeView)((TabItem)MainTabControl.Items[0]).Content;
                            createRecipeView.DataContext = viewModel.CreateRecipeViewModel;
                        }
                        break;

                    case 1: // Display Recipe tab
                        viewModel.ClearDisplayedRecipe();
                        viewModel.DisplayRecipeViewModel.RefreshRecipeList();
                        if (previousTabIndex != -1 && previousTabIndex != 1)
                        {
                            viewModel.DisplayRecipeViewModel.ClearSelections();
                        }
                        break;

                    case 2: // Scale Recipe tab
                        viewModel.ScaleRecipeViewModel.RefreshRecipeList();
                        if (previousTabIndex != -1 && previousTabIndex != 2)
                        {
                            viewModel.ScaleRecipeViewModel.ClearSelections();
                        }
                        break;

                    case 3: // Reset Recipe tab
                        viewModel.ResetRecipeViewModel.RefreshRecipeList();
                        if (previousTabIndex != -1 && previousTabIndex != 3)
                        {
                            viewModel.ResetRecipeViewModel.ClearSelections();
                        }
                        break;

                    case 4: // Delete Recipe tab
                        viewModel.DeleteRecipeViewModel.RefreshRecipeList();
                        if (previousTabIndex != -1 && previousTabIndex != 4)
                        {
                            viewModel.DeleteRecipeViewModel.ClearSelections();
                        }
                        break;
                }
                previousTabIndex = MainTabControl.SelectedIndex;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//