﻿using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using System.Collections.ObjectModel;
using System.Globalization;
using Finanse.DataAccessLayer;
using Finanse.Dialogs;
using Finanse.Models;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;

namespace Finanse.Pages {
    public sealed partial class Strona_glowna : Page {

        private List<int> visiblePayFormList = new List<int>();
        private OperationData storeData;
        private ObservableCollection<GroupInfoList<Operation>> operationGroups = new ObservableCollection<GroupInfoList<Operation>>();
        private DateTime actualYearAndMonth;

        protected override void OnNavigatedTo(NavigationEventArgs e) {

            actualYearAndMonth = (e.Parameter is DateTime) ?
                (DateTime)e.Parameter :
                new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            setNextMonthButtonEnabling();
            setPreviousMonthButtonEnabling();
            storeData = new OperationData(actualYearAndMonth.Month, actualYearAndMonth.Year, false, null);
            ByDateRadioButton.IsChecked = true;
            ///  SetListOfOperations(visiblePayFormList);
            ///  ByDateRadioButton.Checked += ByDateRadioButton_Checked;
            ///  ByCategoryRadioButton.Checked += ByCategoryRadioButton_Checked;
            setActualMonthText();
            setActualMoneyBar();
            setEmptyListViewInfoVisibility();

            base.OnNavigatedTo(e);
        }

        public Strona_glowna() {

            this.InitializeComponent();
           // this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
    
            foreach (var item in Dal.GetAllMoneyAccounts()) {
                ToggleMenuFlyoutItem itema = new ToggleMenuFlyoutItem {
                    Text = item.Name,
                    Tag = item.Id,
                    IsChecked = true,
                };
                itema.Click += Clicky;
                visiblePayFormList.Add(item.Id);
                VisiblePayFormMenuFlyout.Items.Add(itema);
            }
        }

        private List<MoneyAccount> listOfMoneyAccounts() {
            List<MoneyAccount> list = new List<MoneyAccount>();

            foreach (MoneyAccount account in Dal.GetAllMoneyAccounts()) {
                account.actualYearAndMonth = actualYearAndMonth;// actualMonthAndYear();
                list.Add(account);
            }

            return list;
        }

        private bool isFutureMonth(DateTime date) {
            return date > new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); //(actualYearAndMonth.Month <= DateTime.Today.Month && actualYearAndMonth.Year == DateTime.Today.Year) || actualYearAndMonth.Year < DateTime.Today.Year;
        }

        private OperationData setStoreData() {
            return new OperationData(actualYearAndMonth.Month, actualYearAndMonth.Year, isFutureMonth(actualYearAndMonth), visiblePayFormList);
        }

        private void setActualMonthText() {

            if (!isFutureMonth(actualYearAndMonth)) {
                ActualMonthText.Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(actualYearAndMonth.Month).First().ToString().ToUpper() + DateTimeFormatInfo.CurrentInfo.GetMonthName(actualYearAndMonth.Month).Substring(1);
                if (actualYearAndMonth.Year != DateTime.Today.Year)
                    ActualMonthText.Text += " " + actualYearAndMonth.Year.ToString();
            } else {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                ActualMonthText.Text = loader.GetString("plannedString");
            }
        }

        private void groupOperations(ObservableCollection<GroupInfoList<Operation>> operationGroups, OperationData operations) {
            operationGroups.Clear();

            foreach (var s in (bool)ByDateRadioButton.IsChecked ? operations.GroupsByDay : operations.GroupsByCategory)
                operationGroups.Add(s);
        }

        private void Clicky(object sender, RoutedEventArgs e) {
            if (((ToggleMenuFlyoutItem)sender).IsChecked == true)
                visiblePayFormList.Add((int)((ToggleMenuFlyoutItem)sender).Tag);
            else
                visiblePayFormList.Remove((int)((ToggleMenuFlyoutItem)sender).Tag);

            storeData.SetVisiblePayFormList(visiblePayFormList);

          //  groupOperations(operationGroups, storeData);

          //  SetActualMoneyBar();

            setListOfOperations(visiblePayFormList);

          //  if ((semanticZoom.ZoomedOutView as ListViewBase).ItemTemplate == null)
                    if ((bool)ByDateRadioButton.IsChecked)
                (semanticZoom.ZoomedOutView as ListViewBase).ItemsSource = storeData.OperationHeaders;
        }

