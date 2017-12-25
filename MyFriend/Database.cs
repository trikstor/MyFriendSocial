using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MyFriend
{
    public class Database
    {
        private SQLiteConnection connection { get; set; }

        public Database Connect(string databaseName)
        {
            connection = new SQLiteConnection($"Data Source={AppDomain.CurrentDomain.BaseDirectory}{databaseName}");
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
        
        public bool TryExecuteCmd(string request)
        {
            using (var cmd = new SQLiteCommand(request))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    return false;
                }
            }
            connection.Close();
            return true;
        }

        public void ConnectionClose()
        {
            connection.Close();
        }
    }
}