﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaksAndValleys
{
    /// <summary>
    /// Used to hold an index, X coord, and Y coord of each data point within the dataset.
    /// </summary>
    public class DataPoint
    {
        public DataPoint(int index, double x, double y) 
        {
                Index = index;
                X = x;
                Y = y;
        }
        public int Index { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
