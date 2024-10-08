﻿using System;
using System.IO;

namespace OpenMcdf.PerfTest
{
    class Program
    {
        static readonly int MAX_STREAM_COUNT = 5000;
        static readonly string fileName = "PerfLoad.cfs";

        static void Main(string[] args)
        {
            File.Delete(fileName);
            if (!File.Exists(fileName))
            {
                CreateFile(fileName);
            }

            CompoundFile cf = new CompoundFile(fileName);
            DateTime dt = DateTime.Now;
            CFStream s = cf.RootStorage.GetStream("Test1");
            TimeSpan ts = DateTime.Now.Subtract(dt);
            Console.WriteLine(ts.TotalMilliseconds.ToString());
            Console.Read();
        }

        private static void CreateFile(string fn)
        {
            CompoundFile cf = new CompoundFile();
            for (int i = 0; i < MAX_STREAM_COUNT; i++)
            {
                cf.RootStorage.AddStream("Test" + i.ToString()).SetData(Helpers.GetBuffer(300));
            }

            cf.SaveAs(fileName);
            cf.Close();
        }
    }
}
