using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;
using AccesoDatos;

namespace Hash
{
    class Program
    {
        //private MachineKeySection machineKey;

        private static byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        private static string EncodePassword(string password)
        {
            string encodedPassword = password;
            try
            {
                HMACSHA1 hash = new HMACSHA1();
                //hash.Key = HexToByte(machineKey.ValidationKey);
                hash.Key = HexToByte("1AFBCE4D2380DC3E7819AF234A8948DF25000F2AD5E1DA112900F0F0F0A0");
                encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return encodedPassword;

        }


        static void Main(string[] args)
        {
            Console.Write("Database: ");
            string dbName = Console.ReadLine();

            Console.Write("DB User: ");
            string dbUser = Console.ReadLine();

            Console.Write("Password: ");
            string dbPassword = Console.ReadLine();
            
            /*while (password != "exit")
            {
                password = Console.ReadLine();
                Console.WriteLine(EncodePassword(password));
            }*/

            try
            {
                ROracle oDatos = new ROracle(dbUser, dbPassword, dbName);
                Console.Write("Nombre de tabla de usuarios: ");
                string tablename = Console.ReadLine();
                if (oDatos.EjecutarQuery("SELECT usu_cuent, usu_clave FROM " + tablename))
                {
                    int counter = 0;
                    int counterFailed = 0;
                    string failed = "";
                    ROracle oDatos2 = new ROracle(dbUser, dbPassword, dbName);
                    while (oDatos.oDataReader.Read())
                    {
                        string usu_cuent = oDatos.oDataReader.GetString(0);
                        Console.Write("Usuario: " + usu_cuent);
                        string enc_pwd = EncodePassword(oDatos.oDataReader.GetString(1));
                        if (oDatos2.EjecutarQuery("UPDATE " + tablename + " SET usu_clave='" + enc_pwd + "' WHERE usu_cuent='" + usu_cuent + "'"))
                        {
                            Console.WriteLine("   Password: " + enc_pwd);
                            counter++;
                        }
                        else
                        {
                            Console.WriteLine("");
                            counterFailed++;
                            failed += usu_cuent + "  ";
                        }
                    }
                    oDatos2.Dispose();
                    Console.WriteLine("\n" + counter.ToString() + " registros afectados.");
                    Console.WriteLine(counterFailed.ToString() + " registros fallidos   " + failed);
                }
                
                Console.WriteLine("\n Presione ENTER para salir");
                oDatos.Dispose();
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("\n Presione ENTER para salir");
                Console.Read();
            }
        }
        
    }
}
