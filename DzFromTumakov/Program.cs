

using static System.Net.Mime.MediaTypeNames;

namespace DzFromTumakov
{

    internal class Program
    {
        private static string englishAlphaVowels = "aeyuoi";
        private static string englishAlphaConsonants = "qwrtpsdfghjklzxcvbnm";

        private static string russianAlphaVowels = "уеыаоэяию";
        private static string russianAlphaConsonants = "йцкнгшщзхъфвпрлджчсмтб";

        static void Task1(string path)
        {
            /*Упражнение 6.1 Написать программу, которая вычисляет число гласных и согласных букв в
            файле. Имя файла передавать как аргумент в функцию Main. Содержимое текстового файла
            заносится в массив символов. Количество гласных и согласных букв определяется проходом
            по массиву. Предусмотреть метод, входным параметром которого является массив символов.
            Метод вычисляет количество гласных и согласных букв.*/

            Console.WriteLine("Упражнение 6.1");

            if (path.Length == 0)
            {
                Console.WriteLine("Не указан файл в аргументах.");
                return;
            }

            string fileName = path;

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Файл {fileName} не найден.");
                return;
            }

            string fileContent = File.ReadAllText(fileName);

            List<char> charList = new List<char>(fileContent);
            CountVowelsAndConsonants(charList);
        }

        static void CountVowelsAndConsonants(List<char> charList)
        {
            int vowelsCount = 0;
            int consonantsCount = 0;

            string commonAlpha = englishAlphaConsonants + englishAlphaVowels + russianAlphaConsonants + russianAlphaVowels;
            string vowelsAlpha = englishAlphaVowels + russianAlphaVowels;
            string consonantsAlpha = englishAlphaConsonants + russianAlphaConsonants;

            foreach (char c in charList)
            {
                char lowerC = char.ToLower(c);
                
                if (commonAlpha.Contains(lowerC))
                {
                    if (vowelsAlpha.Contains(lowerC))
                    {
                        vowelsCount++;
                    }
                    else
                    {
                        consonantsCount++;
                    }
                }
                
            }

            Console.WriteLine($"Количество гласных: {vowelsCount}");
            Console.WriteLine($"Количество согласных: {consonantsCount}");

        }


        static void Task2()
        {
            /*Упражнение 6.2 Написать программу, реализующую умножению двух матриц, заданных в
            виде двумерного массива. В программе предусмотреть два метода: метод печати матрицы,
            метод умножения матриц (на вход две матрицы, возвращаемое значение – матрица).*/

            Console.WriteLine("Упражнение 6.2");
            int[,] matrixA = 
            {
            { 1, 2, 3 },
            { 4, 5, 6 }
            };

            int[,] matrixB = 
            {
            { 7, 8 },
            { 9, 10 },
            { 11, 12 }
            };

            Console.WriteLine("Матрица A:");
            PrintMatrix(matrixA);

            Console.WriteLine("\nМатрица B:");
            PrintMatrix(matrixB);

            int[,] result = MultiplyMatrices(matrixA, matrixB);

            Console.WriteLine("\nРезультат умножения матриц A и B:");
            PrintMatrix(result);
        }

        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0); 
            int cols = matrix.GetLength(1); 

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int rowsB = matrixB.GetLength(0); 
            int colsB = matrixB.GetLength(1); 

            if (colsA != rowsB)
            {
                throw new InvalidOperationException("Количество столбцов первой матрицы должно быть равно количеству строк второй матрицы.");
            }

            int[,] result = new int[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        result[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            return result;

        }

        static void Task3()
        {
            /*Упражнение 6.3 Написать программу, вычисляющую среднюю температуру за год. Создать
            двумерный рандомный массив temperature[12,30], в котором будет храниться температура
            для каждого дня месяца (предполагается, что в каждом месяце 30 дней). Сгенерировать
            значения температур случайным образом. Для каждого месяца распечатать среднюю
            температуру. Для этого написать метод, который по массиву temperature [12,30] для каждого
            месяца вычисляет среднюю температуру в нем, и в качестве результата возвращает массив
            средних температур. Полученный массив средних температур отсортировать по
            возрастанию.*/

            Console.WriteLine("Упражнение 6.3");

            Random rand = new Random();

            Dictionary<string, int[]> temperature = new Dictionary<string, int[]>();

            string[] months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
                            "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

            foreach (var month in months)
            {
                int[] monthTemperatures = new int[30];
                for (int i = 0; i < 30; i++)
                {
                    monthTemperatures[i] = rand.Next(-30, 41);
                }
                temperature[month] = monthTemperatures;
            }

            Dictionary<string, double> averageTemperatures = new Dictionary<string, double>();

            foreach (var month in temperature)
            {
                double sum = 0;
                foreach (var temp in month.Value)
                {
                    sum += temp;
                }
                double average = sum / month.Value.Length;
                averageTemperatures[month.Key] = average;
            }

            Console.WriteLine("\nМесяца, отсортированные по средней температуре:");
            foreach (var entry in SortByValue(averageTemperatures))
            {
                Console.WriteLine($"{entry.Key}: {entry.Value:F2}°C");
            }
        }

        static List<KeyValuePair<string, double>> SortByValue(Dictionary<string, double> dictionary)
        {
            List<KeyValuePair<string, double>> results = new List<KeyValuePair<string, double>>();

            foreach (var entry in new SortedList<double, string>(dictionary.ToDictionary(k => k.Value, v => v.Key)).Reverse())
            {
                results.Add(new KeyValuePair<string, double>(entry.Value, entry.Key));

            }

            return results;
        }

        static void Main(string[] args)
        {
            Task1(args[0]);
            Task2();
            Task3();

        }


    }

}

