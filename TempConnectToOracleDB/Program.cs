using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

using System.Text;

using System.Data.SqlClient;
namespace TempConnectToOracleDB
{
    class Program
    {
       
        static void Main(string[] args)
        {
            string oradb = "User Id=tolya1; Password=123456; Data Source = XE;";
            OracleConnection conn = new OracleConnection(oradb); 
            conn.Open();


            OracleCommand commUpdate = new OracleCommand();
            commUpdate.Connection = conn;
            commUpdate.CommandType = CommandType.Text;

            OracleCommand commSelect = new OracleCommand();
            commSelect.Connection = conn;
            commSelect.CommandType = CommandType.Text;

            OracleCommand commDelete = new OracleCommand();
            commDelete.Connection = conn;
            commDelete.CommandType = CommandType.Text;

            OracleCommand commInsert = new OracleCommand();
            commInsert.Connection = conn;
            commInsert.CommandType = CommandType.Text;

            OracleCommand commDrop = new OracleCommand();
            commDrop.Connection = conn;
            commDrop.CommandType = CommandType.Text;

            OracleCommand commCreate = new OracleCommand();
            commCreate.Connection = conn;           
            commCreate.CommandType = CommandType.Text;

            commCreate.CommandText =    " CREATE TABLE Persons " +
                                    " ( " +
                                    "  ID  NUMBER not null primary key, " +
                                        " Name varchar2(50) not null, " +
                                        " City varchar2(50) " + 
                                    " ) ";

              
            
            commCreate.ExecuteNonQuery();
            
            commInsert.Parameters.Add(new OracleParameter("Name", "varchar2")).Value = "Nico";
            commInsert.Parameters.Add(new OracleParameter("City", "varchar2")).Value = "Ternopil";
            commInsert.CommandText = "INSERT INTO Persons (ID, Name, City) VALUES (1,:Name, :City)";
            commInsert.ExecuteNonQuery();

            commUpdate.Parameters.Add(new OracleParameter("NewName", "varchar2")).Value = "NICONEW";
            commUpdate.Parameters.Add(new OracleParameter("Name", "varchar2")).Value = "NICO";
            commUpdate.CommandText = "UPDATE Persons SET Persons.Name = :NewName WHERE Persons.Name =  :Name";
            commUpdate.ExecuteNonQuery();

            commSelect.Parameters.Add(new OracleParameter("City", "varchar2")).Value = "Ternopil";
            commSelect.CommandText = "SELECT Name FROM Persons WHERE City = :City";

            OracleDataReader reader = commSelect.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["Name"]);
            }

            reader.Close();
           
            commDelete.Parameters.Add(new OracleParameter("City", "varchar2")).Value = "Ternopil";
            commDelete.CommandText = "DELETE FROM Persons WHERE City = :City";
            commDelete.ExecuteNonQuery();
            
            commDrop.CommandText = "DROP TABLE Persons";
            commDrop.ExecuteNonQuery();
            
            conn.Close();
           
     
            Console.WriteLine("-_-");
            Console.Read();

        }
    }
}
