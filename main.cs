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
                        .Select((c, i) => new
                        {
                            content = (i+1) + Environment.NewLine + c + Environment.NewLine
                        }).Select(o => o.content).ToArray()
                );
            }
        }
    }
}
