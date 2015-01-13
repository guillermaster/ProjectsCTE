using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Text.RegularExpressions;


namespace Utilities
{
    public class Utils
    {
        public static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public static string EncodeToBase64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string DecodeFrom64(string encodedData)
        {

            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;

        }

        public static bool CreateTextFileAppend(string filename, string content)
        {
            //try
            //{
            //    File.AppendAllText(filename, content);
            //    //SW.WriteLine(content);
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}

            //return true;
            try
            {

                if (File.Exists(filename))
                {
                    File.AppendAllText(filename, content);
                }
                else
                {
                    File.WriteAllText(filename, content);
                }
            }
            catch (IOException ex)
            {
                return false;
            }
            return true;
        }

        public static string FormatStringNumber(string number, char decimal_point, int decimal_long)
        {
            string symbol = decimal_point.ToString();
            if (number.Length > 0)
            {
                if (!number.Contains(symbol))
                {
                    number += symbol + "00";
                }
                else
                {
                    string[] entero_decimal = number.Split(decimal_point);
                    if (entero_decimal[1].Length < decimal_long)
                    {
                        for (int i = entero_decimal[1].Length; i < decimal_long; i++)
                        {
                            number += 0;
                        }
                    }
                }
            }
            return number;
        }

        public static Bitmap CreateTextImage(string sImageText)
        {
            Bitmap bmpImage = new Bitmap(1, 1);

            int iWidth = 0;
            int iHeight = 0;

            // Create the Font object for the image text drawing.

            Font MyFont = new Font("Verdana", 10,
                               System.Drawing.FontStyle.Bold,
                               System.Drawing.GraphicsUnit.Point);

            // Create a graphics object to measure the text's width and height.

            Graphics MyGraphics = Graphics.FromImage(bmpImage);

            // This is where the bitmap size is determined.

            iWidth = (int)MyGraphics.MeasureString(sImageText, MyFont).Width;
            iHeight = (int)MyGraphics.MeasureString(sImageText, MyFont).Height;

            // Create the bmpImage again with the correct size for the text and font.

            bmpImage = new Bitmap(bmpImage, new Size(iWidth, iHeight));

            // Add the colors to the new bitmap.

            MyGraphics = Graphics.FromImage(bmpImage);
            MyGraphics.Clear(Color.DarkBlue);
            MyGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            MyGraphics.DrawString(sImageText, MyFont,
                                new SolidBrush(Color.White), 0, 0);
            MyGraphics.Flush();

            return (bmpImage);
        }


        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }


    }
}
