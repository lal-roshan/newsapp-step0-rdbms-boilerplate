using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Test.Infrasetup
{
    public class DataAccess:IDisposable
    {
        readonly SqlConnection con= null;
        readonly SqlCommand cmd = null;
        public JObject queries;

        public DataAccess()
        {
            var builddirectory = Environment.CurrentDirectory;
            int testindex = builddirectory.IndexOf("test");
            string rootdir = builddirectory.Substring(0, testindex);
            string filepath = rootdir + "queries.json";
            queries = JObject.Parse(File.ReadAllText(filepath));

            string constr = Environment.GetEnvironmentVariable("MSSQL_URL");
            if (constr == null)
            {
                var file= JObject.Parse(File.ReadAllText("config.json"));
                constr = file.SelectToken("ConnectionString").ToString();
            }
            con = new SqlConnection(constr);
            cmd = new SqlCommand
            {
                Connection = con
            };
            con.Open();
        }

        public void CreateTable(string command)
        {
            cmd.CommandText = command;
            cmd.ExecuteNonQuery();
        }

        public List<ArrayList> GetTableData(string query)
        {
            List<ArrayList> rows = new List<ArrayList>();
            ArrayList list = new ArrayList();
            cmd.CommandText = query;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                int count = 0;
                ArrayList row = new ArrayList();
                while (sdr.FieldCount > count)
                {
                    row.Add(sdr[count]);
                    count++;
                }
                rows.Add(row);
            }
            sdr.Close();
            return rows;
        }

        public int ExecuteCUDQuery(string query)
        {
            cmd.CommandText = query;
            return cmd.ExecuteNonQuery();
        }
        public bool IsTableCreated(string tableName)
        {

            string query = $"select COUNT(*) from sys.tables where is_ms_shipped=0 and name='{tableName}'";
            cmd.CommandText = query;
            int count =(int) cmd.ExecuteScalar();
            return count > 0 ? true : false;
        }
        public void Dispose()
        {
            if (con.State==ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.CommandText = "drop table reminders";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "drop table news";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "drop table userprofile";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "drop table [user]";
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
        }
    }
}
