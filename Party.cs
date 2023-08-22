using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FarmCompany
{
    internal class Party
    { 
        public int ID { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }

        public Party(int productId, int warehouseId, int quantity)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
            Quantity = quantity;
        }

        public void CreateParty()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                try
                {
                    string sql = $"insert into dbo.Party OUTPUT inserted.ID values({ProductId}, {WarehouseId}, {Quantity})";
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                    ID = (int)cmd.ExecuteScalar();
                    Console.WriteLine("Партия создана");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка!" + "" + ex.Message.ToString());                  
                }                
            }          
        }
        
        public void DeleteParty(int id)
        {
            if (id > 0)
            {
                using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
                {
                    try
                    {
                        string sql = $"delete from dbo.Party where ID = {id}";
                        sqlConnection.Open();
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Партия удалена");
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
