using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmCompany
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Product(string name)
        {
            Name = name;
        }
        public void CreateProduct()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                string sql = $"insert into dbo.Product OUTPUT inserted.ID values(N'{Name}')";
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                ID = (int)cmd.ExecuteScalar();   
            }

            Console.WriteLine("Товар создан");
        }
        public void DeleteProduct(int id)
        {
            if (id > 0)
            {
                using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
                {
                    try
                    {
                        string sql = $"delete from dbo.Product where ID = {id}";
                        sqlConnection.Open();
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Товар удален");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Ошибка!" + "" + ex.Message);
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