        private void Grid_RightTapped(object sender, RightTappedRoutedEventArgs e) {
            FrameworkElement senderElement = sender as FrameworkElement;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            flyoutBase.ShowAt(senderElement);
        }

        private async void DetailsButton_Click(object sender, RoutedEventArgs e) {
            var datacontext = (e.OriginalSource as FrameworkElement).DataContext;
            var ContentDialogItem = new OperationDetailsContentDialog(operationGroups, (Operation)datacontext, "");
            var result = await ContentDialogItem.ShowAsync();
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e) {
            var datacontext = (e.OriginalSource as FrameworkElement).DataContext;
            var ContentDialogItem = new NewOperationContentDialog((Operation)datacontext);
            var result = await ContentDialogItem.ShowAsync();

            if (ContentDialogItem.isSaved()) {
                if ((bool)ByDateRadioButton.IsChecked) {
                    removeOperationFromListByDay((Operation)datacontext);
                    tryAddOperationToListByDay(ContentDialogItem.newOperation());
                }
                else {
                    removeOperationFromListByCategory((Operation)datacontext);
                    tryAddOperationToListByCategory(ContentDialogItem.newOperation());
                }

                setActualMoneyBar();
            }
        }

        private void removeOperationFromListByDay(Operation operation) {
            int index = operationGroups.Count - 1;  /// głupio bo leci po ostatnim indeksie a nie widzi że jest bez daty
            if (!operation.Date.Equals(""))
                index = operationGroups.IndexOf(operationGroups.First(i => ((GroupHeaderByDay)i.Key).date == operation.Date));

            if (operationGroups.ElementAt(index).Count > 1)
                operationGroups.ElementAt(index).Remove(operation);
            else
                operationGroups.RemoveAt(index);
        }

        private void tryAddOperationToListByDay(Operation operation) {
            if (actualYearAndMonth > DateTime.Today.Date) {
                if (operation.Date.Equals("")) {
                    if (operationGroups.Any(i => ((GroupHeaderByDay)i.Key).date == ""))
                        operationGroups.First(i => ((GroupHeaderByDay)i.Key).date == "").Insert(0, operation);
                    else {
                        GroupInfoList<Operation> group = new GroupInfoList<Operation>() {
                            Key = new GroupHeaderByDay(""),
                        };
                        group.Add(operation);
                        operationGroups.Add(group);
                    }
                }
                else if (Convert.ToDateTime(operation.Date).Date > DateTime.Today.Date){

                }
            }
            else if (Convert.ToDateTime(operation.Date).Month == actualYearAndMonth.Month && Convert.ToDateTime(operation.Date).Year == actualYearAndMonth.Year) {
                if (operationGroups.Any(i => ((GroupHeaderByDay)i.Key).date == operation.Date)) {
                    operationGroups.First(i => ((GroupHeaderByDay)i.Key).date == operation.Date).Insert(0, operation);
                }
                else {
                    GroupInfoList<Operation> group = new GroupInfoList<Operation>() {
                        Key = new GroupHeaderByDay(operation.Date),
                    };
                    group.Add(operation);

                    int i;
                    for (i = 0; i < operationGroups.Count; i++)
                        if (((GroupHeaderByDay)operationGroups.ElementAt(i).Key).date.CompareTo(operation.Date) < 0)
                            break;

                    operationGroups.Insert(i, group);
                }
            }
        }

        private void removeOperationFromListByCategory(Operation operation) {
            int index = operationGroups.IndexOf(operationGroups.First(i => ((GroupHeaderByCategory)i.Key).categoryId == operation.Id));
            if (operationGroups.ElementAt(index).Count > 1)
                operationGroups.ElementAt(index).Remove(operation);
            else
                operationGroups.RemoveAt(index);
        }

