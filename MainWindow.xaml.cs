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

            DataPoints = ReadCSVFile(startIndex, csvFilePath);
            PeaksAndValleys = Calculate.GetPeaks(DataPoints, threshold);
            PeakPoints = PeaksAndValleys[0];
            ValleyPoints = PeaksAndValleys[1];
            DataContext = this;
        }

        /// <summary>
        /// Using CSVHelper, read in a CSV file and return a list of DataPoints (containing an index, X coord, and Y coord).
        /// </summary>
        /// <param name="startIndex">Start index coorisponding to the index of the first set of coordinate values within the csv file.</param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private List<DataPoint> ReadCSVFile(int startIndex, string filepath)
        {
            using (var reader = new StreamReader(filepath))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CreateSpecificCulture("")))
            {
                var data = new List<DataPoint>();
                // Remove the beginning data that does not coorispond to any data points
                for(int i = 0; i < startIndex; i++)
                {
                    csv.Read();
                }

                // Set the data-start index for tracking indices for quickly finding where each value is located. NOTE: When viewing in Exel, the data will be at index+1 because it starts at index 1, not 0.
                int index = startIndex;
                while (csv.Read())
                {
                    var x = csv.GetField<double>(0);
                    var y = csv.GetField<double>(1);
                    var point = new DataPoint(index, x, y);
                    data.Add(point);

                    index++;
                }

                return data;
            }
        }
    }
}
