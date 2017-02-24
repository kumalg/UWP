﻿using Finanse.Models;
using Finanse.Models.Statistics;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Finanse.Models.Categories;

namespace Finanse.DataAccessLayer {
    class StatisticsDal : DalBase {

        public static List<ChartPart> GetExpensesFromCategoryGroupedBySubCategoryInRange(DateTime minDate, DateTime maxDate, int categoryId) {
            List<ChartPart> models = new List<ChartPart>();

            // Create a new connection
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath)) {
                // Activate Tracing
                db.TraceListener = new DebugTraceListener();

                var query = from item in db.Table<Operation>().ToList()
                            where !String.IsNullOrEmpty(item.Date)
                            && item.isExpense
                            && IsDateInRange(item.Date, minDate, maxDate)
                            && item.CategoryId == categoryId
                            group item.Cost by item.SubCategoryId into g
                            orderby g.Sum() descending
                            select new {
                                SubCategoryId = g.Key,
                                Cost = g.Sum()
                            };

                foreach (var item in query) {
                    SubCategory subCategory = Dal.GetSubCategoryById(item.SubCategoryId);
                    if (subCategory != null)
                        models.Add(new ChartPart {
                            SolidColorBrush = subCategory.Brush,
                            Name = subCategory.Name,
                            UnrelativeValue = (double)item.Cost
                        });
                    else {
                        Category category = Dal.GetCategoryById(categoryId);
                        models.Add(new ChartPart {
                            SolidColorBrush = category.Brush,
                            Name = category.Name,
                            UnrelativeValue = (double)item.Cost
                        });
                    }
                }
            }
            
            return models;
        }


        private static bool IsDateInRange(string dateString, DateTime minDate, DateTime maxDate) {
            DateTime date = Convert.ToDateTime(dateString);
            return date.Date >= minDate.Date && date <= maxDate.Date;
        }
    }
}
