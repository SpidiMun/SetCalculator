using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SetCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetResult(object sender, RoutedEventArgs e)
        {
            Calculation expressionResult = new Calculation();
            result.Text = string.Empty;
            universum.Text = string.Empty;
            result.Text = expressionResult.GetResult(arguments.Text, expression.Text);

            Arguments arg = new Arguments(arguments.Text);
            string txt = "U " + arg.Universum().GetSet();
            universum.Text = txt;
        }
    }
}
