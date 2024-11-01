﻿using System;
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
        public int R { get; set; }

        public Matrix(double[,] a, double[] b, int r)
        {
            A = a;
            B = b;
            R = r;
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
        public static double[,] Transposition(double[,] A)
        {
            int n = A.GetLength(0);
            int m = A.GetLength(1);
            double[,] result = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result[j, i] = A[i, j];
                }
            }
            return result;
        }
        //public static double[] SquareMethod(double[,] A)
        //{
        //    int n = A.GetLength(0);
        //    int m = A.GetLength(1);
        //    double[,] Ta = Transposition(A);
        //    double[] result = new double[n];
        //    for (int i = 0; i < n; i++)
        //    {
        //        for (int j = 0; j < m; j++)
        //        {
        //            A *
        //        }
        //    }

        //}

    }
}
