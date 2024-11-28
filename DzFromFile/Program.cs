using System.Collections.Generic;

namespace DzFromFile
{
    internal class Program
    {
        static void Task1()
        {
            /*1.Создать List на 64 элемента, скачать из интернета 32 пары картинок(любых). В List
            должно содержаться по 2 одинаковых картинки. Необходимо перемешать List с
            картинками.Вывести в консоль перемешанные номера(изначальный List и полученный
            List).Перемешать любым способом*/

            Console.WriteLine("Задача 1");

            List<KeyValuePair<int, string>> images = new List<KeyValuePair<int, string>>();
            for (int i = 1; i <= 32; i++)
            {
                string imageUrl = $"Image_{i}.jpg";
                images.Add(new KeyValuePair<int, string>(i, imageUrl));
                images.Add(new KeyValuePair<int, string>(i, imageUrl));
            }
            Console.WriteLine("Изначальный список:");
            for (int i = 0; i < images.Count; i++)
            {
                Console.WriteLine($"Номер картинки: {images[i].Key}");
            }

            MixUp(images);

            Console.WriteLine("\nПеремешанный список:");
            for (int i = 0; i < images.Count; i++)
            {
                Console.WriteLine($"Номер картинки: {images[i].Key}");
            }

        }

        static void MixUp<T>(List<T> list)
        {
            Random rand = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        static void Task2()
        {
            /*2. Создать студента из вашей группы (фамилия, имя, год рождения, с каким экзаменом
            поступил, баллы). Создать словарь для студентов из вашей группы (10 человек).
            Необходимо прочитать информацию о студентах с файла. В консоли необходимо создать
            меню:
            a. Если пользователь вводит: Новый студент, то необходимо ввести
            информацию о новом студенте и добавить его в List
            b. Если пользователь вводит: Удалить, то по фамилии и имени удаляется
            студент
            c. Если пользователь вводит: Сортировать, то происходит сортировка по баллам
            (по возрастанию)*/

            Console.WriteLine("Задача 2");

            List<Student> students = ReadStudentsFromFile(@"C:\Users\KomaM\OneDrive\Рабочий стол\students.txt");

            Console.WriteLine("Текущий список студентов:");
            PrintStudents(students);
            Console.WriteLine();

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "Новый студент")
                {
                    Console.WriteLine("Введите имя студента:");
                    string firstName = Console.ReadLine();
                    Console.WriteLine("Введите фамилию студента:");
                    string lastName = Console.ReadLine();
                    Console.WriteLine("Введите год рождения студента:");
                    uint yearOfBirth = uint.Parse(Console.ReadLine());
                    Console.WriteLine("Введите экзамен при поступлении:");
                    string admissionExam = Console.ReadLine();
                    Console.WriteLine("Введите балл студента за экзамены:");
                    uint scores = uint.Parse(Console.ReadLine());

                    students.Add(new Student
                    {
                        firstName = firstName,
                        lastName = lastName,
                        yearOfBirth = yearOfBirth,
                        admissionExam = admissionExam,
                        scores = scores
                    });
                }
                else if (command == "Удалить")
                {
                    Console.WriteLine("Введите имя студента:");
                    string firstName = Console.ReadLine();
                    Console.WriteLine("Введите фамилию студента:");
                    string lastName = Console.ReadLine();

                    students.RemoveAll(student => student.lastName == lastName && student.firstName == firstName);
                }
                else if (command == "Сортировать")
                {
                    students = students.OrderBy(student => student.scores).ToList();
                }
                else
                {
                    return;
                }

                Console.WriteLine("Текущий список студентов:");
                PrintStudents(students);
                Console.WriteLine();
            }

        }

