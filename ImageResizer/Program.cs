using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    static class Program
    {
        //main 變成非同步 會變8ms 但是沒有圖片
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
            //E3-1230v3 邏輯處理器 8核
            //1. 花費時間: 3672 | 2311 ms,37.06%
            //2. 花費時間: 3363 | 2277 ms,32.29%
            //3. 花費時間: 3359 | 2319 ms,30.96%
            //4. 花費時間: 3323 | 2367 ms,28.77%
            //5. 花費時間: 3456 | 2490 ms,27.95%
            //6. 花費時間: 3378 | 2209 ms,34.61%
            //7. 花費時間: 3487 | 2376 ms,31.86%
            //8. 花費時間: 3294 | 2341 ms,28.93%
            //9. 花費時間: 3521 | 2396 ms,31.95%
            //10.花費時間: 3769 | 2266 ms,39.88%
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