        private void tryAddOperationToListByCategory(Operation operation) {
            if (Convert.ToDateTime(operation.Date).Date > DateTime.Today.Date) {
                /// planowany wydatek
            }
            else if (Convert.ToDateTime(operation.Date).Month == actualYearAndMonth.Month && Convert.ToDateTime(operation.Date).Year == actualYearAndMonth.Year) {
                if (operationGroups.Any(i => ((GroupHeaderByCategory)i.Key).categoryId == operation.Id)) {
                    operationGroups.First(i => ((GroupHeaderByCategory)i.Key).categoryId == operation.Id).Add(operation);
                }
                else {
                    GroupInfoList<Operation> group = new GroupInfoList<Operation>();
                    group.Add(operation);
                    operationGroups.Add(group);
                }
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e) {
            var datacontext = (e.OriginalSource as FrameworkElement).DataContext;
            var ContentDialogItem = new Delete_ContentDialog(operationGroups, (Operation)datacontext, "");
            var result = await ContentDialogItem.ShowAsync();
            setActualMoneyBar();
        }

        private void Grid_DragStarting(UIElement sender, DragStartingEventArgs args) {
            FrameworkElement senderElement = sender as FrameworkElement;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            flyoutBase.ShowAt(senderElement);
        }

        private void OperacjeListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            this.OperacjeListView.SelectedItem = null;
        }

        private void semanticZoom_ViewChangeStarted(object sender, SemanticZoomViewChangedEventArgs e) {
            if (e.SourceItem == null)
                return;

            if (e.SourceItem.Item.GetType() == typeof(HeaderItem)) {
                HeaderItem hi = (HeaderItem)e.SourceItem.Item;

                var group = operationGroups.SingleOrDefault(d => ((GroupHeaderByDay)d.Key).dayNum == hi.Day);

                if (group != null)
                    e.DestinationItem = new SemanticZoomLocation() { Item = group };
            }

            //e.DestinationItem = new SemanticZoomLocation { Item = e.SourceItem.Item };
        }

        private async void PreviousMonthButton_Click(object sender, RoutedEventArgs e) {
            activateProgressRing();
            await Task.Delay(1);

            actualYearAndMonth = actualYearAndMonth.AddMonths(-1);
            setListOfOperations(visiblePayFormList);

            deactivateProgressRing();
        }

        private async void NextMonthButton_Click(object sender, RoutedEventArgs e) {
            activateProgressRing();
            await Task.Delay(1);

            actualYearAndMonth = actualYearAndMonth.AddMonths(1);
            setListOfOperations(visiblePayFormList);

            deactivateProgressRing();
        }

        private async void IncomingOperationsButton_Click(object sender, RoutedEventArgs e) {
            activateProgressRing();
            await Task.Delay(1);

            actualYearAndMonth = actualYearAndMonth.AddMonths(1);
            setListOfOperations(visiblePayFormList);
            PrevMonthButton.IsEnabled = true; // ponieważ trzeba wrócić z planowanych do aktualnego miesiąca
            IncomingOperationsButton.Visibility = Visibility.Collapsed;

            deactivateProgressRing();
        }

        private void activateProgressRing() {
            ProgressRingBackground.Visibility = Visibility.Visible;
            ProgressRing.IsActive = true;
        }

        private void deactivateProgressRing() {
            ProgressRingBackground.Visibility = Visibility.Collapsed;
            ProgressRing.IsActive = false;
        }

        private void setListOfOperations(List<int> visiblePayFormList) {

            storeData = setStoreData();
            setActualMonthText();
            groupOperations(operationGroups, storeData);

           // if ((semanticZoom.ZoomedOutView as ListViewBase).ItemTemplate == null)
                 if ((bool)ByDateRadioButton.IsChecked)
                (semanticZoom.ZoomedOutView as ListViewBase).ItemsSource = storeData.OperationHeaders;

            setActualMoneyBar();

            setNextMonthButtonEnabling();
            setPreviousMonthButtonEnabling();

            setEmptyListViewInfoVisibility();

            BalanceListView.ItemsSource = listOfMoneyAccounts();
        }

