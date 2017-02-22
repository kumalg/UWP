﻿using Finanse.DataAccessLayer;
using Finanse.Dialogs;
using Finanse.Models;
using Finanse.Models.MoneyAccounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Finanse.Pages {

    public sealed partial class AccountsPage : Page {

        private ObservableCollection<Account> MoneyAccounts = new ObservableCollection<Account>(AccountsDal.getAllMoneyAccounts());

        public AccountsPage() {
            this.InitializeComponent();
        }

        private ObservableCollection<Account> accounts;
        private ObservableCollection<Account> Accounts {
            get {
                if (accounts == null)
                    accounts = new ObservableCollection<Account>(AccountsDal.getAllAccounts());
                return accounts;
            }
        }

        private async void NewMoneyAccount_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            var ContentDialogItem = new NewMoneyAccountContentDialog();
            var result = await ContentDialogItem.ShowAsync();

            if (result == ContentDialogResult.Primary) {
                Account newAccount = ContentDialogItem.getNewAccount();
                newAccount.Id = AccountsDal.getHighestIdOfAccounts();

                if (newAccount is BankAccount)
                    Accounts.Insert(0, new BankAccountWithCards((BankAccount)newAccount));
                else if (newAccount is CardAccount) {
                    CardAccount newCardAccount = (CardAccount)newAccount;
                    BankAccountWithCards bankAccount = (BankAccountWithCards)Accounts.SingleOrDefault(i => i.Id == ((CardAccount)newAccount).BankAccountId);
                    bankAccount.Cards.Insert(0, newCardAccount);
                }
                else
                    Accounts.Insert(0, newAccount);
            }
        }

        private async void EditButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            object datacontext = (e.OriginalSource as FrameworkElement).DataContext;
            Account oldAccound = (Account)datacontext;
            EditMoneyAccountContentDialog editMoneyAccountContentDialog = new EditMoneyAccountContentDialog(oldAccound);
            ContentDialogResult result = await editMoneyAccountContentDialog.ShowAsync();

            if (result == ContentDialogResult.Primary) {
                Account EditedAccound = editMoneyAccountContentDialog.EditedAccount;
                int index = Accounts.IndexOf(oldAccound);
                Accounts.Remove(oldAccound);
                Accounts.Insert(index, EditedAccound);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            object datacontext = (e.OriginalSource as FrameworkElement).DataContext;
            showDeleteAccountContentDialog((Account)datacontext);
        }

        private async void EditCard_Click(object sender, RoutedEventArgs e) {
            object datacontext = (e.OriginalSource as FrameworkElement).DataContext;
            CardAccount oldAccound = (CardAccount)datacontext;
            EditMoneyAccountContentDialog editMoneyAccountContentDialog = new EditMoneyAccountContentDialog((Account)datacontext);
            ContentDialogResult result = await editMoneyAccountContentDialog.ShowAsync();

            if (result == ContentDialogResult.Primary) {
                CardAccount EditedAccound = (CardAccount)editMoneyAccountContentDialog.EditedAccount;
                BankAccountWithCards bankAccount = (BankAccountWithCards)Accounts.SingleOrDefault(i => i.Id == oldAccound.BankAccountId);
                bankAccount.Cards.Remove(oldAccound);

                bankAccount = (BankAccountWithCards)Accounts.SingleOrDefault(i => i.Id == EditedAccound.BankAccountId);
                bankAccount.Cards.Insert(0, EditedAccound);
            }
        }

        private void DeleteCard_Click(object sender, RoutedEventArgs e) {
            object datacontext = (e.OriginalSource as FrameworkElement).DataContext;
            showDeleteCardContentDialog((CardAccount)datacontext);
        }

        private async void showDeleteAccountContentDialog(Account account) {
            string message = account is BankAccount ? 
                "Czy chcesz usunąć konto bankowe ze wszystkimi kartami? Zostaną usunięte wszystkie operacje skojażone z tym kontem." : 
                "Czy chcesz usunąć konto? Zostaną usunięte wszystkie operacje skojażone z tym kontem.";
            AcceptContentDialog acceptDeleteOperationContentDialog = new AcceptContentDialog(message);
            ContentDialogResult result = await acceptDeleteOperationContentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                deleteAccountWithOperations(account);
        }

        private async void showDeleteCardContentDialog(CardAccount account) {
            AcceptContentDialog acceptDeleteOperationContentDialog = new AcceptContentDialog("Czy chcesz usunąć kartę płatniczą? Zostaną usunięte wszystkie operacje skojażone z tą kartą.");
            ContentDialogResult result = await acceptDeleteOperationContentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                deleteAccountWithOperations(account);
        }

        private void deleteAccountWithOperations(Account account) {
            if (account is CardAccount)
                deleteCardWithOperations((CardAccount)account);
            else
                Accounts.Remove(account);

            if (account is BankAccount)
                AccountsDal.removeBankAccountWithCards(account.Id);
            else
                AccountsDal.removeSingleAccountWithOperations(account.Id);
        }
        private void deleteCardWithOperations(CardAccount cardAccount) {
            BankAccountWithCards bankAccount = (BankAccountWithCards)Accounts.SingleOrDefault(i => i.Id == cardAccount.BankAccountId);
            bankAccount.Cards.Remove(cardAccount);
            AccountsDal.removeSingleAccountWithOperations(cardAccount.Id);
        }

        private void ListViewItem_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e) {
            showFlyout(sender as FrameworkElement);
        }

        private void ListViewItem_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e) {
            showFlyout(sender as FrameworkElement);
        }

        private void showFlyout(FrameworkElement senderElement) {
            FlyoutBase.GetAttachedFlyout(senderElement).ShowAt(senderElement);
        }
    }
}