using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FarmCompany
{
    internal class Farm
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public Farm(string name, string adress, string phone)
        {
            Name = name;
            Adress = adress;
            Phone = phone;
        }

        public void CreateFarm()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                string sql = $"insert into dbo.Farm OUTPUT inserted.ID values(N'{Name}', N'{Adress}', N'{Phone}')";
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                ID = (int)cmd.ExecuteScalar();
            }

            Console.WriteLine("Аптека создана");
        }
        public void DeleteFarm(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                try
                {
                    string sql = $"delete from dbo.Farm where ID = {id}";
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Аптека удалена");
                }
                catch (Exception ex)
                {
                    {
                        Console.WriteLine("Ошибка!" + "" + ex.Message);
                    }
                }

            }
        }
    }
}
