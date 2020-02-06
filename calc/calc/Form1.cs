using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calc
{
    public partial class Form1 : Form
    {
        private Stack<double> stack;
        private Stack<string> operators;
        public Form1()
        {
            InitializeComponent();
            stack = new Stack<double>();
            operators = new Stack<string>();
        }

        private void EnterNum(object sender, EventArgs e)
        {
            if (result.Text == "0" || result.Text=="error")
                result.Clear();
            result.Text += ((Button)sender).Text;
        }

        private void Calculate(object sender, EventArgs e)
        {
            try
            {
                stack.Push(Double.Parse(result.Text));
                switch (((Button)sender).Text[0])
                {
                    case '*':
                    case '+':
                    case '-':
                    case '/':                  
                        result.Text = "0";
                        CalculatePrev();
                        operators.Push(((Button)sender).Text);
                        break;
                    default:
                        CalculatePrev();
                        result.Text = stack.Pop().ToString();
                        break;
                }
            }
            catch(ArithmeticException ex)
            {
                result.Text = "error";
                stack.Clear();
                operators.Clear();
            }

        }
        private void CalculatePrev ()
        {
            
            if (stack.Count > 1)
            {
                double b = stack.Pop();
                double a = stack.Pop();
                string oper = operators.Pop();
                if (oper == "/")
                {
                    
                        if (b == 0)
                            throw new ArithmeticException();
                        stack.Push(a/b);
                    
                }else if(oper=="*") stack.Push(a * b);
                else if(oper=="+") stack.Push(a + b);
                else if(oper=="-")stack.Push(a - b);

            }
        }
        private void Clear(object sender, EventArgs e)
        {
            stack.Clear();
            operators.Clear();
            result.Text = "0";
        }
    }
}
