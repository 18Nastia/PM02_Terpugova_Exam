using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите количество точек:");
        int n = int.Parse(Console.ReadLine());

        double[,] distances = new double[n, n];

        // Инициализация матрицы расстояний
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                distances[i, j] = (i == j) ? 0 : double.MaxValue; // Расстояние до самого себя = 0
            }
        }

        Console.WriteLine("Введите расстояния между точками (формат: точка1 точка2 расстояние):");
        Console.WriteLine("Введите 'Q' для завершения ввода.");

        while (true)
        {
            string input = Console.ReadLine();
            if (input.Equals("Q", StringComparison.OrdinalIgnoreCase)) break;

            var parts = input.Split(' ');
            try
            {
                int point1 = int.Parse(parts[0]) - 1;
                int point2 = int.Parse(parts[1]) - 1;

                // Заменяем запятую на точку для корректного парсинга
                double distance = double.Parse(parts[2].Replace(',', '.'), CultureInfo.InvariantCulture);

                distances[point1, point2] = distance;
                distances[point2, point1] = distance; // если граф неориентированный
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка ввода: " + ex.Message);
            }
        }

        // Применение алгоритма Флойда
        double[,] shortestPaths = CalculateFloyd(distances, out int[,] next);

        while (true)
        {
            Console.WriteLine("Введите две точки для поиска кратчайшего пути (или 'Q' для выхода):");
            string input = Console.ReadLine();
            if (input.Equals("Q", StringComparison.OrdinalIgnoreCase)) break;

            var points = input.Split(' ');
            try
            {
                int p1 = int.Parse(points[0]) - 1;
                int p2 = int.Parse(points[1]) - 1;

                double routeLength = shortestPaths[p1, p2];
                Console.WriteLine($"Кратчайшее расстояние между точками {p1 + 1} и {p2 + 1}: {routeLength}");
                GetPath(next, p1, p2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка ввода: " + ex.Message);
            }
        }
    }

    static double[,] CalculateFloyd(double[,] a, out int[,] next)
    {
        int n = a.GetLength(0);
        double[,] d = (double[,])a.Clone();
        next = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (d[i, j] < double.MaxValue)
                    next[i, j] = j;
                else
                    next[i, j] = -1;
            }
        }

        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (d[i, j] > d[i, k] + d[k, j])
                    {
                        d[i, j] = d[i, k] + d[k, j];
                        next[i, j] = next[i, k];
                    }
                }
            }
        }
        return d;
    }

    static void GetPath(int[,] next, int start, int end)
    {
        if (next[start, end] == -1)
        {
            Console.WriteLine("Нет пути между этими точками.");
            return;
        }

        Console.Write("Путь: ");
        int current = start;
        while (current != end)
        {
            Console.Write((current + 1) + " -> ");
            current = next[current, end];
        }
        Console.WriteLine(end + 1);
    }
}
