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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;
        //public RecipeManagerClass RecipeManager { get; private set; }
        public MainWindow()
        {
            InitializeComponent();

            // Set the DataContext to the MainWindowViewModel
            //var viewModel = new MainWindowViewModel();
            viewModel = new MainWindowViewModel();
            DataContext = viewModel;

            MainTabControl.SelectionChanged += MainTabControl_SelectionChanged;
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabControl.SelectedIndex == 0) // Create Recipe tab
            {
                viewModel.ResetCreateRecipeViewModel();
                // Reset the DataContext of the View1 to the new CreateRecipeViewModel
                var createRecipeView = (Views.View1)((TabItem)MainTabControl.Items[0]).Content;
                createRecipeView.DataContext = viewModel.CreateRecipeViewModel;
            }
            else if (MainTabControl.SelectedIndex == 1) // Display Recipe tab
            {
                viewModel.DisplayRecipeViewModel.RefreshRecipeList();
            }
            else if (MainTabControl.SelectedIndex == 2) // Scale Recipe tab
            {
                viewModel.ScaleRecipeViewModel.RefreshRecipeList();
            }
        }
        // Handle tab selection change
        //MainTabControl.SelectionChanged += (s, e) =>
        //    {
        //        if (MainTabControl.SelectedIndex == 0) // Assuming the "Create Recipe" tab is the first tab
        //        {
        //            viewModel.ResetCreateRecipeViewModel();
        //        }
        //        else if (MainTabControl.SelectedIndex == 1) // Assuming the "Display Recipe" tab is the second tab
        //        {
        //            viewModel.DisplayRecipeViewModel.RefreshRecipeList();
        //        }
        //        else if(MainTabControl.SelectedIndex == 2)
        //        {
        //            viewModel.ScaleRecipeViewModel.RefreshRecipeList();
        //        }
        //    };




            // Set the DataContext to the MainWindowViewModel
            //DataContext = new MainWindowViewModel();
            //RecipeManager = new RecipeManagerClass();

            //// Pass the RecipeManager instance to the ViewModels
            //var createRecipeViewModel = new CreateRecipeViewModel(RecipeManager);
            //View1.DataContext = createRecipeViewModel;
    }
}

