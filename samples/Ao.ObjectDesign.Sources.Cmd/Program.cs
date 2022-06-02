using Ao.ObjectDesign.Sources.Sql;
using Microsoft.Data.Sqlite;
using SqlKata;
using SqlKata.Compilers;
using System;

namespace Ao.ObjectDesign.Sources.Cmd
{
    internal class Program
    {
        static void Main(string[] args)
        {
           var q = new Query("students")
                .Where("id", 1)
                .Limit(10);
            var ds = new SqliteConnectionStringBuilder();
            ds.DataSource = "students.db";
            var sqlite = new SqliteConnection(ds.ConnectionString);
            var p = new SqlDataProvider<Student>(q, new SqliteCompiler(), sqlite);
            var dt = p.Get();
            Console.WriteLine(dt.Count);
        }
    }
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}