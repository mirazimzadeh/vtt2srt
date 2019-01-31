using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Mironline.vtt2srt
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.vtt").Select(o=>o.ToLower()))
            {
                File.WriteAllLines(file.Replace(".vtt", ".srt"),
                    File.ReadAllText(file, Encoding.UTF8)
                        .Split(new[] {"\n\n"}, StringSplitOptions.None)
                        .Where(o => o != @"WEBVTT")
                        .Select((c, i) => new {
                            content = CorrectFormat(i,c)
                        }).Select(o => o.content).ToArray()
                );
            }
        }

        static string CorrectFormat(int index , string lineString)
        {
            var lines = lineString.Split('\n');
            
            var times = lines[0].Split(new[] {"-->"}, StringSplitOptions.None);

            return (index+1)  
                      + Environment.NewLine + ("00:" + times[0].Trim() + " --> " + "00:" + times[1].Trim()).Replace(".", ",")
                      + Environment.NewLine + lines[1] + Environment.NewLine;
                ;
        }
    }
}
