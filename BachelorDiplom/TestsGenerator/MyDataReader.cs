using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BachelorLibAPI.TestsGenerator
{
    public class MyDataReader
    {
        public List<string> Numbers { get; private set; }
        public List<string> Names { get; private set; }
        public List<string> Midnames { get; private set; }
        public List<string> Lastnames { get; private set; }
        public List<string> Dates { get; private set; }

        private readonly Dictionary<Files, string> Filenames = new Dictionary<Files, string>()
        {
            {Files.NUMBERS, @"..\..\TestsGenerator\Source\Numbers.txt"},
            {Files.NAMES, @"..\..\TestsGenerator\Source\Names.txt"},
            {Files.MIDNAMES, @"..\..\TestsGenerator\Source\Midnames.txt"},
            {Files.LASTNAMES, @"..\..\TestsGenerator\Source\Lastnames.txt"},
            {Files.DATES, @"..\..\TestsGenerator\Source\Dates.txt"}
        };

        public void LoadFromFiles()
        {
            //Numbers = ReadFile(Filenames[Files.NUMBERS]);
            Names = ReadFile(Filenames[Files.NAMES]);
            Midnames = ReadFile(Filenames[Files.MIDNAMES]);
            Lastnames = ReadFile(Filenames[Files.LASTNAMES]);
            //Dates = ReadFile(Filenames[Files.DATES]);
        }

        private List<string> ReadFile(string filename)
        {
            var result = new List<string>();
            using (var file = new StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        var forAdd = line.Split().Where(x => x != "");
                        if(forAdd.Count() != 0)
                            result.Add(forAdd.First());
                    }
                }
            }
            return result;
        }
        
    }
}