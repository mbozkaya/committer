using Commiter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Commiter
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        public static string BachtFileName { get; set; } = "commit.bat";
        static void Main(string[] args)
        {
            //Get();
            AppendChanges();
            Commit();


            Console.ReadKey();
        }

        public async static void Get()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://api.github.com/users/mbozkaya/events");

            var msg = await stringTask;
            List<GitHub.GetEvent> deserialized = JsonConvert.DeserializeObject<List<GitHub.GetEvent>>(msg);

            var pushEvents = deserialized.Where(w => w.type == "PushEvent" && w.CreatedDate.Date == DateTime.Now.Date).ToList();

            foreach (var push in pushEvents)
            {
                push.payload.commits.ForEach(f2 => Console.WriteLine(f2.message));
            }

        }

        public static void Commit()
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = $"{GetApplicationRoot()}\\{BachtFileName}";
            proc.StartInfo.WorkingDirectory = $"{Directory.GetCurrentDirectory()}";
            //proc.StartInfo.Arguments = "start /wait commit.bat ";
            proc.StartInfo.Arguments = "test123 ";
            proc.Start();

        }

        public static void Test()
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = $"{GetApplicationRoot()}\\test.bat";
            proc.StartInfo.WorkingDirectory = $"{Directory.GetCurrentDirectory()}";
            proc.StartInfo.Arguments = $"start /wait {"test"} {"24324"}";
            proc.Start();

        }


        public static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                              .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }

        //public static int GetCommitCount()
        //{
        //}

        public static void AppendChanges()
        {
            File.AppendAllText(@"c:\Repository\javascript30.com-Tutorials\readme.md", $"text content {DateTime.Now.ToShortTimeString()}" + Environment.NewLine);
        }
    }
}
