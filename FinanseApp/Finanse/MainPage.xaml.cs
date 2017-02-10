﻿using System;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Finanse.Pages;
using Windows.Graphics.Display;
using Finanse.DataAccessLayer;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.System.Profile;
using Windows.UI.Core;
using Windows.Foundation;
using Windows.ApplicationModel.Core;
using Finanse.Models;

namespace Finanse {
    public sealed partial class MainPage : Page {

        public MainPage() {
            this.InitializeComponent();
            Dal.createDB();
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(360, 530));

            CoreApplication.GetCurrentView().TitleBar.IsVisibleChanged += TitleBar_IsVisibleChanged;

            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            formattableTitleBar.ButtonHoverBackgroundColor = Functions.GetSolidColorBrush("#19000000").Color;

            if (OperationsAppBarRadioButton != null)
                OperationsAppBarRadioButton.IsChecked = true;

            if (Strona_glowna_ListBoxItem != null)
                Strona_glowna_ListBoxItem.IsChecked = true;
        }

        private void TitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args) {
            TitleBar.Visibility = sender.IsVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e) {
            if (AktualnaStrona_Frame.CurrentSourcePageType != typeof(Strona_glowna)) {
                OperationsAppBarRadioButton.IsChecked = true;// AktualnaStrona_Frame.Navigate(typeof(Strona_glowna));
                e.Handled = true;
            }
        }
        private void BackRequestedEvent(object sender, BackRequestedEventArgs e) {
            if (AktualnaStrona_Frame.CurrentSourcePageType != typeof(Strona_glowna)) {
                AktualnaStrona_Frame.Navigate(typeof(Strona_glowna));
                e.Handled = true;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            SystemNavigationManager.GetForCurrentView().BackRequested += BackRequestedEvent;
            base.OnNavigatedTo(e);
        }
       
        private async void ShowStatusBar() {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar")) {
                var statusBar = StatusBar.GetForCurrentView();
                await statusBar.ShowAsync();
            }
        }

        private async void HideStatusBar() {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar")) {
                var statusBar = StatusBar.GetForCurrentView();
                await statusBar.HideAsync();
            }
        }

        private void whichOrientation() {
            DisplayInformation info = DisplayInformation.GetForCurrentView();

            if (info.CurrentOrientation == DisplayOrientations.Landscape || info.CurrentOrientation == DisplayOrientations.LandscapeFlipped) {
                StatusBarAndTitleBar("Background", "Text-1");
                HideStatusBar();
            }
            else {
                StatusBarAndTitleBar("AccentColor", "AccentText");
                ShowStatusBar();
            }
        }

        private void StatusBarAndTitleBar(string statusBarBackgroundColor, string statusBarForegroundColor) {
            //PC customization
            /*
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView")) {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null) {
                    titleBar.ButtonBackgroundColor = ((SolidColorBrush)Application.Current.Resources["AccentColor"] as SolidColorBrush).Color;
                    titleBar.ButtonForegroundColor = Colors.White;

                    titleBar.BackgroundColor = ((SolidColorBrush)Application.Current.Resources["AccentColor"] as SolidColorBrush).Color;
                    titleBar.ForegroundColor = Colors.White;
                }
            }
            */
            //Mobile customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar")) {

                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null) {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = ((SolidColorBrush)Application.Current.Resources[statusBarBackgroundColor] as SolidColorBrush).Color;
                    if (statusBarForegroundColor == "White")
                        statusBar.ForegroundColor = Colors.White;
                    else
                        statusBar.ForegroundColor = ((SolidColorBrush)Application.Current.Resources[statusBarForegroundColor] as SolidColorBrush).Color;
                }
            }
        }

        private void OperationsAppBarRadioButton_Click(object sender, RoutedEventArgs e) {

        }

        private void closeCommandBar() {
            if (CommandBar != null)
                CommandBar.IsOpen = false;
        }

        private async void OperationsAppBarRadioButton_Checked(object sender, RoutedEventArgs e) {
            closeCommandBar();

            if (Strona_glowna_ListBoxItem != null)
                Strona_glowna_ListBoxItem.IsChecked = true;
            if (OperationsAppBarRadioButton != null)
                OperationsAppBarRadioButton.IsChecked = true;

            await Task.Delay(5);
            if (AktualnaStrona_Frame.CurrentSourcePageType != typeof(Strona_glowna))
                AktualnaStrona_Frame.Navigate(typeof(Strona_glowna));
        }

        private async void CategoriesAppBarRadioButton_Checked(object sender, RoutedEventArgs e) {
            closeCommandBar();

            if (Kategorie_ListBoxItem != null)
                Kategorie_ListBoxItem.IsChecked = true;
            if (CategoriesAppBarRadioButton != null)
                CategoriesAppBarRadioButton.IsChecked = true;

            await Task.Delay(5);
            AktualnaStrona_Frame.Navigate(typeof(Kategorie));
        }

        private async void AddNewOperationAppBarRadioButton_Checked(object sender, RoutedEventArgs e) {
            closeCommandBar();

            if (AddNewOperation_ListBoxItem != null)
                AddNewOperation_ListBoxItem.IsChecked = true;
            if (AddNewOperationAppBarRadioButton != null)
                AddNewOperationAppBarRadioButton.IsChecked = true;

            await Task.Delay(5);
            AktualnaStrona_Frame.Navigate(typeof(Nowa_Operacja));
        }

        private async void StatisticsAppBarRadioButton_Checked(object sender, RoutedEventArgs e) {
            closeCommandBar();

            if (Statystyki_ListBoxItem != null)
                Statystyki_ListBoxItem.IsChecked = true;
            if (StatisticsAppBarRadioButton != null)
                StatisticsAppBarRadioButton.IsChecked = true;

            await Task.Delay(5);
            AktualnaStrona_Frame.Navigate(typeof(Statystyki));
        }
        private async void RadioButton_Click(object sender, RoutedEventArgs e) {
            MoreAppBarRadioButton.IsChecked = false;
            await Task.Delay(5);
            CommandBar.IsOpen = !CommandBar.IsOpen;
        }

        private void navigateIfDifferentPage(Type pageType) {
            if (AktualnaStrona_Frame.CurrentSourcePageType != pageType)
                AktualnaStrona_Frame.Navigate(pageType);
        }

        private async void SzablonyAppBarButton_Click(object sender, RoutedEventArgs e) {
            UncheckAllMenuButtons();

            if (Szablony_ListBoxItem != null)
                Szablony_ListBoxItem.IsChecked = true;

            await Task.Delay(5);
            //  (Frame as ThemeAwareFrame).AppTheme = ElementTheme.Light;
            navigateIfDifferentPage(typeof(Szablony));
        }

        private async void ZleceniaStaleAppBarButton_Click(object sender, RoutedEventArgs e) {
            UncheckAllMenuButtons();

            if (ZleceniaStale_ListBoxItem != null)
                ZleceniaStale_ListBoxItem.IsChecked = true;

            await Task.Delay(5);
            //   (Frame as ThemeAwareFrame).AppTheme = ElementTheme.Dark;
            navigateIfDifferentPage(typeof(ZleceniaStale));
        }

        private async void KontaAppBarButton_Click(object sender, RoutedEventArgs e) {
            UncheckAllMenuButtons();

            if (Konta_ListBoxItem != null)
                Konta_ListBoxItem.IsChecked = true;

            await Task.Delay(5);
            navigateIfDifferentPage(typeof(Konta));
        }

        private async void UstawieniaAppBarButton_Click(object sender, RoutedEventArgs e) {
            UncheckAllMenuButtons();

            if (Ustawienia_ListBoxItem != null)
                Ustawienia_ListBoxItem.IsChecked = true;

            await Task.Delay(5);
            navigateIfDifferentPage(typeof(Ustawienia));
        }

        private void CommandBar_Opening(object sender, object e) {
            OperationsAppBarRadioButton.Height = Double.NaN;
            CategoriesAppBarRadioButton.Height = Double.NaN;
            AddNewOperationAppBarRadioButton.Height = Double.NaN;
            StatisticsAppBarRadioButton.Height = Double.NaN;
        }

        private void CommandBar_Closing(object sender, object e) {
            OperationsAppBarRadioButton.Height = 40;
            CategoriesAppBarRadioButton.Height = 40;
            AddNewOperationAppBarRadioButton.Height = 40;
            StatisticsAppBarRadioButton.Height = 40;
        }

        private void UncheckAllMenuButtons() {
            if (AddNewOperationAppBarRadioButton != null) {
                OperationsAppBarRadioButton.IsChecked = false;
                CategoriesAppBarRadioButton.IsChecked = false;
                AddNewOperationAppBarRadioButton.IsChecked = false;
                StatisticsAppBarRadioButton.IsChecked = false;
            }
        }
        
        private void AktualnaStrona_Frame_Navigated(object sender, NavigationEventArgs e) {
            if (((Frame)sender).SourcePageType == typeof(Strona_glowna)) {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                TitleBar.Margin = new Thickness(16, 0, 0, 0);
                if (OperationsAppBarRadioButton != null)
                    OperationsAppBarRadioButton.IsChecked = true;
                if (Strona_glowna_ListBoxItem != null)
                    Strona_glowna_ListBoxItem.IsChecked = true;
            }
            else {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                TitleBar.Margin = new Thickness(64, 0, 0, 0);
            }
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e) {
            whichOrientation();
        }
    }
}
