using Dapper;
//using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace SQLLite
{
    public class SqliteDataAccess
    {
        public static List<PersonModel> LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PersonModel>("select * from Person", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void LoadData()
        {
            try
            {
                using (var conn = new SQLiteConnection(LoadConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT ID FROM Person WHERE ID='@ID'",conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", '1');
                        using (var reader = cmd.ExecuteReader())
                        {
                            var count = 0;
                            while (reader.Read())
                            {
                                count += 1;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public static bool LogIn(PersonModel person)
        {
            var count = 0;
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();                
                
                    using (var cmd = new SQLiteCommand("SELECT Username FROM Person WHERE Username='@Username'", cnn))
                    {
                        cmd.Parameters.AddWithValue("@Username", person.Username);
                        using (var reader = cmd.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                count += 1;
                            }

                        }

                    }
                cnn.Close();
                }

            return count == 1;
            
        }

        public static void SavePerson(PersonModel person)
        {
            int i = 0;
            
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                using (SQLiteTransaction transaction = cnn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(cnn))
                    {
                        //SQLiteParameter IDParam = new SQLiteParameter();
                        //SQLiteParameter NameParam = new SQLiteParameter();
                      

                        cmd.CommandText = "INSERT into Person (FirstName, Surname, Username) VALUES (@FirstName, @Surname, @Username)";
                        //cmd.Parameters.AddWithValue("ID", 2);
                        cmd.Parameters.AddWithValue("FirstName", person.FirstName);
                        cmd.Parameters.AddWithValue("Surname", person.LastName);
                        cmd.Parameters.AddWithValue("Username", person.Username);
                        

                        i = cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    //cnn.Execute("insert into Person (ID, Name) values (1, 'Lewis')", person)                    
                    
                }
                cnn.Close();
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
