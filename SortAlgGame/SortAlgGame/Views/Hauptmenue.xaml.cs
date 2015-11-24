﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SortAlgGame.Views
{
    /// <summary>
    /// Interaktionslogik für Hauptmenue.xaml
    /// </summary>
    public partial class Hauptmenue : Page
    {
        public Hauptmenue()
        {
            InitializeComponent();
            DataContext = new Hauptmenue();
        }

        private void BtnErklClick(object sender, RoutedEventArgs e)
        {
            MenueErklaerung erkl = new MenueErklaerung();
            this.NavigationService.Navigate(erkl);
        }
    }
}
