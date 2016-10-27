using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClearUnchangedFile
{
    public class SettingInfo
    {
        public string ClearFolder { get; set; }
        public string CompareFolder { get; set; }
        public IList<string> ForceDeleteFiles { get; set; }
    }

    public class Setting
    {
        public string ClearFolder { get; set; }
        public string CompareFolder { get; set; }
        public IList<string> ForceDeleteFiles { get; set; }


        public SettingInfo GetSettingInfo()
        {
            return LoadSetting(SelectSettingFile(GetSettingFiles()));
        }
        public IList<string> GetSettingFiles()
        {
            var current = new DirectoryInfo(Directory.GetCurrentDirectory());
            return current.GetFiles("*setting.json").Select(o => o.Name).ToList();
        }
        public string SelectSettingFile(IList<string> files)
        {
            var count = files.Count;
            if (count == 0)
            {
                return null;
            }
            if (count == 1)
            {
                return files[0];
            }

            Console.WriteLine("all setting");
            Console.WriteLine();

            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
            Console.WriteLine();
            Console.WriteLine("select setting file , can use regexp");

            while (true)
            {
                Console.WriteLine();
                var select = Console.ReadLine();
                if (select.Length < 3)
                {
                    Console.WriteLine("select key length must greater than 2");
                    continue;
                }

                foreach (var file in files)
                {
                    if (file == select)
                    {
                        return file;
                    }
                }

                var regExp = new Regex(select);
                foreach (var file in files)
                {
                    if (regExp.IsMatch(file))
                    {
                        return file;
                    }
                }
                Console.WriteLine("not match");
            }
        }

        public SettingInfo LoadSetting(string settingFileName)
        {
            Console.WriteLine($"load setting {settingFileName}");

            var json = File.ReadAllText(settingFileName);
            return (SettingInfo)JsonConvert.DeserializeObject(json, typeof(SettingInfo));
        }
    }



}
