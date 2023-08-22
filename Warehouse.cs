using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FarmCompany
{
    internal class Warehouse
    {
        public int ID { get; set; }
        public int FarmId { get; set;}
        public string WarehouseName { get; set;}

        public Warehouse(int farmId, string warehouseName)
        {
            FarmId = farmId;
            WarehouseName = warehouseName;
        }

        public void CreateWarehouse()
        {
  
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                try
                {
                    string sql = $"insert into dbo.Warehouse OUTPUT inserted.ID values({FarmId}, N'{WarehouseName}')";
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                    ID = (int)cmd.ExecuteScalar();
                    Console.WriteLine("Склад создан");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка!" + "" + ex.Message.ToString());
                }
            }           

        }

        public void DeleteWarehouse(int id)
        {
            if (id > 0)
            {
                using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
                {
                    try
                    {
                        string sql = $"delete from dbo.Warehouse where ID = {id}";
                        sqlConnection.Open();
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Склад удален");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Ошибка!" + "" + ex.Message.ToString());
                    }
                }               
            }
            else
            {
                Console.WriteLine("Введите значение больше 0");
            }
        }
    }
}
