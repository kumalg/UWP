﻿using System;
using Finanse.DataAccessLayer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Finanse.Models;
using Finanse.Models.MAccounts;

namespace Finanse.Dialogs {

    public sealed partial class EditMoneyAccountContentDialog : INotifyPropertyChanged {
        public MAccount EditedAccount { get; }
        private readonly MAccount _accountToEdit;

        private readonly TextBoxEvents _textBoxEvents = new TextBoxEvents();

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<KeyValuePair<object, object>> _colorBase;
        private List<KeyValuePair<object, object>> ColorBase => _colorBase ??
                                                                (_colorBase = ((ResourceDictionary) Application.Current.Resources["ColorBase"]).ToList());

        private string ColorKey => SelectedColor.Key.ToString();

        private KeyValuePair<object, object> _selectedColor;

        public KeyValuePair<object, object> SelectedColor {
            get {
                if (_selectedColor.Key == null || _selectedColor.Value == null)
                    _selectedColor = ColorBase.ElementAt(3);

                return _selectedColor;
            }
            set {
                _selectedColor = value;
                EditedAccount.ColorKey = ColorKey;
                RaisePropertyChanged(nameof(PrimaryButtonEnabling));
            }
        }

        //  private readonly IEnumerable<MAccount> _bankAccounts = MAccountsDal.GetAllAccounts();
        private readonly IEnumerable<MAccount> _bankAccounts =
            new List<MAccount> { new MAccount { Name = "Brak", Id = -1 } }.AsEnumerable().Concat(MAccountsDal.GetAllAccounts());//MAccountsDal.GetAllAccounts().Concat();

   //     private Visibility BankAccountsComboBoxVisibility => _accountToEdit is SubMAccount ? Visibility.Visible : Visibility.Collapsed;

        private MAccount _selectedBankAccount;

        public MAccount SelectedBankAccount {
            get {
                if (_selectedBankAccount == null && _bankAccounts.Any())
                    _selectedBankAccount = _bankAccounts.ElementAt(0);
                return _selectedBankAccount;
            }
            set {
                if (value.Id == -1) {
                    SelectedBankAccount = null;
                }
                else {
                    _selectedBankAccount = value;
                    if (EditedAccount is SubMAccount)
                        ((SubMAccount)EditedAccount).BossAccountGlobalId = _selectedBankAccount.GlobalId;
                }
                RaisePropertyChanged(nameof(PrimaryButtonEnabling));
            }
        }

        private bool PrimaryButtonEnabling => NameNotNullAndCategoryNotInBase;


        private bool NameNotNullAndCategoryNotInBase => !(MAccountsDal.AccountExistInBaseByName(NameValue.Text) || string.IsNullOrEmpty(NameValue.Text));

        public Brush NameValueForeground => IsThisCategoryInBase()
            ? (SolidColorBrush)Application.Current.Resources["RedColorStyle"]
            : (SolidColorBrush)Application.Current.Resources["Text"];

        private bool IsThisCategoryInBase() {
            return 
                !String.Equals(_accountToEdit.Name, NameValue.Text, StringComparison.CurrentCultureIgnoreCase) 
                && MAccountsDal.AccountExistInBaseByName(NameValue.Text);
        }

        public EditMoneyAccountContentDialog(MAccount accountToEdit) {
            InitializeComponent();

            _accountToEdit = accountToEdit;
            EditedAccount = accountToEdit.Clone();

            NameValue.Text = EditedAccount.Name;
            SelectedColor = ColorBase.SingleOrDefault(i => i.Key.ToString() == accountToEdit.ColorKey);
            if (accountToEdit is SubMAccount)
                SelectedBankAccount = _bankAccounts.SingleOrDefault(i => i.GlobalId == ((SubMAccount)accountToEdit).BossAccountGlobalId);
        }

        private void NewCategory_AddButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) {
            MAccountsDal.UpdateAccount(EditedAccount);
        }
        
        private void NameValue_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args) {
            EditedAccount.Name = sender.Text;
            RaisePropertyChanged(nameof(PrimaryButtonEnabling));
            NameValue.Foreground = NameValueForeground;
        }
    }
}
