using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAndLerning
{
    class Distribution
    {
        double[,] class1, class2;
        List<double[]> learn1, learn2, test1, test2;
        Random rnd = new Random(DateTime.Now.Millisecond);
        public void GetAllClassesValues(string path)
        {
            class1 = new double[7, 2520];
            class2 = new double[7, 2520];
            ReadCsvFiles(path);

        }
        public void RunDistripution(int test)
        {
            List<double[]> class1list = new List<double[]>();
            List<double[]> class2list = new List<double[]>();
            for (int i = 0; i < (class1.Length / (class1.GetUpperBound(0) + 1)); i++)
            {
                    double[] vect = new double[7];
                vect[0]=class1[0, i];
                vect[1] = class1[1, i];
                vect[2] = class1[2, i];
                vect[3] = class1[3, i];
                vect[4] = class1[4, i];
                vect[5] = class1[5, i];
                vect[6] = class1[6, i];
                class1list.Add(vect);
            }
            for (int i = 0; i < (class2.Length / (class2.GetUpperBound(0) + 1)); i++)
            {
                double[] vect = new double[7];
                vect[0] = class2[0, i];
                vect[1] = class2[1, i];
                vect[2] = class2[2, i];
                vect[3] = class2[3, i];
                vect[4] = class2[4, i];
                vect[5] = class2[5, i];
                vect[6] = class2[6, i];
                class2list.Add(vect);
            }
            learn1 = new List<double[]>();
            learn2 = new List<double[]>();
            for(int f=0;f< (class1.Length / (class1.GetUpperBound(0) + 1));f++)
            {
                double[] vec1 = new double[7];
                for (int y = 0; y < 7; y++)
                    vec1[y] = class1[y, f];
                learn1.Add(vec1);
            }
            for (int f = 0; f < (class2.Length / (class2.GetUpperBound(0) + 1)); f++)
            {
                double[] vec2 = new double[7];
                for (int y = 0; y < 7; y++)
                    vec2[y] = class2[y, f];
                learn2.Add(vec2);
            }
            test1 = new List<double[]>();
            test2 = new List<double[]>();
            double testingd = 2520.0 * ((test / 100.0));
            int testing = Convert.ToInt32(testingd);
            int testingqvt = testing % 2;
            int k = 0,h=2519;
            while(k<= testing)
            {
                int cl1 = rnd.Next(0, h);
                h--;
                double[] vec1 = new double[7];
                for(int y=0;y<7;y++)
                vec1[y] = class1[y, cl1];
                test1.Add(vec1);
                double[] vec2 = new double[7];
                for (int y = 0; y < 7; y++)
                    vec2[y] = class2[y, cl1];
                test2.Add(vec2);
                learn1.RemoveAt(cl1);
                learn2.RemoveAt(cl1);
                k++;
            }
            Normilise(class1list, "Class1");
            Normilise(class2list, "Class2");
            Normilise(learn1, "TeachingTop");
            Normilise(learn2, "TeachingBottom");
            Normilise(test1, "TestingUp");
            Normilise(test2, "TestingBottom");
            WriteToFile(class1list, "Class1");
            WriteToFile(class2list, "Class2");
            WriteToFile(learn1, "TeachingTop");
            WriteToFile(learn2, "TeachingBottom");
            WriteToFile(test1, "TestingUp");
            WriteToFile(test2, "TestingBottom");
        }
        void Normilise(List<double[]> mass,string name)
        {
            double[] min =new double[7];
            double[] max = new double[7];
            for(int i=0;i<7;i++)
            {
                min[i] = Int32.MaxValue;
                max[i] = Int32.MinValue;
            }
            foreach (double[] tx in mass)
            {
                 for(int i=0;i<7;i++)
                {
                    if (min[i] > tx[i])
                        min[i] = tx[i];
                    if (max[i] < tx[i])
                        max[i] = tx[i];
                }
            }
            foreach (double[] tx in mass)
            {
                for (int i = 0; i < 7; i++)
                {
                    tx[i] = (tx[i] - min[i]) / (max[i] - min[i]);
                }
            }
            WriteToFile(mass, "Normilize "+name);
        }
        void ReadCsvFiles(string path)
        {
            string folderPath = path + @"\1";
            int k = 0;
            double[] numbers = new double[7];
            if (Directory.Exists(folderPath))
            {
                string[] allfiles = Directory.GetFiles(folderPath);

                for (int j = 0; j < allfiles.Length; j++)
                {
                    string file = allfiles[j].Split('\\').Last();
                    file = file.Remove(0, 7);
                    file = file.Remove(file.Length - 4);
                    numbers[j] = Convert.ToDouble(file);
                }
                Array.Sort(numbers);
                char[] parm = new char[2] { '\r', '\n' };
                foreach (double num in numbers)
                {
                    string readText = File.ReadAllText(folderPath + @"\Britnes" + num + ".csv");
                    string[] str = readText.Split(parm);
                    List<string> krp = new List<string>();
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i].Length != 0)
                            krp.Add(str[i]);
                    }
                    foreach (string txt in krp)
                    {
                        string[] con = txt.Split(';');
                        class1[0, k] = Convert.ToDouble(con[0]);
                        class1[1, k] = Convert.ToDouble(con[1]);
                        class1[2, k] = Convert.ToDouble(con[2]);
                        class1[3, k] = Convert.ToDouble(con[3]);
                        class1[4, k] = Convert.ToDouble(con[4]);
                        class1[5, k] = Convert.ToDouble(con[5]);
                        class1[6, k] = Convert.ToDouble(con[6]);
                        k++;
                    }

                }
            }
            folderPath = path + @"\2";
            k = 0;
            numbers = new double[7];
            if (Directory.Exists(folderPath))
            {
                string[] allfiles = Directory.GetFiles(folderPath);

                for (int j = 0; j < allfiles.Length; j++)
                {
                    string file = allfiles[j].Split('\\').Last();
                    file = file.Remove(0, 7);
                    file = file.Remove(file.Length - 4);
                    numbers[j] = Convert.ToDouble(file);
                }
                Array.Sort(numbers);
                char[] parm = new char[2] { '\r', '\n' };
                foreach (double num in numbers)
                {
                    string readText = File.ReadAllText(folderPath + @"\Britnes" + num + ".csv");
                    string[] str = readText.Split(parm);
                    List<string> krp = new List<string>();
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i].Length != 0)
                            krp.Add(str[i]);
                    }
                    foreach (string txt in krp)
                    {
                        string[] con = txt.Split(';');
                        class2[0, k] = Convert.ToDouble(con[0]);
                        class2[1, k] = Convert.ToDouble(con[1]);
                        class2[2, k] = Convert.ToDouble(con[2]);
                        class2[3, k] = Convert.ToDouble(con[3]);
                        class2[4, k] = Convert.ToDouble(con[4]);
                        class2[5, k] = Convert.ToDouble(con[5]);
                        class2[6, k] = Convert.ToDouble(con[6]);
                        k++;
                    }

                }
            }
        }
        void WriteToFile(List<double[]> result, string name)
        {
            string workpath = Directory.GetCurrentDirectory();
            if (!Directory.Exists(workpath+@"\result"))
                Directory.CreateDirectory(workpath + @"\result");

            string pathCsvFile = workpath + @"\result\" + name + ".csv";
            string delimiter = ";";
            StringBuilder sb = new StringBuilder();
            int j = 0;
            foreach (double[] t in result)
            {
                sb.AppendLine(string.Join(delimiter, result[j]));
                j++;
            }
            File.WriteAllText(pathCsvFile, sb.ToString());
        }
    }
}
