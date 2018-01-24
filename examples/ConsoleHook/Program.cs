﻿// This code is distributed under MIT license. 
// Copyright (c) 2010-2018 George Mamaladze
// See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleHook
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var quit = new AutoResetEvent(false);

            var selector = new Dictionary<string, Action<AutoResetEvent>>
            {
                {"1. Log keys", LogKeys.Do},
                {"2. Detect key combinations", DetectCombinations.Do},
                {"3. Detect key sequences", DetectSequences.Do},
                {"Q. Quit", Exit}
            };

            Action<AutoResetEvent> action = null;

            while (action == null)
            {
                Console.WriteLine("Please select one of these:");
                foreach (var selectorKey in selector.Keys)
                    Console.WriteLine(selectorKey);
                var ch = Console.ReadKey(true).KeyChar;
                action = selector
                    .Where(p => p.Key.StartsWith(ch.ToString()))
                    .Select(p => p.Value).FirstOrDefault();
            }
            Console.WriteLine("--------------------------------------------------");
            action(quit);
        }


        private static void Exit(AutoResetEvent quit)
        {
            quit.Set();
        }
    }
}