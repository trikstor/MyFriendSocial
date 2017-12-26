using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MyFriend
{
    public class Database
    {
        private SQLiteConnection connection { get; set; }

        public Database Connect(string databaseName)
        {
            connection = new SQLiteConnection($"Data Source={AppDomain.CurrentDomain.BaseDirectory}{databaseName};Version=3;");
            return this;
        }

        public IEnumerable<SQLiteDataReader> Select(string request)
        {
            using (var cmd = new SQLiteCommand(request, connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader;
                    }
                }
            }
            connection.Close();
        }
        
        public IEnumerable<SQLiteDataReader[]> Select(string[] requests)
        {
            return requests
                .Select(request => Select(request)
                .ToArray());
        }
        
        public bool TryExecuteCmd(string request)
        {
            using (var cmd = new SQLiteCommand(request))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    connection.Close();
                    return false;
                }
            }
            connection.Close();
            return true;
        }

        public bool CheckEquality(string request, string expected)
        {
            return Select(request).First().ToString() == expected;
        }
        
        public void ConnectionClose()
        {
            connection.Close();
        }
    }
}