﻿using System;

namespace ManageConnection
{
    public class CustomConnection
    {
        public CustomConnection()
        {

        }

        string _serveur = "serveur";
        string _database = "database";
        string _user = "user";
        string _password = "password";
        string _path = "path";
        int _port = 0;

        public string Serveur
        {
            get
            {
                return _serveur;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidOperationException("Please specify a valid Server.");
                else
                    _serveur = value;
            }
        }

        public string Database
        {
            get
            {
                return _database;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidOperationException("Please specify a valid Database.");
                else
                    _database = value;
            }
        }

        public string User
        {
            get
            {
                return _user;
            }

            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new InvalidOperationException("Please specify a valid Username.");
                else
                    _user = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidOperationException("Please specify a valid Password.");
                else
                    _password = value;
            }
        }

        public int Port
        {
            get
            {
                return _port;
            }

            set
            {
                if(value <= 0)
                    throw new InvalidOperationException("Please specify a valid Port number.");
                else
                    _port = value;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidOperationException("Please specify a valid Path.");
                else
                    _path = value;
            }
        }
    }
}