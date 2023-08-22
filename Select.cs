using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmCompany
{
    internal class Select
    {
        public void SelectProduct(int id)
        {
            if (id > 0)
            {
                using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
                {
                    DataSet product = new DataSet();
                    DataTable dt = new DataTable();
                    product.Tables.Add(dt);
                    dt.Columns.Add(new DataColumn("Name"));
                    dt.Columns.Add(new DataColumn("Count"));
                    try
                    {
                        string sql = $"select p.name, SUM(pr.quantity) from Product p\r\n" +
                            $"join FarmProduct fp ON p.ID = fp.productId" +
                            $"\r\njoin Farm f ON f.ID = fp.farmId" +
                            $"\r\njoin Warehouse w ON w.farmId = f.ID" +
                            $"\r\njoin Party pr ON pr.warehouseId = w.Id" +
                            $"\r\nwhere f.ID = {id}" +
                            $"\r\ngroup by p.name";
                        sqlConnection.Open();
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            dt.Rows.Add(reader.GetString(0), (int)reader.GetInt32(1));
                        }
                        Console.WriteLine("Name\t             Count");
                        foreach (DataRow dr in dt.Rows)
                        {

                            string name = dr["Name"].ToString();
                            int count = Convert.ToInt32(dr["Count"]);
                            Console.WriteLine("{0}\t     {1}", name, count);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка!" + "" + ex.Message.ToString());
                    }
                }
            }
        }
    }
}
