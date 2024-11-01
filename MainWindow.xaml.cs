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
            double[,] A = new double[,] { { 1, 2, 3 }, { 2, 2, 2 }, { 3, 4, 4 } };
            double[] B = new double[] { 1, 2, 3 };
            int n = A.GetLength(0);
            if (MatrixMetod.IsChecked == true)
            {
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
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {

            Method();
        }
    }
}