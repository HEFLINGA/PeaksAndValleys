using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaksAndValleys
{
    public static class Calculate
    {



        /// <summary>
        /// Takes in a list of DataPoints, loops through them, and returns a new List of DataPoints lists. Index 0 is a list of Peaks locations, and Index 1 is the Valleys list of locations.
        /// </summary>
        /// <param name="dataPoints">List of DataPoint coordinates.</param>
        /// <param name="threshold">A threshold used to add to or take away from a current point, to check if a current potential valley or peak is a true valley/peak in the particular dataset.</param>
        /// <returns>A List of Lists of DataPoints, Index 0 being the list of Peak locations, and Index 1 being the list of Valley locations.</returns>
        public static List<List<DataPoint>> GetPeaks(List<DataPoint> dataPoints, double threshold)
        {
            // Create a list of lists, as well as list of DataPoints for peaks and valleys.
            // Add in the first point from the dataPoints list as a temp valley for thresholding.
            List<List<DataPoint>> peaksAndValleys = new List<List<DataPoint>>();
            List<DataPoint> peaks = new List<DataPoint>() { dataPoints[dataPoints.Count-1] };
            List<DataPoint> valleys = new List<DataPoint>() { dataPoints[0] };

            char lastAdded = 'P';

            // Iterate over the list of dataPoints, checking to see if the current point is a "local max" (peak) or "local min" (valley)
            for(int i = 1; i < dataPoints.Count - 1; i++)
            {
                DataPoint currentPoint = dataPoints[i];
                DataPoint previousPoint= dataPoints[i-1];
                DataPoint nextPoint    = dataPoints[i+1];

                // Checking for local min
                if (currentPoint.Y < previousPoint.Y && currentPoint.Y < nextPoint.Y)
                {
                    // Use threshold to check that there is a minimum distance between a peak/valley
                    // Check if a peak was added last. If not, no new valley should be added.
                    if (currentPoint.Y + threshold < peaks[peaks.Count - 1].Y && lastAdded == 'P')
                    {
                        valleys.Add(currentPoint);
                        lastAdded = 'V';
                    }
                }

                // Checking for local max
                if (currentPoint.Y > previousPoint.Y && currentPoint.Y > nextPoint.Y)
                {
                    // Use threshold to check that there is a minimum distance between a peak/valley
                    // Check if a Valley was added last. If not, no new peak should be added.
                    if (currentPoint.Y - threshold > valleys[valleys.Count-1].Y && lastAdded == 'V')
                    { 
                        peaks.Add(currentPoint);
                        lastAdded = 'P';
                    }
                }
            }

            // Remove the temporary first peak/valley datapoint
            peaks.Remove(peaks[0]);
            valleys.Remove(valleys[0]);

            peaksAndValleys.Add(peaks);
            peaksAndValleys.Add(valleys);

            return peaksAndValleys;
        }
    }
}
