using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;

namespace ADO.NET.Lesson4_ProviderFactories
{
    class Program
    {
        static void Main(string[] args)
        {
            string SqlFactory = ConfigurationManager.AppSettings["factory"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(SqlFactory);

            try
            {
                DbConnection connection = factory.CreateConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings
                    ["Access"].ConnectionString;

                DbCommand cmd = factory.CreateCommand();
                cmd.CommandText = ConfigurationManager.AppSettings["newEquipment"];
                cmd.Connection = connection;

                using (connection)
                {
                    connection.Open();
                    DbDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine("Гаражный номер: {0}, SN: {1}",
                            reader["intGarageRoom"], reader["strSerialNo"]);
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            Console.ReadKey();
        }


    }
}
