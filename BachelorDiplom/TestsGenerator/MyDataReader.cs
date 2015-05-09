using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BachelorLibAPI.TestsGenerator
{
    public class MyDataReader
    {
        private List<string> Numbers { get; set; }
        public List<string> Names { get; private set; }
        public List<string> Midnames { get; private set; }
        public List<string> Lastnames { get; private set; }
        public List<string> Dates { get; private set; }

        private readonly Dictionary<Files, string> _filenames = new Dictionary<Files, string>()
        {
            {Files.Numbers, @"..\..\TestsGenerator\Source\Numbers.txt"},
            {Files.Names, @"..\..\TestsGenerator\Source\Names.txt"},
            {Files.Midnames, @"..\..\TestsGenerator\Source\Midnames.txt"},
            {Files.Lastnames, @"..\..\TestsGenerator\Source\Lastnames.txt"},
            {Files.Dates, @"..\..\TestsGenerator\Source\Dates.txt"}
        };

        public void LoadFromFiles()
        {
            //Numbers = ReadFile(Filenames[Files.NUMBERS]);
            Names = ReadFile(_filenames[Files.Names]);
            Midnames = ReadFile(_filenames[Files.Midnames]);
            Lastnames = ReadFile(_filenames[Files.Lastnames]);
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