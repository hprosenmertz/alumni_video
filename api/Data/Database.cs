using System;
using System.Collections.Generic;
using System.Dynamic;
using MySql.Data.MySqlClient;

namespace api.Data
{
    public class Database
    {
        public string ConnString {get; set;}

        public MySqlConnection Conn {get; set;}

        public Database(){
            // string server = Environment.GetEnvironmentVariable("alumni_database_server");
            // string name = Environment.GetEnvironmentVariable("alumni_database_name");
            // string port = Environment.GetEnvironmentVariable("alumni_database_port");
            // string username = Environment.GetEnvironmentVariable("alumni_database_username");
            // string password = Environment.GetEnvironmentVariable("alumni_database_password");


            string server = "tvcpw8tpu4jvgnnq.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string name =("pyzkm2hiy735lro9");
            string port = ("3306");
            string username = ("m7u7tqnkafs8m4df");
            string password = ("z0vknjsuu2bg19cw");


            System.Console.WriteLine(name);
            System.Console.WriteLine(port);
            System.Console.WriteLine(username);
            System.Console.WriteLine(password);
            System.Console.WriteLine("Got the database " + server);

            this.ConnString = $@"server = {server}; user={username};database={name}; port={port};password={password}; ";
            this.Conn = new MySqlConnection(this.ConnString);
        }

        public void Open(){
            this.Conn.Open();
        }

        public void Close(){
            this.Conn.Close();
        }

        public List<ExpandoObject> Select(string query){    //Expaando object = objects not tied to actual class
            //send in sql in query

            List<ExpandoObject> results = new();
            try{
                using var cmd = new MySqlCommand(query, this.Conn); //connection from above
                using var rdr = cmd.ExecuteReader();
                while(rdr.Read()){
                    var temp = new ExpandoObject() as IDictionary<string, Object>;
                    for(int i = 0; i<rdr.FieldCount; i++){
                        temp.TryAdd(rdr.GetName(i), rdr.GetValue(i));
                    }
                    results.Add((ExpandoObject)temp);
                }
            }
            catch (Exception e){
                System.Console.WriteLine("Select Query Error");
                System.Console.WriteLine(e.Message);
            }
            return results;
        }

        public void Insert(string query, Dictionary<string, object> values){
            QueryWithData(query, values);
        }

        public void Update(string query, Dictionary<string, object> values){
            QueryWithData(query, values);
        }

        private void QueryWithData(string query, Dictionary<string, object> values){
            try{
                using var cmd = new MySqlCommand(query, this.Conn);
                foreach(var p in values){
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e){
                System.Console.WriteLine("Error Inserting Data");
                System.Console.WriteLine(e.Message);
            }
        }
        
    }
}