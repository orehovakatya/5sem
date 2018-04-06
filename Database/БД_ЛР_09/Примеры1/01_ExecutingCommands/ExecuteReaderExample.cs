using System;
using System.Data.SqlClient;

public class ExecuteReaderExample
{
   public static void Main(string[] args)
   {
      string source = @"server=.\SQLEXPRESS;integrated security=SSPI;database=Northwind";
      string select = @"SELECT ContactName,CompanyName FROM Customers";
      SqlConnection conn = new SqlConnection(source);
      conn.Open();
      SqlCommand cmd = new SqlCommand(select, conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
         Console.WriteLine("Contact: {0,-20} Company: {1}", rdr[0], rdr[1]);
      }
   }
}
