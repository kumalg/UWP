﻿using System.Net.Sockets;
using Finanse.Models.MAccounts;
using Finanse.Models.Operations;

namespace Finanse.DataAccessLayer {
    using Models.Categories;
    using Models.Helpers;
    using Models.MoneyAccounts;
    using SQLite.Net;
    using SQLite.Net.Platform.WinRT;
    using System.IO;
    using System.Linq;
    using Windows.Storage;

    public class DalBase {
        public static string DbOneDriveName = "dbOneDrive.sqlite";
        private static string _dbPath = string.Empty;
        public static string DbPath {
            get {
                if (string.IsNullOrEmpty(_dbPath))
                    _dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");
                return _dbPath;
            }
        }

        public static string DbPathLocalFromFileName(string fileName) => Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);

        protected static SQLiteConnection DbConnection => new SQLiteConnection(new SQLitePlatformWinRT(), DbPath);

        protected static SQLiteConnection DbConnectionFromPath(string path) => new SQLiteConnection(new SQLitePlatformWinRT(), path);

        public static void CreateDb() {

            using (var db = DbConnection) {
                // Activate Tracing
                db.Execute("PRAGMA foreign_keys = ON");
                db.TraceListener = new DebugTraceListener();

                /*
                db.Execute("CREATE TABLE IF NOT EXISTS images ( "
                    + "nameRed VARCHAR(20) NOT NULL PRIMARY KEY,"
                    + "patientID INT,"
                    + "FOREIGN KEY(patientID) REFERENCES patients(id) ) ");*/

                // db.CreateTable<MoneyAccount>();

                var operationCategory = db.ExecuteScalar<string>("SELECT name FROM sqlite_master WHERE name='OperationCategory'");
                if (!string.IsNullOrEmpty(operationCategory))
                    db.Execute("ALTER TABLE OperationCategory RENAME TO Category");

                var operationSubCategory = db.ExecuteScalar<string>("SELECT name FROM sqlite_master WHERE name='OperationSubCategory'");
                if (!string.IsNullOrEmpty(operationSubCategory))
                    db.Execute("ALTER TABLE OperationSubCategory RENAME TO SubCategory");

                db.CreateTable<Operation>();
                db.CreateTable<OperationPattern>();
                db.CreateTable<Category>();
                db.CreateTable<SubCategory>();
                db.CreateTable<CashAccount>();
                db.CreateTable<CardAccount>();
                db.CreateTable<BankAccount>();
                db.CreateTable<MoAccount>();

                ConvertAccounts(db);

                CheckSyncColumns(db, "Operation");
                CheckSyncColumns(db, "OperationPattern");
                CheckSyncColumns(db, "Category");
                CheckSyncColumns(db, "SubCategory");
                CheckSyncColumns(db, "MoAccount");

                db.Execute("UPDATE Category SET CantDelete = 1 WHERE Id = 1");
                db.Execute("UPDATE Category SET CantDelete = 0 WHERE CantDelete ISNULL");
                db.Execute("UPDATE SubCategory SET CantDelete = 0 WHERE CantDelete ISNULL");

                db.Execute(AccountQueries.SeqTriggerCashAccount);
                db.Execute(AccountQueries.SeqTriggerBankAccount);
                db.Execute(AccountQueries.SeqTriggerCardAccount);

                if (!db.ExecuteScalar<bool>("SELECT * FROM Category WHERE Id = 1 LIMIT 1")) {
                    db.Execute("INSERT INTO Category (Id) VALUES (1)");
                    db.Update(new Category { Id = 1, Name = "Inne", ColorKey = "14", IconKey = "FontIcon_2", VisibleInIncomes = true, VisibleInExpenses = true, CantDelete = true });
                }

                db.Execute("INSERT INTO sqlite_sequence (name, seq) SELECT 'Account', 0 WHERE NOT EXISTS(SELECT 1 FROM sqlite_sequence WHERE name = 'Account')");

                db.Execute("UPDATE Operation SET LastModifed = SUBSTR(LastModifed,1,11) || REPLACE(SUBSTR(LastModifed,12),'.',':')");

                if (db.ExecuteScalar<int>("SELECT seq FROM sqlite_sequence WHERE name = 'Category'") == 1) {
               //     db.Insert(new Category { Id = 1, Name = "Inne", ColorKey = "14", IconKey = "FontIcon_2", VisibleInIncomes = true, VisibleInExpenses = true, CantDelete = true});
                    db.Insert(new Category { Id = 2, Name = "Jedzenie", ColorKey = "04", IconKey = "FontIcon_6", VisibleInExpenses = true, VisibleInIncomes = true });
                    db.Insert(new Category { Id = 3, Name = "Rozrywka", ColorKey = "12", IconKey = "FontIcon_20", VisibleInIncomes = false, VisibleInExpenses = true });
                    db.Insert(new Category { Id = 4, Name = "Rachunki", ColorKey = "08", IconKey = "FontIcon_21", VisibleInIncomes = false, VisibleInExpenses = true });
                    db.Insert(new Category { Id = 5, Name = "Prezenty", ColorKey = "05", IconKey = "FontIcon_13", VisibleInIncomes = true, VisibleInExpenses = true });
                    db.Insert(new Category { Id = 6, Name = "Praca", ColorKey = "14", IconKey = "FontIcon_9", VisibleInIncomes = true, VisibleInExpenses = false});

                    db.Insert(new SubCategory { Id = 1, Name = "Prąd", ColorKey = "07", IconKey = "FontIcon_19", BossCategoryId = 4, VisibleInIncomes = false, VisibleInExpenses = true });
                    db.Insert(new SubCategory { Id = 2, Name = "Imprezy", ColorKey = "11", IconKey = "FontIcon_17", BossCategoryId = 3, VisibleInIncomes = false, VisibleInExpenses = true });
                }

                if (!(db.Table<CashAccount>().Any() || db.Table<BankAccount>().Any())) {
                    AccountsDal.AddAccount(new CashAccount { Name = "Gotówka", ColorKey = "01" });
                    AccountsDal.AddAccount(new BankAccount { Name = "Konto bankowe", ColorKey = "02", });
                    AccountsDal.AddAccount(new CardAccount { Name = "Karta", ColorKey = "03", BankAccountId = db.ExecuteScalar<int>("SELECT Id FROM BankAccount LIMIT 1")});
                }

                ConvertLocalIdReferenceToGlobal(db);
            }
        }

