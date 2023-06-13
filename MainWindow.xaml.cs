using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using CsvHelper;

namespace PeaksAndValleys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Lists for holding data points, and displaying in the window.
        public List<DataPoint> DataPoints { get; set; }
        public List<List<DataPoint>> PeaksAndValleys { get; set; }
        public List<DataPoint> PeakPoints { get; set; }
        public List<DataPoint> ValleyPoints { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var csvFilePath = "../../data/DataToParse.csv";
            int startIndex = 3;

            // A threshold to detect false peaks/valleys when discovering local minimums and maximums.
            double threshold = 1.00;

            DataPoints = Helper.ReadCSVFile(startIndex, csvFilePath);
            PeaksAndValleys = Helper.GetPeaksAndValleys(DataPoints, threshold);
            PeakPoints = PeaksAndValleys[0];
            ValleyPoints = PeaksAndValleys[1];
            DataContext = this;
        }
    }
}
