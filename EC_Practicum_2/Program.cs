﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_Practicum_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Experiment(18, "../../le450_15c.txt", 10, "Expiriment 1");
            a.Run();
            Console.ReadLine();
        }
    }
}
