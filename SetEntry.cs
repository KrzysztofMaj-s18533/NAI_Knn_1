using System;
using System.Collections.Generic;
using System.Text;

namespace NAI_Knn_1
{
    class SetEntry
    {
        public List<double> values { get; set; }
        public string identifier { get; set; }
        public double distance;
        public Dictionary<string,int> resultVals = new Dictionary<string, int>();
        public void setEuclidean(SetEntry entry){
            double distance = 0;
            for(int i = 0; i < values.Count; i++)
            {
                distance += Math.Pow((entry.values[i] - this.values[i]),2);
            }
            this.distance = distance;
        }
    }
}