        private bool setEmptyListViewInfoVisibility() {
            if (operationGroups.Count == 0) {
                semanticZoom.Visibility = Visibility.Collapsed;
                EmptyListViewInfo.Visibility = Visibility.Visible;
                return false;
            }
            else {
                semanticZoom.Visibility = Visibility.Visible;
                EmptyListViewInfo.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        private void setNextMonthButtonEnabling() {
            if (new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1) <= actualYearAndMonth) {
                NextMonthButton.Visibility = Visibility.Collapsed;
                IncomingOperationsButton.Visibility = Visibility.Visible;
            } else {
                NextMonthButton.Visibility = Visibility.Visible;
                IncomingOperationsButton.Visibility = Visibility.Collapsed;
            }
        }

        private void setPreviousMonthButtonEnabling() {
            Operation eldestOperation = Dal.GetEldestOperation();

            PrevMonthButton.IsEnabled = eldestOperation == null ? 
                false : 
                Convert.ToDateTime(Dal.GetEldestOperation().Date) <= actualYearAndMonth;
        }

        private void setActualMoneyBar() {
            decimal actualMoney = 0;

            foreach (var group in operationGroups)
                actualMoney += group.decimalCost;

            ActualMoneyBar.Text = actualMoney.ToString("C", Settings.GetActualCurrency());
        }

        private void listViewByDate() {
            OperacjeListView.GroupStyle.Clear();
            OperacjeListView.GroupStyle.Add(ByDateGroupStyle);

            OperacjeListView.SelectionChanged -= OperacjeListView_SelectionChanged;
            OperacjeListView.SelectedItem = null;
            (semanticZoom.ZoomedOutView as ListViewBase).ItemsSource = storeData.OperationHeaders;
            (semanticZoom.ZoomedOutView as ListViewBase).ItemTemplate = null;
            (semanticZoom.ZoomedOutView as ListViewBase).ItemTemplateSelector = GroupEmptyOrFullSelector;
            (semanticZoom.ZoomedOutView as ListViewBase).ItemsPanel = ByDateGroupItemsPanelTemplate;

            OperacjeListView.SelectionChanged += OperacjeListView_SelectionChanged;

            semanticZoom.ViewChangeStarted -= semanticZoom_ViewChangeStarted;
            semanticZoom.ViewChangeStarted += semanticZoom_ViewChangeStarted;
        }

        private void listViewByCategory() {
            OperacjeListView.GroupStyle.Clear();
            OperacjeListView.GroupStyle.Add(ByCategoryGroupStyle);

            (semanticZoom.ZoomedOutView as ListViewBase).ItemsSource = ContactsCVS.View.CollectionGroups;
            (semanticZoom.ZoomedOutView as ListViewBase).ItemsPanel = ByCategoryGroupItemsPanelTemplate;
            (semanticZoom.ZoomedOutView as ListViewBase).ItemTemplateSelector = null;
            (semanticZoom.ZoomedOutView as ListViewBase).ItemTemplate = ByCategoryGroupItemTemplate;
        }

        private async void OperacjeListView_ItemClick(object sender, ItemClickEventArgs e) {
            Operation thisOperation = (Operation)e.ClickedItem;

            var ContentDialogItem = new OperationDetailsContentDialog(operationGroups, thisOperation, "");

            var result = await ContentDialogItem.ShowAsync();
        }

        private void ByCategoryRadioButton_Checked(object sender, RoutedEventArgs e) {
            activateProgressRing();
            //   await Task.Delay(5);

            groupOperations(operationGroups, storeData);
            listViewByCategory();

            deactivateProgressRing();
        }

        private void ByDateRadioButton_Checked(object sender, RoutedEventArgs e) {
            activateProgressRing();
            // await Task.Delay(5);

            groupOperations(operationGroups, storeData);
            listViewByDate();

            deactivateProgressRing();
        }

        private void ByDateButton_Click(object sender, RoutedEventArgs e) {
            ByDateButton.Foreground = ((SolidColorBrush)Application.Current.Resources["AccentColor"] as SolidColorBrush);
            ByCategoryButton.Foreground = ((SolidColorBrush)Application.Current.Resources["Text-1"] as SolidColorBrush);
            ByDateRadioButton.IsChecked = true;
        }

        private void ByCategoryButton_Click(object sender, RoutedEventArgs e) {
            ByDateButton.Foreground = ((SolidColorBrush)Application.Current.Resources["Text-1"] as SolidColorBrush);
            ByCategoryButton.Foreground = ((SolidColorBrush)Application.Current.Resources["AccentColor"] as SolidColorBrush);
            ByCategoryRadioButton.IsChecked = true;
        }
    }
}
