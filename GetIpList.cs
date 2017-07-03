using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Collections.Generic;

namespace ConsoleApplication7
{
    class Program
    {
        private static List<string> ipImagesNumbers = new List<string>();
        private static List<Bitmap> numbersImages   = new List<Bitmap>();
        private static List<string> ports           = new List<string>();
        
        static void Main(string[] args)
        {
            
            WebClient client = new WebClient();
            string source = client.DownloadString("https://www.torvpn.com/en/proxy-list");

            for (int i = 0; i < 11; i++)
                numbersImages.Add(Bitmap.FromFile(@"C:\Numbers\" + i + ".png") as Bitmap);
            foreach(Match m in Regex.Matches(source, "proxy-visible-(.*)\\.png\" alt=\"IP"))
                ipImagesNumbers.Add(m.Groups[1].Value);
            foreach(Match m in Regex.Matches(source, "<td>(\\d{2,4})<\\/td>"))
                ports.Add(m.Groups[1].Value);

            foreach (string n in ipImagesNumbers)
            {
                client.DownloadFile("https://www.torvpn.com/proxypic/proxy-visible-" + n + ".png", @"C:\proxy\" + n + ".png");
                Bitmap bitmap = (Bitmap)Bitmap.FromFile(@"C:\proxy\" + n + ".png");
                string ip = "";
                for(int i=0; i<15; i++)
                {
                    Bitmap bit = new Bitmap(bitmap);
                    Rectangle rect = new Rectangle(3 + (i * 8), 0, 8, bit.Height);
                    bit = bit.Clone(rect, bit.PixelFormat);
                    if (ImagesEquals(numbersImages[0], bit))
                        ip += "0";
                    else if (ImagesEquals(numbersImages[1], bit))
                        ip += "1";
                    else if (ImagesEquals(numbersImages[2], bit))
                        ip += "2";
                    else if (ImagesEquals(numbersImages[3], bit))
                        ip += "3";
                    else if (ImagesEquals(numbersImages[4], bit))
                        ip += "4";
                    else if (ImagesEquals(numbersImages[5], bit))
                        ip += "5";
                    else if (ImagesEquals(numbersImages[6], bit))
                        ip += "6";
                    else if (ImagesEquals(numbersImages[7], bit))
                        ip += "7";
                    else if (ImagesEquals(numbersImages[8], bit))
                        ip += "8";
                    else if (ImagesEquals(numbersImages[9], bit))
                        ip += "9";
                    else if (ImagesEquals(numbersImages[10], bit))
                        ip += ".";
                    else
                        continue;

                }
                Console.WriteLine(ip);
                Console.Read();
            }
        }

        public static bool ImagesEquals(Bitmap b1, Bitmap b2)
        {
            int correct = 0, incorrect = 0;
            for(int i=0; i < b1.Width; i++)
                for(int k=0; k<b1.Height; k++)
                {
                    if (b1.GetPixel(i, k) == b2.GetPixel(i, k))
                        correct++;
                    else
                        incorrect++;
                }
            return correct/(correct + incorrect) == 1;
        }
    }
}
