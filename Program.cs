using System;
using System.IO;
using System.Linq;

namespace NAI_Knn_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int rightClassified = 0;
            int k = 1;
            DataSet trainingSet = new DataSet("C:/Users/peacr/source/repos/NAI_Knn_1/iris_training.txt");
            Console.WriteLine("Training set count: " + trainingSet.entries.Count);
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
                    }
                }
                double accuracy = (matches*1.0 / k*1.0)*100.0;
                accuracy = Math.Round(accuracy, 2);
                //Console.WriteLine(matches + " " + accuracy + "%");
                if(matchedLabel == testEntry.identifier)
                {
                    rightClassified++;
                }
            }
            double classifierAccuracy = (rightClassified * 1.0 / testSet.entries.Count*1.0)*100.0;
            Console.WriteLine(rightClassified + " " + classifierAccuracy + "%");
        }
    }
}
