using PoliceHR.Models.SqLiteModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace PoliceHR.ViewModels.SqLiteViewModels
{
    public class SqLiteVm
    {
        public string DbFileName = "LoginSettings.db3";
        public string DBpath
        {
            get
            {
                string DBfolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(DBfolder, DbFileName);
            }
        }
        public SqLiteVm()
        {
            if (!File.Exists(DBpath))
            {
                var db = new SQLiteConnection(DBpath);
                db.CreateTable<Logins>();
                db.CreateTable<ConnString>();
            }
        }
        public ObservableCollection<Logins> UsersSource()
        {
            ObservableCollection<Logins> Source = new ObservableCollection<Logins>();
            var DB = new SQLiteConnection(DBpath);
            var table = DB.Table<Logins>();
            foreach (var s in table)
            {
                Source.Add(new Logins
                {
                    User_Name = s.User_Name,
                    User_ID = s.User_ID,
                    Password = s.Password,
                });
            }
            return Source;
        }
        public string ConnStringSource()
        {
            string Source="";
            var DB = new SQLiteConnection(DBpath);
            var table = DB.Table<ConnString>();
            foreach (var s in table)
            {
                Source = s.ConnactionString;
            }
            return Source;
        }
        public void InsertLogins(string User_Name, string Password)
        {
            var DB = new SQLiteConnection(DBpath);
            var NewLogin = new Logins();
            NewLogin.User_Name = User_Name;
            NewLogin.Password = Password;
            DB.Insert(NewLogin);
        }
        public void UpdateLogins(string User_Name, string Password, int id)
        {
            var db = new SQLiteConnection(DBpath);
            var ULogin = new Logins();
            ULogin.User_Name = User_Name;
            ULogin.Password = Password;
            ULogin.User_ID = id;
            db.Update(ULogin);
        }

        public void InsertConnString(string ConnStringg)
        {
            var DB = new SQLiteConnection(DBpath);
            var NewConnString = new ConnString();
            NewConnString.ConnactionString = ConnStringg;
            DB.Insert(NewConnString);
        }
        public void UpdateConnString(string ConnStringg, int id)
        {
            var db = new SQLiteConnection(DBpath);
            var UConnString = new ConnString();
            UConnString.ConnactionString = ConnStringg;
            UConnString.ConnString_ID = id;
            db.Update(UConnString);
        }

    }
}
