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
        //public RecipeManagerClass RecipeManager { get; private set; }
        public MainWindow()
        {
            InitializeComponent();

            // Set the DataContext to the MainWindowViewModel
            var viewModel = new MainWindowViewModel();
            DataContext = viewModel;

            // Handle tab selection change
            MainTabControl.SelectionChanged += (s, e) =>
            {
                if (MainTabControl.SelectedIndex == 0) // Assuming the "Create Recipe" tab is the first tab
                {
                    viewModel.ResetCreateRecipeViewModel();
                }
                else if (MainTabControl.SelectedIndex == 1) // Assuming the "Display Recipe" tab is the second tab
                {
                    viewModel.DisplayRecipeViewModel.RefreshRecipeList();
                }
            };
            // Set the DataContext to the MainWindowViewModel
            //DataContext = new MainWindowViewModel();
            //RecipeManager = new RecipeManagerClass();

            //// Pass the RecipeManager instance to the ViewModels
            //var createRecipeViewModel = new CreateRecipeViewModel(RecipeManager);
            //View1.DataContext = createRecipeViewModel;
        }
    }
}
