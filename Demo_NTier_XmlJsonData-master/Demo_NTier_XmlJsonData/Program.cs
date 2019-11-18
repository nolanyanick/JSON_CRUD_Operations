﻿using Demo_NTier_XmlJsonData.DataAccessLayer;
using Demo_NTier_XmlJsonData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_NTier_XmlJsonData.PresentationLayer;

/// <summary>
/// Demo app for XML and Json serialization
/// </summary>
namespace Demo_NTier_XmlJsonData
{
    class Program
    {
        private enum DataType
        {
            XML,
            JSON
        }

        static void Main(string[] args)
        {
            ConsoleView consoleView = new ConsoleView();
        }
    }
}
