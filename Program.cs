using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileReadProg
{
    class Program
    {
        private const string InputPath = @"c:\CMS\input.txt";
        private const string OutputPath = @"c:\CMS\output.txt";
        public class AppList
        {
            public string AppName { get; set; }
            public string ApiName { get; set; }
            public string Version { get; set; }

        }
        static void Main(string[] args)
        {
            string line;

            List<AppList> appLists = new List<AppList>();

            StreamReader file =
                new StreamReader(InputPath);
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split(',');
                appLists.Add(new AppList()
                {
                    AppName = data[0],
                    ApiName = data[1],
                    Version = data[2]
                });
            }
            var list = appLists.GroupBy(student => student.ApiName)
                       .Select(group =>
                             new
                             {
                                 ApiName = group.Key,
                                 AppList = group.OrderByDescending(x => x.Version).Take(1).ToList()
                             })
                       .OrderBy(group => group.AppList.First().Version);

            var outputList = list.Select(s => s.AppList);

            using (TextWriter tw = new StreamWriter(OutputPath))
            {
                foreach (var lst in outputList)
                {
                    foreach (var item in lst)
                    {
                        tw.WriteLine(string.Format("AppName: {0} , ApiName: {1} , Version: {2}", item.AppName, item.ApiName, item.Version));
                    }
                }
            }


            file.Close();
        }
    }
}
