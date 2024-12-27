using slau;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace чм_лаба_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Method()
        {
            double[,] A = new double[,] { { 17, 12, 16 },{ 12, 16, 3 },{ 1, 3, 8 } };
            double[] B = new double[] { 5, 7, 9 };
//            double [,] A = new double[,] { { 3.0, 1.0, 0.0 }, { -1.0, 4.0, 2.0 }, { 0.0, 2.0, 3.0 } };
//            double[] B = new double[] { 1.0, -3.0, 5.0 };

            int n = A.GetLength(0);
            if (MatrixMetod.IsChecked == true)
            {
                Sh.Text = "";
                try
                {
                    double[] result = Matrix.SolveLinearSystem(A, B);
                    Sh.Text = string.Join(", ", result.Select((value, index) => $"x{index + 1} = {value:F2}"));
                }
                catch (Exception ex)
                {
                    Sh.Text = "Error: " + ex.Message;
                }
            }
            if (GaussMetod.IsChecked == true)
            {
                Sh.Text = "";
                try
                {
                    double[] result = Matrix.Solve(A, B, n);
                    Sh.Text = string.Join(", ", result.Select((value, index) => $"x{index + 1} = {value:F2}"));
                }
                catch (Exception ex)
                {
                    Sh.Text = "Ошибка: " + ex.Message;
                }
            }
            if (SquareMethod.IsChecked == true)
            {
                    Sh.Text = "";
                    try
                    {
                        double[] result = Matrix.SquareSolve(A, B);
                        Sh.Text = string.Join(", ", result.Select((value, index) => $"x{index + 1} = {value:F2}"));
                    }
                    catch (Exception ex)
                    {
                        Sh.Text = "Ошибка: " + ex.Message;
                    }
                
            }
            if (ZeydelMethod.IsChecked == true)
            {
                Sh.Text = "";
                try
                {
                    double[] result = Matrix.ZeydelMethod(A, B);
                    Sh.Text = string.Join(", ", result.Select((value, index) => $"x{index + 1} = {value:F2}"));
                }
                catch (Exception ex)
                {
                    Sh.Text = "Ошибка: " + ex.Message;
                }

            }
            if (GaussJordan.IsChecked == true)
            {
                Sh.Text = "";
                try
                {
                    double[] result = Matrix.GausseJordan(A, B,n);
                    Sh.Text = string.Join(", ", result.Select((value, index) => $"x{index + 1} = {value:F2}"));
                }
                catch (Exception ex)
                {
                    Sh.Text = "Ошибка: " + ex.Message;
                }

            }
            if (LU.IsChecked == true)
            {
                Sh.Text = "";
                try
                {
                    double[] result = Matrix.LU (A,B);
                    Sh.Text =string.Join(", ", result.Select((value, index) => $"x{index + 1} = {value:F2}"));
                }
                catch(Exception ex)
                {
                    Sh.Text = "Ошибка: " + ex.Message;
                }
            }

            if (Kramer.IsChecked == true)
            {
                Sh.Text = "";
                try
                {

                    List<Equation> eqList = new List<Equation>();
                    double[] e = new double[A.GetLength(0)];
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        double[] z = new double[A.GetLength(1)];
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            z[j] = A[i, j];
                        }
                        Equation eq = new Equation(z, B[i]);
                        eqList.Add(eq);
                    }

                    var sysEq = new SysEq(eqList);

                    var answer = sysEq.SolveKramer();
                    if (sysEq.Status == true)
                    {

                        for (int i = 0; i < answer.Length; i++)
                        {
                            Sh.Text = Sh.Text + $"x{i + 1}={answer[i]}, ";
                        }

                    }

                }

                catch (Exception ex)
                {
                    Sh.Text = "Ошибка: " + ex.Message;
                }


            }

            if (Progon.IsChecked == true)
            {
                Sh.Text = "";
                try
                {

                    List<Equation> eqList = new List<Equation>();
                    double[] e = new double[A.GetLength(0)];
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        double[] z = new double[A.GetLength(1)];
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            z[j] = A[i, j];
                        }
                        Equation eq = new Equation(z, B[i]);
                        eqList.Add(eq);
                    }

                    var sysEq = new SysEq(eqList);

                    var answer = sysEq.SolveProgonka();
                    Sh.Text = "";
                    if (sysEq.Status == true)
                    {

                        for (int i = 0; i < answer.Length; i++)
                        {
                            Sh.Text = Sh.Text + $"x{i + 1}={answer[i]}, ";
                        }

                    }
                    else
                    {
                        Sh.Text = "Ошибка: найти корни не удалось";
                    }

                }

                catch (Exception ex)
                {
                    Sh.Text = "Ошибка: " + ex.Message;
                }
            }

            if (SimpleIteration.IsChecked == true)
            {
                Sh.Text = "";
                try
                {

                    List<Equation> eqList = new List<Equation>();
                    double[] e = new double[A.GetLength(0)];
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        double[] z = new double[A.GetLength(1)];
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            z[j] = A[i, j];
                        }
                        Equation eq = new Equation(z, B[i]);
                        eqList.Add(eq);
                    }

                    var sysEq = new SysEq(eqList);

                    var answer = sysEq.SolveSimpleIterations(0.01, 1000);
                    if (sysEq.Status == true)
                    {

                        for (int i = 0; i < answer.Length; i++)
                        {
                            Sh.Text = Sh.Text + $"x{i + 1}={answer[i]}, ";
                        }

                    }
                    else
                    {
                            Sh.Text = "Ошибка: найти корни не удалось";
                    }

                }

                catch (Exception ex)
                {
                    Sh.Text = "Ошибка: " + ex.Message;
                }
            }

        }


        private void Start_Click(object sender, RoutedEventArgs e)
            {

                Method();
            }

        private void SimpleIteration_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
    }

