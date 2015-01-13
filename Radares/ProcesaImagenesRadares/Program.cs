using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProcesaImagenesRadares
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Ingrese la ruta de origen que contiene las imágenes: ");
            string originPath = Console.ReadLine();
            Console.Write("\nIngrese la ruta destino donde se guardarán las imágenes procesadas: ");
            string destinyPath = Console.ReadLine();
            Console.Write("\nProcesando imágenes");
            string[] filePaths;

            try
            {
                filePaths = Directory.GetFiles(@originPath, "*.jpg", SearchOption.AllDirectories);
                int i = 0;
                int nExito = 0;
                foreach (string filePath in filePaths)
                {
                    Image srcImg = Image.FromFile(@filePath);
                    if (ProcessImage(Image.FromFile(@filePath), destinyPath + "/" + GetFileName(@filePath), 25))
                    {
                        Console.Write(".");
                        nExito++;
                    }
                    srcImg.Dispose();
                    //Console.WriteLine(filePath);
                    i++;
                }
                Console.WriteLine("\n\nImágenes leídas: " + i.ToString());
                Console.WriteLine("Imágenes procesadas exitósamente: " + nExito.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.WriteLine("\n\nPresione ENTER para salir...");
            Console.Read();
        }

        private static bool ProcessImage(Image srcImage, string destFilePath, int quality)
        {
            try
            {
                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
                EncoderParameters encParams = new EncoderParameters(1);
                encParams.Param[0] = qualityParam;
                srcImage.Save(destFilePath, jpegCodec, encParams);
                srcImage.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string GetFileName(string fullPath)
        {
            int pos = fullPath.LastIndexOf('/');
            if (pos != -1)
            {
                return fullPath.Substring(pos + 1, fullPath.Length - 1);
            }
            else
            {
                pos = fullPath.LastIndexOf('\\');
                if (pos != -1)
                {
                    return fullPath.Substring(pos + 1, fullPath.Length - 1 - pos);
                }
                else
                    return string.Empty;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }    
}
