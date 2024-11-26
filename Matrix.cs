using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace чм_лаба_2
{
    public class Matrix
    {
        public double[,] A { get; set; }
        public double[] B { get; set; }

        public Matrix(double[,] a, double[] b)
        {
            A = a;
            B = b;
        }

        public static double[] SolveLinearSystem(double[,] A, double[] B)
        {
            int n = B.Length;
            double[,] inverseA = InverseMatrix(A);
            double[] result = new double[n];

            for (int i = 0; i < n; i++)
            {
                result[i] = 0;
                for (int j = 0; j < n; j++)
                {
                    result[i] += inverseA[i, j] * B[j];
                }
            }

            return result;
        }

        static double[,] InverseMatrix(double[,] A)
        {
            int n = A.GetLength(0);
            double[,] result = new double[n, n];
            double[,] temp = (double[,])A.Clone();

            for (int i = 0; i < n; i++)
                result[i, i] = 1;

            for (int i = 0; i < n; i++)
            {
                if (temp[i, i] == 0)
                    throw new InvalidOperationException("Matrix is singular and cannot be inverted.");

                double diag = temp[i, i];
                for (int j = 0; j < n; j++)
                {
                    temp[i, j] /= diag;
                    result[i, j] /= diag;
                }

                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        double factor = temp[j, i];
                        for (int k = 0; k < n; k++)
                        {
                            temp[j, k] -= factor * temp[i, k];
                            result[j, k] -= factor * result[i, k];
                        }
                    }
                }
            }

            return result;
        }

        public static double[] Solve(double[,] A, double[] B, int n)
        {
            // Добавление свободных членов в матрицу A
            double[,] augmentedMatrix = new double[n, n + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i, j] = A[i, j];
                }
                augmentedMatrix[i, n] = B[i]; // последний столбец - это B
            }

            // Прямой ход
            for (int i = 0; i < n; i++)
            {
                double maxEl = Math.Abs(augmentedMatrix[i, i]);
                int maxRow = i;
                for (int k = i + 1; k < n; k++)
                {
                    if (Math.Abs(augmentedMatrix[k, i]) > maxEl)
                    {
                        maxEl = Math.Abs(augmentedMatrix[k, i]);
                        maxRow = k;
                    }
                }

                // Обмен текущей строки с максимальной строкой
                for (int k = i; k < n + 1; k++)
                {
                    double tmp = augmentedMatrix[maxRow, k];
                    augmentedMatrix[maxRow, k] = augmentedMatrix[i, k];
                    augmentedMatrix[i, k] = tmp;
                }

                // Обнуление под текущей строкой
                for (int k = i + 1; k < n; k++)
                {
                    double c = -augmentedMatrix[k, i] / augmentedMatrix[i, i];
                    for (int j = i; j < n + 1; j++)
                    {
                        if (i == j)
                        {
                            augmentedMatrix[k, j] = 0;
                        }
                        else
                        {
                            augmentedMatrix[k, j] += c * augmentedMatrix[i, j];
                        }
                    }
                }
            }

            // Обратный ход
            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = augmentedMatrix[i, n] / augmentedMatrix[i, i];
                for (int j = i + 1; j < n; j++)
                {
                    x[i] -= augmentedMatrix[i, j] * x[j] / augmentedMatrix[i, i];
                }
            }

            return x;
        }


        public static double[,] SquareMethod(double[,] A)
        {
            int n = A.GetLength(0);
            double[,] L = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    // Сумма для диагональных элементов
                    double sum = 0;
                    for (int k = 0; k < j; k++)
                    {
                        sum += L[i, k] * L[j, k];
                    }

                    // Для диагональных элементов
                    if (i == j)
                    {
                        double value = A[i, i] - sum;
                        if (value < 0)
                        {
                            throw new InvalidOperationException("Матрица не положительно определена.");
                        }
                        L[i, j] = Math.Sqrt(value);
                    }
                    else
                    {
                        // Для недиагональных элементов
                        L[i, j] = (A[i, j] - sum) / L[j, j];
                    }
                }
            }

            return L;
        }

        // Метод для решения системы Ly = b
        public static double[] ForwardSubstitution(double[,] L, double[] B)
        {
            int n = L.GetLength(0);
            double[] y = new double[n];

            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++)
                {
                    sum += L[i, j] * y[j];
                }
                y[i] = (B[i] - sum) / L[i, i];
            }

            return y;
        }

        // Метод для решения системы L^T x = y
        public static double[] BackwardSubstitution(double[,] L, double[] y)
        {
            int n = L.GetLength(0);
            double[] x = new double[n];

            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += L[j, i] * x[j];
                }
                x[i] = (y[i] - sum) / L[i, i];
            }

            return x;
        }
        // Метод для решения СЛАУ с использованием разложения Холецкого
        public static double[] SquareSolve(double[,] A, double[] B)
        {

            // Получаем нижнюю треугольную матрицу L
            double[,] L = SquareMethod(A);

            // Решаем систему Ly = b
            double[] y = ForwardSubstitution(L, B);

            // Решаем систему L^T x = y
            double[] x = BackwardSubstitution(L, y);

            return x;


        }
        public static double[] GausseJordan(double[,] A, double[] B, int n)
        {
            // Добавление свободных членов в матрицу A (та же логика, что и в Solve)
            double[,] augmentedMatrix = new double[n, n + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i, j] = A[i, j];
                }
                augmentedMatrix[i, n] = B[i];
            }

            // Прямой ход (модифицированный для Гаусса-Жордана)
            for (int i = 0; i < n; i++)
            {
                double maxEl = Math.Abs(augmentedMatrix[i, i]);
                int maxRow = i;
                for (int k = i + 1; k < n; k++)
                {
                    if (Math.Abs(augmentedMatrix[k, i]) > maxEl)
                    {
                        maxEl = Math.Abs(augmentedMatrix[k, i]);
                        maxRow = k;
                    }
                }

                // Обмен текущей строки с максимальной строкой (та же логика)
                for (int k = i; k < n + 1; k++)
                {
                    double tmp = augmentedMatrix[maxRow, k];
                    augmentedMatrix[maxRow, k] = augmentedMatrix[i, k];
                    augmentedMatrix[i, k] = tmp;
                }

                // Деление строки на главный элемент (для получения 1 на диагонали)
                double div = augmentedMatrix[i, i];
                if (div == 0)
                {
                    throw new Exception("Singular matrix - no unique solution"); // Обработка вырожденной матрицы
                }
                for (int k = i; k <= n; k++)
                {
                    augmentedMatrix[i, k] /= div;
                }

                // Обнуление элементов над и под диагональю
                for (int k = 0; k < n; k++)
                {
                    if (k != i)
                    {
                        double c = -augmentedMatrix[k, i];
                        for (int j = i; j <= n; j++)
                        {
                            augmentedMatrix[k, j] += c * augmentedMatrix[i, j];
                        }
                    }
                }
            }


            // Решение (x) находится в последнем столбце расширенной матрицы
            double[] x = new double[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = augmentedMatrix[i, n];
            }

            return x;
        }

        public static double[] ZeydelMethod(double[,] A, double[] B)
        {

            double[] x = new double[B.Length]; // Начальное приближение
            double epsilon = 1e-10; // Допустимая погрешность
            int maxIterations = 1000; // Максимальное количество итераций

            for (int k = 0; k < maxIterations; k++)
            {
                double[] xOld = (double[])x.Clone(); // Копируем текущее приближение

                for (int i = 0; i < B.Length; i++)
                {
                    double sum = B[i];
                    for (int j = 0; j < B.Length; j++)
                    {
                        if (j != i)
                        {
                            sum -= A[i, j] * x[j];
                        }
                    }
                    x[i] = sum / A[i, i];
                }

                // Проверка на сходимость
                double maxDiff = 0;
                for (int i = 0; i < B.Length; i++)
                {
                    maxDiff = Math.Max(maxDiff, Math.Abs(x[i] - xOld[i]));
                }

                if (maxDiff < epsilon)
                {
                    Console.WriteLine($"Решение найдено за {k + 1} итераций:");
                    for (int i = 0; i < B.Length; i++)
                    {
                        Console.WriteLine($"x[{i}] = {x[i]}");
                    }
                    return x;
                }
            }

            Console.WriteLine("Достигнуто максимальное количество итераций.");
            return x;
        }
    }
}


