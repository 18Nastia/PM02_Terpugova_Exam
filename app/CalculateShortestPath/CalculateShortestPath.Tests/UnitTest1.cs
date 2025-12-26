using Xunit;

namespace CalculateShortestPath.Tests
{
    public class FloydWarshallSimpleTests
    {
        [Fact]
        public void CalculateFloyd_SimpleGraph_ReturnsCorrectShortestPaths()
        {
            // Исходная матрица расстояний для простого графа
            double[,] distances = new double[,]
            {
            { 0, 1, 4 },   // Расстояния от вершины 1
            { double.MaxValue, 0, 2 }, // Расстояния от вершины 2 (Значение double.MaxValue обозначает отсутствие ребра между вершинами.)
            { double.MaxValue, double.MaxValue, 0 } // Расстояния от вершины 3
            };

            // Ожидаемая матрица кратчайших расстояний
            double[,] expected = new double[,]
            {
            { 0, 1, 3 },   // Кратчайшие расстояния от вершины 1
            { double.MaxValue, 0, 2 }, // Кратчайшие расстояния от вершины 2
            { double.MaxValue, double.MaxValue, 0 } // Кратчайшие расстояния от вершины 3
            };

            double[,] result = Program.CalculateFloyd(distances, out int[,] next);

            // Проверка результатов ()
            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.Equal(expected[i, j], result[i, j]);
                }
            }
        }
    }
}