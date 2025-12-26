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

        return;
    }
}