﻿using System;
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
    /// Interaction logic for View4.xaml
    /// </summary>
    public partial class View4 : UserControl
    {
        public View4()
        {
            InitializeComponent();
            DataContext = new View4ViewModel();
        }
    }
}