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
using static System.Convert;

namespace MyCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum SetOperation { Undef, Sum, Minus, Mult, Div }
        public SetOperation op = SetOperation.Undef;
        public bool operationExec = false;
        public double res;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnCE_Click(object sender, RoutedEventArgs e)
        {
            CurrentOperation.Clear();
            CurrentOperation.Text = "0";
        }
        private void BtnC_Click(object sender, RoutedEventArgs e)
        {
            CurrentOperation.Clear();
            CurrentOperation.Text = "0";
            PrevoisOperation.Clear();
        }
        private void BtnDigit_Click(object sender, RoutedEventArgs e)
        {
            // считываем текущее значение 
            string curr = CurrentOperation.Text.ToString();
            Button btn = (Button)sender;
            // если текущее значение - "0" или была нажата кнопка арифметического действия
            if (curr.Equals("0") || operationExec)
            {
                operationExec = false;
                // запоминаем текущую запись в результат
                res = ToDouble(curr);
                // очищаем поле 
                CurrentOperation.Clear();
                // записываем в поле контент кнопки
                CurrentOperation.Text = btn.Content.ToString();
            }
            else // если в поле уже есть записи и не была нажата кнопка арифметического действия
            {
                // сцепляем текущее и новое значение (контент кнопки)
                CurrentOperation.Text = curr + btn.Content.ToString();
            }
        }
        private void BtnOperation_Click(object sender, RoutedEventArgs e)
        {
            operationExec = true;
            // считываем текущее значение
            string curr = CurrentOperation.Text.ToString();
            double tempRes = ToDouble(curr);
            Button btn = (Button)sender;
            // определяем тип операции, в зависимости от контента кнопки
            switch (btn.Content.ToString())
            {
                case "+":
                    op = SetOperation.Sum;
                    res += tempRes;
                    break;
                case "-":
                    op = SetOperation.Minus;
                    res -= tempRes;
                    break;
                case "*":
                    op = SetOperation.Mult;
                    res *= tempRes;
                    break;
                case "/":
                    op = SetOperation.Div;
                    if ((int)tempRes != 0)
                    {
                        res /= tempRes;
                    }
                    break;
            }
            PrevoisOperation.Text = curr + btn.Content.ToString();
        }
        private void BtnEq_Click(object sender, RoutedEventArgs e)
        {
            bool divisionByZero = false;
            // считываем предыдущее значение
            string prev = PrevoisOperation.Text.ToString();
            // считываем текущее значение
            string curr = CurrentOperation.Text.ToString();
            double tempRes = ToDouble(curr);
            switch (op)
            {
                case SetOperation.Sum:
                    op = SetOperation.Sum;
                    res += tempRes;
                    break;
                case SetOperation.Minus:
                    op = SetOperation.Minus;
                    res -= tempRes;
                    break;
                case SetOperation.Mult:
                    op = SetOperation.Mult;
                    res *= tempRes;
                    break;
                case SetOperation.Div:
                    op = SetOperation.Div;
                    if ((int)tempRes != 0)
                    {
                        res /= tempRes;
                    }
                    else
                    {
                        divisionByZero = true;
                    }
                    break;
            }
            PrevoisOperation.Text = prev + curr + "=";
            CurrentOperation.Clear();
            if (divisionByZero)
            {
                CurrentOperation.Text = "Деление на ноль невозможно!!!!!";
            }
            else
            {
                CurrentOperation.Text = res.ToString();
            }
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // считываем текущее значение
            string curr = CurrentOperation.Text.ToString();
            int len = curr.Length;
            if (len == 1)
            {
                curr = "0";
            }
            else
            {
                curr = curr.Remove(curr.Length - 1);
            }
            CurrentOperation.Clear();
            CurrentOperation.Text = curr;
        }
        private void BtnPoint_Click(object sender, RoutedEventArgs e)
        {
            // считываем текущее значение
            string curr = CurrentOperation.Text.ToString();
            if (!curr.Contains(","))
            {
                curr += ",";
            }
            CurrentOperation.Clear();
            CurrentOperation.Text = curr ;
        }
    }
}
