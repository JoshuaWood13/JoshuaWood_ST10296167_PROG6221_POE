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
using WpfApp_POE_Part3.Classes;

namespace WpfApp_POE_Part3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RecipeManagerClass recipeList;

        public MainWindow()
        {
            InitializeComponent();
            recipeList = new RecipeManagerClass();

            //CreateRecipeContent.Content = new CreateRecipeUserControl(recipeList);
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabControl.SelectedItem is TabItem tabItem)
            {
                switch (tabItem.Header.ToString())
                {
                    case "Create Recipe":
                        CreateRecipeContent = new CreateRecipeUserControl(recipeList); // Create a new instance
                        tabItem.Content = CreateRecipeContent;
                        break;
                    case "Home":
                        // Code for home tab if needed
                        break;
                    case "View Recipes":
                        // Code for view recipes tab if needed
                        break;
                    case "Edit Recipe":
                        // Code for edit recipe tab if needed
                        break;
                    case "Delete Recipe":
                        // Code for delete recipe tab if needed
                        break;
                    default:
                        break;
                }
            }
        }

        public void SwitchToHomeTab()
        {
            MainTabControl.SelectedIndex = 0; // Index of the Home tab
        }
    }
}
