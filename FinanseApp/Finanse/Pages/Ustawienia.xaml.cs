﻿using Finanse.Elements;
using Finanse.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Finanse.Pages {

    public sealed partial class Ustawienia : Page {

        public Ustawienia() {

            this.InitializeComponent();

            foreach (CultureInfo item in Settings.GetAllCurrencies()) {

                CurrencyValue.Items.Add(new ComboBoxItem {
                    Content = item.DisplayName,
                    Tag = item.Name
                });
            }

            if (Settings.GetActualIconStyle() == "Segoe UI") {
                ColorIcon_RadioButton.IsChecked = true;
            }
            else
                MonoIcon_RadioButton.IsChecked = true;

            if (Settings.GetTheme() == ApplicationTheme.Dark)
                ThemeToggle.IsOn = true;
            else
                ThemeToggle.IsOn = false;

            Calendar.MaxDate = DateTime.Today;
            Calendar.MinDate = Convert.ToDateTime("2016.05.10");

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            Settings.SetActualCurrency((string)((ComboBoxItem)CurrencyValue.SelectedItem).Tag);
        }

        private void IconStyleRadioButton_Checked(object sender, RoutedEventArgs e) {

            Settings.SetActualIconStyle(((RadioButton)sender).Tag.ToString());
        }

        private void ThemeToggle_Toggled(object sender, RoutedEventArgs e) {
            if (ThemeToggle.IsOn) {
                Settings.SetTheme(1);
            }
            else {
                Settings.SetTheme(0);
            }
        }
    }
}
