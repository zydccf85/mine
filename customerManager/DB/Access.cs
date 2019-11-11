using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using System.IO;
using System.Windows;
using Dos.ORM;
namespace CustomerManager.DB
{
    public class Access<T> where T:Entity
    {


        public string DBPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Test.mdb");

        public IDbConnection Conn;
        public Access()
        {
            Conn = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;" + "Data Source=" + DBPath);

        }
        public List<T> GetList(string sql,object obj)
        {
           // return DB.Content.From<T>().Where
            return Conn.Query<T>(sql,obj).ToList();
        }
        public bool Delete(T t)
        { 
            return Conn.Delete<T>(t);
        }
        public long Insert(T t)
        {
            return Conn.Execute("insert into [customer](Name,Sid) values(@Name,@Sid)", t);
            //return Conn.Insert<T>(t);
        }
        public int Update(string sql)
        {
           
            return Conn.Execute(sql);
            //bool flag = Conn.Update<T>(t);
            //return flag ? 1 : -1;
        }
    }
}