        private static void ConvertLocalIdReferenceToGlobal(SQLiteConnection db) {
            var subMoAccounts = db.Query<SubMAccount>("SELECT * FROM MoAccount WHERE BossAccountGlobalId IS NOT NULL AND IsDeleted = 0");

            foreach (var subMoAccount in subMoAccounts) {
                var bossMoAccount =
                    db.Query<MAccount>("SELECT * FROM MoAccount WHERE Id = ? LIMIT 1", subMoAccount.BossAccountGlobalId)
                        .FirstOrDefault();

                if (bossMoAccount != null)
                    db.Execute("UPDATE MoAccount SET BossAccountGlobalId = ? WHERE Id = ?", bossMoAccount.GlobalId, subMoAccount.Id);
            }

            /*
            var subCategories = db.Query<SubCategory>("SELECT * FROM SubCategory WHERE IsDeleted = 0");

            foreach (var subCategory in subCategories) {
                var bossMoAccount =
                    db.Query<MAccount>("SELECT * FROM MoAccount WHERE Id = ? LIMIT 1", subCategory.BossCategoryId)
                        .FirstOrDefault();

                if (bossMoAccount != null)
                    db.Execute("UPDATE MoAccount SET BossAccountGlobalId = ? WHERE Id = ?", bossMoAccount.GlobalId, subCategory.Id);
            }*/
        }

        private static void CheckSyncColumns(SQLiteConnection db, string tableName) {
                db.Execute("UPDATE " + tableName + " SET GlobalId = ? || '_' || Id WHERE GlobalId ISNULL", Informations.DeviceId);
                db.Execute("UPDATE " + tableName + " SET LastModifed = ? WHERE LastModifed ISNULL", DateTimeOffsetHelper.DateTimeOffsetNowString);
                db.Execute("UPDATE " + tableName + " SET IsDeleted = 0 WHERE IsDeleted ISNULL");
        }

        private static void ConvertAccounts(SQLiteConnection db) {
            var bankAccounts = db.Query<BankAccount>("SELECT * FROM BankAccount");
            var cashAccounts = db.Query<CashAccount>("SELECT * FROM CashAccount");
            var cardAccounts = db.Query<CardAccount>("SELECT * FROM CardAccount");

            foreach (var bankAccount in bankAccounts) {
                Account account = db.Query<BankAccount>("SELECT * FROM MoAccount WHERE Id = ? LIMIT 1", bankAccount.Id).FirstOrDefault();
                if (account == null)
                    db.Execute("INSERT INTO MoAccount (Id, Name, ColorKey, IsDeleted) VALUES(?, ?, ?, 0)", bankAccount.Id, bankAccount.Name, bankAccount.ColorKey);
            }

            foreach (var cashAccount in cashAccounts) {
                Account account = db.Query<CashAccount>("SELECT * FROM MoAccount WHERE Id = ? LIMIT 1", cashAccount.Id).FirstOrDefault();
                if (account == null)
                    db.Execute("INSERT INTO MoAccount (Id, Name, ColorKey, IsDeleted) VALUES(?, ?, ?, 0)", cashAccount.Id, cashAccount.Name, cashAccount.ColorKey);
            }

            foreach (var cardAccount in cardAccounts) {
                Account account = db.Query<CardAccount>("SELECT * FROM MoAccount WHERE Id = ? LIMIT 1", cardAccount.Id).FirstOrDefault();
                if (account == null)
                    db.Execute("INSERT INTO MoAccount (Id, Name, ColorKey, BossAccountGlobalId, IsDeleted) VALUES(?, ?, ?, ?, 0)", cardAccount.Id, cardAccount.Name, cardAccount.ColorKey, cardAccount.BankAccountId);
            }
        }
        /*
        public static async Task CreateDatabase() {
            // Create a new connection
            using (var db = DbConnection) {
                // Activate Tracing
                db.TraceListener = new DebugTraceListener();

                // Create the table if it does not exist
                var c = db.CreateTable<Operation>();
                var info = db.GetMapping(typeof(Operation));

            }
        }
        */
    }
}