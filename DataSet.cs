using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NAI_Knn_1
{
    class DataSet
    {
        public string[] dataLines;
        public List<SetEntry> entries = new List<SetEntry>();
        public DataSet(String path){
            dataLines = File.ReadAllLines(path);
            foreach(var line in dataLines)
            {
                char[] splits = new char[] { ' ', '\t' };
                string[] values_id = line.Split(splits, StringSplitOptions.RemoveEmptyEntries);
                string[] vals = new string[values_id.Length - 1];
                for(int i = 0; i < vals.Length; i++)
                {
                    vals[i] = values_id[i];
                    vals[i] = vals[i].Replace(',', '.');
                    //Console.WriteLine("Po zamianie ',' na '.' : " + vals[i]);
                }
                string id = values_id[values_id.Length - 1];
                var entry = new SetEntry { values = new List<double>(), identifier = "" };
                foreach(var value in vals)
                {
                    //Console.WriteLine(value);
                    double numValue = double.Parse(value);
                    entry.values.Add(numValue);
                }
                entry.identifier = id;
                //Console.WriteLine(entry.identifier);
                foreach(double wrr in entry.values)
                {
                    //Console.WriteLine(wrr);
                }
                entries.Add(entry);
            }
        }
    }
}