        static void PrintStudents(List<Student> students)
        {
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"Имя: {students[i].firstName}");
                Console.WriteLine($"Фамилия: {students[i].lastName}");
                Console.WriteLine($"Год рождения: {students[i].yearOfBirth}");
                Console.WriteLine($"Экзамен при поступлении: {students[i].admissionExam}");
                Console.WriteLine($"Баллы: {students[i].scores}");
                Console.WriteLine();
            }
        }

        // Задать отдельный вопрос chatgpt, как считывать класс Student из файла (по какому формату, чтобы было легко)
        private static List<Student> ReadStudentsFromFile(string path)
        {
            List<Student> students = new List<Student>();

            // Не пустые строки
            string[] lines = File.ReadAllLines(path)
                     .Where(line => !string.IsNullOrWhiteSpace(line))
                     .ToArray();

            while (lines.Length > 0)
            {
                string firstName = lines[0];
                string lastName = lines[1];
                uint yearOfBirth = uint.Parse(lines[2]);
                string admissionExam = lines[3];
                uint scores = uint.Parse(lines[2]);

                students.Add(new Student
                {
                    firstName = firstName,
                    lastName = lastName,
                    yearOfBirth = yearOfBirth,
                    admissionExam = admissionExam,
                    scores = scores
                });

                lines = lines.Skip(5).ToArray();
            }



            return students;
        }

        class Student
        {
            public string firstName;
            public string lastName;
            public uint yearOfBirth;
            public string admissionExam;
            public uint scores;
        }


        static void Task3()
        {
            /*3. Создать бабулю. У бабули есть Имя, возраст, болезнь и лекарство от этой болезни,
            которое она принимает (болезней может быть у бабули несколько). Реализуйте список
            бабуль. Также есть больница (у больницы есть название, список болезней, которые они
            лечат и вместимость). Больниц также, как и бабуль несколько. Бабули по очереди
            поступают (реализовать ввод с клавиатуры) и дальше идут в больницу, в зависимости от
            заполненности больницы и списка болезней, которые лечатся в данной больнице,
            реализовать функционал, который будет распределять бабулю в нужную больницу. Если
            бабуля не имеет болезней, то она хочет только спросить - она может попасть в первую
            свободную клинику. Если бабулю ни одна из больниц не лечит, то бабуля остаётся на
            улице плакать. На экран выводить список всех бабуль, список всех больниц, болезни,
            которые там лечат и сколько бабуль в данный момент находится в больнице, также
            вывести процент заполненности больницы. P.S. Бабуля попадает в больницу, если там
            лечат более 50% ее болезней. Больницы реализовать в виде стека, бабуль в виде
            очереди.*/


            Console.WriteLine("Задача 3");
        }

        static void Task4()
        {
            /*4. Написать метод для обхода графа в глубину или ширину - вывести на экран кратчайший
            путь.*/

            Console.WriteLine("Задача 4");

            int[,] graph = new int[,]
            {
                { 0, 1, 0, 0, 1 },
                { 1, 0, 1, 1, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 1 },
                { 1, 0, 0, 1, 0 }
            };

            int start = 0;
            int end = 3;

            List<int> shortestPath = FindShortestPathBFS(graph, start, end);

            if (shortestPath.Count > 0)
            {
                Console.WriteLine("Кратчайший путь: " + string.Join(" -> ", shortestPath));
            }
            else
            {
                Console.WriteLine("Путь не найден.");
            }
        }

        static List<int> FindShortestPathBFS(int[,] graph, int start, int end)
        {
            int n = graph.GetLength(0);
            bool[] visited = new bool[n];
            int[] previous = new int[n];
            for (int i = 0; i < n; i++) previous[i] = -1;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);
            visited[start] = true;

            while (queue.Count > 0)
            {
                int currentVertex = queue.Dequeue();

                if (currentVertex == end)
                {
                    break;
                }

                for (int adjacentVertex = 0; adjacentVertex < n; adjacentVertex++)
                {
                    if (graph[currentVertex, adjacentVertex] == 1 && !visited[adjacentVertex])
                    {
                        queue.Enqueue(adjacentVertex);
                        visited[adjacentVertex] = true;
                        previous[adjacentVertex] = currentVertex;
                    }
                }
            }

            // Восстановление пути
            List<int> path = new List<int>();
            for (int at = end; at != -1; at = previous[at])
            {
                path.Add(at);
            }
            path.Reverse();

            // Если стартовая вершина не равна начальной в пути, путь не существует
            if (path[0] != start)
            {
                path.Clear();
            }

            return path;
        }


        static void Main()
        {
            Task1();
            Task2();
            //Task3();
            Task4();

        }
    }
}
