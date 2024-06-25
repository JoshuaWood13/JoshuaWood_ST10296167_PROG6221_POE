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
using WpfApp_Part3_POE.ViewModels;

namespace WpfApp_Part3_POE.Views
{
    /// <summary>
    /// Interaction logic for View1.xaml
    /// </summary>
    public partial class View1 : UserControl
    {
        public View1()
        {
            InitializeComponent();

            //// Bind the InputPanel to the IngredientInputs collection
            //var inputPanelBinding = new Binding
            //{
            //    Source = DataContext,
            //    Path = new PropertyPath("IngredientInputs")
            //};

            //BindingOperations.SetBinding(InputPanel, ItemsControl.ItemsSourceProperty, inputPanelBinding);
        }
    }
}
