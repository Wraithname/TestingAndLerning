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
        Random rnd = new Random();
        /// <summary>
        /// Получение значений классов
        /// </summary>
        /// <param name="path">Путь</param>
        public void GetAllClassesValues(string path)
        {
            class1 = new double[11, 2527];
            class2 = new double[11, 2527];
            ReadCsvFiles(path);

        }
        /// <summary>
        /// Создание массива псевдослучайных чисел без повтора
        /// </summary>
        /// <param name="length">Размер тестового набора</param>
        /// <returns></returns>
        private int[] RandOnly(int length)
        {
            int[] numbrnd = new int[length];
            int i = 0;
            while (i < length)
            {
                int num = rnd.Next(0, 2527 - i);
                if (!numbrnd.Contains(num))
                {
                    numbrnd[i] = num;
                    i++;
                }
            }
            return numbrnd;
        }
        /// <summary>
        /// Метод распределения выборок
        /// </summary>
        /// <param name="test">Процент тестовой выборки</param>
        public void RunDistripution(int test)
        {
            List<double[]> class1list = new List<double[]>();
            List<double[]> class2list = new List<double[]>();
            for (int i = 0; i < (class1.Length / (class1.GetUpperBound(0) + 1)); i++)
            {
                double[] vect = new double[11];
                vect[0] = class1[0, i];
                vect[1] = class1[1, i];
                vect[2] = class1[2, i];
                vect[3] = class1[3, i];
                vect[4] = class1[4, i];
                vect[5] = class1[5, i];
                vect[6] = class1[6, i];
                vect[7] = class1[7, i];
                vect[8] = class1[8, i];
                vect[9] = class1[9, i];
                vect[10] = class1[10, i];
                class1list.Add(vect);
            }
            for (int i = 0; i < (class2.Length / (class2.GetUpperBound(0) + 1)); i++)
            {
                double[] vect = new double[11];
                vect[0] = class2[0, i];
                vect[1] = class2[1, i];
                vect[2] = class2[2, i];
                vect[3] = class2[3, i];
                vect[4] = class2[4, i];
                vect[5] = class2[5, i];
                vect[6] = class2[6, i];
                vect[7] = class2[7, i];
                vect[8] = class2[8, i];
                vect[9] = class2[9, i];
                vect[10] = class2[10, i];
                class2list.Add(vect);
            }
            learn1 = new List<double[]>();
            learn2 = new List<double[]>();
            for (int f = 0; f < (class1.Length / (class1.GetUpperBound(0) + 1)); f++)
            {
                double[] vec1 = new double[11];
                for (int y = 0; y < 11; y++)
                    vec1[y] = class1[y, f];
                learn1.Add(vec1);
            }
            for (int f = 0; f < (class2.Length / (class2.GetUpperBound(0) + 1)); f++)
            {
                double[] vec2 = new double[11];
                for (int y = 0; y < 11; y++)
                    vec2[y] = class2[y, f];
                learn2.Add(vec2);
            }
            test1 = new List<double[]>();
            test2 = new List<double[]>();
            double testingd = 2527.0 * ((test / 100.0));
            int testing = Convert.ToInt32(testingd);
            int testingqvt = testing % 2;
            testing += testingqvt;
            int k = 0;
            int[] rndarrayclass1 = RandOnly(testing);
            int[] rndarrayclass2 = RandOnly(testing);
            while (k < testing)
            {

                double[] vec1 = new double[11];
                for (int y = 0; y < 11; y++)
                    vec1[y] = class1[y, rndarrayclass1[k]];
                test1.Add(vec1);
                double[] vec2 = new double[11];
                for (int y = 0; y < 11; y++)
                    vec2[y] = class2[y, rndarrayclass2[k]];
                test2.Add(vec2);
                learn1.RemoveAt(rndarrayclass1[k]);
                learn2.RemoveAt(rndarrayclass2[k]);
                k++;
            }
            WriteToFile(class1list, "Class1");
            WriteToFile(class2list, "Class2");
            WriteToFile(learn1, "TeachingTop");
            WriteToFile(learn2, "TeachingBottom");
            WriteToFile(test1, "TestingUp");
            WriteToFile(test2, "TestingBottom");
            Normilise(class1list, "Class1");
            Normilise(class2list, "Class2");
            Normilise(learn1, "TeachingTop");
            Normilise(learn2, "TeachingBottom");
            Normilise(test1, "TestingUp");
            Normilise(test2, "TestingBottom");
        }
        /// <summary>
        /// Нормализация
        /// </summary>
        /// <param name="mass">Лист векторов</param>
        /// <param name="name">Имя выборки</param>
        void Normilise(List<double[]> mass, string name)
        {
            double[] min = new double[11];
            double[] max = new double[11];
            for (int i = 0; i < 11; i++)
            {
                min[i] = Int32.MaxValue;
                max[i] = Int32.MinValue;
            }
            foreach (double[] tx in mass)
            {
                for (int i = 0; i < 11; i++)
                {
                    if (min[i] > tx[i])
                        min[i] = tx[i];
                    if (max[i] < tx[i])
                        max[i] = tx[i];
                }
            }
            foreach (double[] tx in mass)
            {
                for (int i = 0; i < 11; i++)
                {
                    tx[i] = (tx[i] - min[i]) / (max[i] - min[i]);
                }
            }
            WriteToFile(mass, "Normilize " + name);
        }
        /// <summary>
        /// Чтение CSV файла
        /// </summary>
        /// <param name="path">Путь</param>
        void ReadCsvFiles(string path)
        {
            int k = 0;
                if (Directory.Exists(path))
                {
                    string[] allfiles = Directory.GetFiles(path);
                    char[] parm = new char[2] { '\r', '\n' };
                    string readText = File.ReadAllText(allfiles[0]);
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
                        class1[7, k] = Convert.ToDouble(con[7]);
                        class1[8, k] = Convert.ToDouble(con[8]);
                        class1[9, k] = Convert.ToDouble(con[9]);
                        class1[10, k] = Convert.ToDouble(con[10]);
                        k++;
                    }
                    k = 0;
                    readText = File.ReadAllText(allfiles[1]);
                    str = readText.Split(parm);
                    krp = new List<string>();
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
                        class2[7, k] = Convert.ToDouble(con[7]);
                        class2[8, k] = Convert.ToDouble(con[8]);
                        class2[9, k] = Convert.ToDouble(con[9]);
                        class2[10, k] = Convert.ToDouble(con[10]);
                        k++;
                    }
                }
        }
        /// <summary>
        /// Запись в CSV файл
        /// </summary>
        /// <param name="result">Данные для записи</param>
        /// <param name="name">Имя файла</param>
        void WriteToFile(List<double[]> result, string name)
        {
            string workpath = Directory.GetCurrentDirectory();
            if (!Directory.Exists(workpath + @"\result"))
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
