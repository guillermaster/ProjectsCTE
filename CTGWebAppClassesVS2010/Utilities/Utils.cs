using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Text.RegularExpressions;

namespace Utilities
{
    public class Utils
    {
        public static string CreateRandomPassword(int passwordLength)
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
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
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string DecodeFrom64(string encodedData)
        {

            byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);
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
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        public static string FormatStringNumber(string number, char decimalPoint, int decimalLong)
        {
            string symbol = decimalPoint.ToString();
            if (number.Length > 0)
            {
                if (!number.Contains(symbol))
                {
                    number += symbol + "00";
                }
                else
                {
                    string[] enteroDecimal = number.Split(decimalPoint);
                    if (enteroDecimal[1].Length < decimalLong)
                    {
                        for (int i = enteroDecimal[1].Length; i < decimalLong; i++)
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

            Font myFont = new Font("Verdana", 10,
                               FontStyle.Bold,
                               GraphicsUnit.Point);

            // Create a graphics object to measure the text's width and height.

            Graphics myGraphics = Graphics.FromImage(bmpImage);

            // This is where the bitmap size is determined.

            iWidth = (int)myGraphics.MeasureString(sImageText, myFont).Width;
            iHeight = (int)myGraphics.MeasureString(sImageText, myFont).Height;

            // Create the bmpImage again with the correct size for the text and font.

            bmpImage = new Bitmap(bmpImage, new Size(iWidth, iHeight));

            // Add the colors to the new bitmap.

            myGraphics = Graphics.FromImage(bmpImage);
            myGraphics.Clear(Color.DarkBlue);
            myGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            myGraphics.DrawString(sImageText, myFont,
                                new SolidBrush(Color.White), 0, 0);
            myGraphics.Flush();

            return (bmpImage);
        }


        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }


        public static Bitmap CreateBarcode(string data)
        {
            Bitmap barCode = new Bitmap(1, 1);

            Font cb128 = new Font("IDAutomationSC128XL", 60, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            Graphics graphics = Graphics.FromImage(barCode);

            SizeF dataSize = graphics.MeasureString(data, cb128);

            barCode = new Bitmap(barCode, dataSize.ToSize());

            graphics = Graphics.FromImage(barCode);

            graphics.Clear(Color.White);
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            graphics.DrawString(data, cb128, new SolidBrush(Color.Black), 0, 0);
            graphics.Flush();

            cb128.Dispose();
            graphics.Dispose();

            return barCode;
        }

        private static double VersionToNumber(string version)
        {
            return Convert.ToDouble(version);
        }

        public static bool BrowserIsSupported(string browserName, string version)
        {
            switch (browserName)
            {
                case "IE":
                    if (VersionToNumber(version) < 7)
                        return false;
                    break;
                case "Firefox":
                    if (VersionToNumber(version) < 1.5)
                        return false;
                    break;
                case "Opera":
                    if (VersionToNumber(version) < 9)
                        return false;
                    break;
                case "Safari":
                    if (VersionToNumber(version) < 3)
                        return false;
                    break;
                case "Chrome":
                    if (VersionToNumber(version) < 2)
                        return false;
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
}