using System;
using System.Collections.Generic;
using System.Text;

namespace base64Encode
{
    class Program
    {
        public static string EncodeToBase64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        static void Main(string[] args)
        {
            Console.Write("Ingrese texto a codificar:  ");
            string txt = Console.ReadLine();
            Console.WriteLine(EncodeToBase64(txt));
            Console.ReadLine();
        }
    }
}
