﻿using Finanse.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Finanse.Models {
    public class OperationData {
        private int month, year;
        private bool isFuture;
        private List<int> visiblePayFormList;

        private readonly ItemCollection _collection = new ItemCollection();
        public OperationData(int month, int year, bool isFuture, List<int> visiblePayFormList) {

            this.month = month;
            this.year = year;
            this.isFuture = isFuture;
            this.visiblePayFormList = visiblePayFormList;
        }

        public void SetVisiblePayFormList(List<int> visiblePayFormList) {
            if(this.visiblePayFormList != visiblePayFormList)
                this.visiblePayFormList = visiblePayFormList;
        }

        private ObservableCollection<GroupInfoList<Operation>> groupsByDay = null;

        public ObservableCollection<GroupInfoList<Operation>> GroupsByDay {
            get {
                if (groupsByDay == null || visiblePayFormList != null) {

                    groupsByDay = new ObservableCollection<GroupInfoList<Operation>>();

                    GroupInfoList<Operation> info;
                    //decimal sumCost = 0;

                    var query = from item in isFuture ? Dal.GetAllFutureOperations(visiblePayFormList) : Dal.GetAllOperations(month, year, visiblePayFormList)
                                group item by item.Date into g
                                orderby g.Key descending
                                select new {
                                    GroupName = g.Key,
                                    Items = g
                                };

                    foreach (var g in query) {
                        info = new GroupInfoList<Operation>() {
                            Key = new GroupHeaderByDay(g.GroupName),
                        };

                        //sumCost = 0;

                        foreach (var item in g.Items.OrderByDescending(i => i.Id)) {

                            info.Add(item);
                            //sumCost += item.isExpense ? -item.Cost : item.Cost;
                        }

                        //info.decimalCost = sumCost;
                        //info.cost = sumCost.ToString("C", Settings.GetActualCurrency());
                        groupsByDay.Add(info);
                    }
                }
                return groupsByDay;
            }
        }

        List<HeaderItem> operationHeaders = null;
        public List<HeaderItem> OperationHeaders {
            get {
                if (operationHeaders == null || visiblePayFormList != null) {
                    operationHeaders = new List<HeaderItem>();

                    int dayOfWeek = (int)(new DateTime(year, month, 1).DayOfWeek);
                    for (int i = 1; i < dayOfWeek; i++) {
                        operationHeaders.Add(new HeaderItem() { Day = String.Empty, IsEnabled = false });
                    }

                    for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++) {

                        if (this.GroupsByDay.Any(k => ((GroupHeaderByDay)k.Key).dayNum == i.ToString()))
                            operationHeaders.Add(new HeaderItem() { Day = i.ToString(), IsEnabled = true });
                        else
                            operationHeaders.Add(new HeaderItem() { Day = i.ToString(), IsEnabled = false });
                    }
                }

                return operationHeaders;
            }
        }

        private ObservableCollection<GroupInfoList<Operation>> groupsByCategory = null;

        public ObservableCollection<GroupInfoList<Operation>> GroupsByCategory {
            get {
                if (groupsByCategory == null || visiblePayFormList != null) {

                    groupsByCategory = new ObservableCollection<GroupInfoList<Operation>>();

                    GroupInfoList<Operation> info;
                    //decimal sumCost = 0;

                    var query = from item in isFuture ? Dal.GetAllFutureOperations(visiblePayFormList) : Dal.GetAllOperations(month, year, visiblePayFormList)
                                group item by item.CategoryId into g
                                orderby g.Key descending
                                select new {
                                    GroupName = g.Key,
                                    Items = g
                                };

                    foreach (var g in query) {
                        info = new GroupInfoList<Operation>();

                        info.Key = new GroupHeaderByCategory {
                            name = "Nieprzyporządkowane",
                            icon = ((FontIcon)Application.Current.Resources["DefaultEllipseIcon"]).Glyph,
                            color = ((SolidColorBrush)Application.Current.Resources["DefaultEllipseColor"]).Color.ToString(),
                            opacity = 0.2,
                        };

                        foreach (OperationCategory item in Dal.GetAllCategories()) {
                            if (item.Id == g.GroupName) {
                                ((GroupHeaderByCategory)info.Key).name = item.Name;
                                ((GroupHeaderByCategory)info.Key).icon = item.Icon;
                                ((GroupHeaderByCategory)info.Key).color = item.Color;
                                ((GroupHeaderByCategory)info.Key).opacity = 1;
                                break;
                            }
                        }

                        ((GroupHeaderByCategory)info.Key).iconStyle = new FontFamily(Settings.GetActualIconStyle());

                        //sumCost = 0;

                        foreach (var item in g.Items) {

                            info.Add(item);
                            //sumCost += item.isExpense ? -item.Cost : item.Cost;
                        }

                        //info.cost = sumCost.ToString("C", Settings.GetActualCurrency());
                        groupsByCategory.Add(info);
                    }
                }
                return groupsByCategory;
            }
        }
    }
}
