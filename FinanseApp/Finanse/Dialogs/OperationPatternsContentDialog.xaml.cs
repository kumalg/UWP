﻿using Finanse.DataAccessLayer;
using Finanse.Models;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Finanse.Dialogs {

    public sealed partial class OperationPatternsContentDialog : ContentDialog {

        private List<OperationPattern> OperationPatterns = Dal.GetAllPatterns();

        public OperationPatternsContentDialog(Operation selectedOperation) {
            this.InitializeComponent();
        }

        private void OperationPatternsListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Hide();
        }

        public Operation setOperation () {
            return ((OperationPattern)OperationPatternsListView.SelectedItem).toOperation();
        }
    }
}
