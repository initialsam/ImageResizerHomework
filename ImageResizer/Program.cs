using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();
            imageProcess.Clean(destPath);
            double orig = GetExecutionTime(()=> imageProcess.ResizeImages(sourcePath, destPath,2.0));

            imageProcess = new ImageProcess();
            imageProcess.Clean(destPath);
            double @new = GetExecutionTime(() => imageProcess.ResizeImagesAsync(sourcePath, destPath, 2.0).Wait());
         
            Console.WriteLine($"花費時間: {orig} | {@new} ms,{((orig - @new) / orig).ToString("P")}");
        }

        private static Double GetExecutionTime(Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            action.Invoke();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}
