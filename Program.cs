using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NAI_Knn_1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet trainingSet = new DataSet("C:/Users/peacr/source/repos/NAI_Knn_1/iris_training.txt");
            Console.WriteLine("Please provide the value 'k' less than or equal to " + trainingSet.entries.Count + ":");
            string kString = Console.ReadLine();
            int rightClassified = 0;
            int k = int.Parse(kString);
            //Console.WriteLine("Training set count: " + trainingSet.entries.Count);
            Console.WriteLine("k var value: " + k);
            DataSet testSet = new DataSet("C:/Users/peacr/source/repos/NAI_Knn_1/iris_test.txt");
            foreach(var testEntry in testSet.entries)
            {
                foreach(var trainingEntry in trainingSet.entries)
                {
                    trainingEntry.setEuclidean(testEntry);
                }
                trainingSet.entries = trainingSet.entries.OrderBy(o => o.distance).ToList();
                for(int i = 0; i < k; i++)
                {
                    if (!testEntry.resultVals.ContainsKey(trainingSet.entries[i].identifier)){
                        testEntry.resultVals.Add(trainingSet.entries[i].identifier, 0);
                    }
                    testEntry.resultVals[trainingSet.entries[i].identifier]++;
                }
                int matches = 0;
                string matchedLabel = "";
                foreach(var val in testEntry.resultVals)
                {
                    //Console.WriteLine(val);
                    if(val.Value > matches)
                    {
                        matches = val.Value;
                        matchedLabel = val.Key;
                    }else if(val.Value == matches)
                    {
                        Random coinToss = new Random();
                        if(coinToss.Next() % 2 == 0)
                        {
                            matches = val.Value;
                            matchedLabel = val.Key;
                        }
                    }
                }
                //double accuracy = (matches*1.0 / k*1.0)*100.0;
                //accuracy = Math.Round(accuracy, 2);
                //Console.WriteLine(matches + " " + accuracy + "%");
                for(int i = 0; i < testEntry.values.Count; i++)
                {
                    Console.Write(testEntry.values[i] + "\t");
                }
                Console.Write(testEntry.identifier);
                Console.WriteLine(" was classified as " + matchedLabel);
                if(matchedLabel == testEntry.identifier)
                {
                    rightClassified++;
                }
            }
            double classifierAccuracy = (rightClassified * 1.0 / testSet.entries.Count*1.0)*100.0;
            Console.WriteLine(rightClassified + " " + classifierAccuracy + "%");
            while (true)
            {
                Console.WriteLine("Please provide an additional attribute vector (or write 'stop' to end program): ");
                string readLine = Console.ReadLine();
                if (readLine.Equals("stop"))
                {
                    break;
                }

                char[] splits = new char[] { ' ', '\t' };
                string[] values_id = readLine.Split(splits, StringSplitOptions.RemoveEmptyEntries);
                string[] vals = new string[values_id.Length - 1];
                for (int i = 0; i < vals.Length; i++)
                {
                    vals[i] = values_id[i];
                    vals[i] = vals[i].Replace(',', '.');
                    //Console.WriteLine("Po zamianie ',' na '.' : " + vals[i]);
                }
                string id = values_id[values_id.Length - 1];
                var entry = new SetEntry { values = new List<double>(), identifier = "" };
                foreach (var value in vals)
                {
                    //Console.WriteLine(value);
                    double numValue = double.Parse(value);
                    entry.values.Add(numValue);
                }
                entry.identifier = id;
                //Console.WriteLine(entry.identifier);

                foreach (var trainingEntry in trainingSet.entries)
                {
                    trainingEntry.setEuclidean(entry);
                }
                trainingSet.entries = trainingSet.entries.OrderBy(o => o.distance).ToList();
                for (int i = 0; i < k; i++)
                {
                    if (!entry.resultVals.ContainsKey(trainingSet.entries[i].identifier))
                    {
                        entry.resultVals.Add(trainingSet.entries[i].identifier, 0);
                    }
                    entry.resultVals[trainingSet.entries[i].identifier]++;
                }
                int matches = 0;
                string matchedLabel = "";
                foreach (var val in entry.resultVals)
                {
                    //Console.WriteLine(val);
                    if (val.Value > matches)
                    {
                        matches = val.Value;
                        matchedLabel = val.Key;
                    }
                    else if (val.Value == matches)
                    {
                        Random coinToss = new Random();
                        if (coinToss.Next() % 2 == 0)
                        {
                            matches = val.Value;
                            matchedLabel = val.Key;
                        }
                    }
                }

                for (int i = 0; i < entry.values.Count; i++)
                {
                    Console.Write(entry.values[i] + "\t");
                }
                Console.Write(entry.identifier);
                Console.WriteLine(" was classified as " + matchedLabel);
            }
        }
    }
}
