using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StackProject
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            UserInterface userInterface = new UserInterface();
            SolveEquation solveEquation = new SolveEquation();

            solveEquation.solve("s(p)");
        }
    }

    public class UserInterface
    {
        public void mainMenu()
        {
            bool exit = default;
            
            Console.WriteLine("Hello And Welcome To Our App, This Is The Worst Calculator Ever Written In The Whole World Of Calculators! \n" +
                              "\nDISCLAIMER : We DO NOT recommend using this app, the only reason i wrote this useless piece of shit \n" +
                              "is because i was held at gunpoint by Matin Aazami, if you are reading this, please help me!!!\n");
            
            while (!exit)
            {
                Console.WriteLine("Now, What Can I Do For You? \n1.Calculate\n2.Show History \n3.Exit (The Only Right Choice Here)");

                int cmd = Console.Read();

                switch (cmd)
                {
                    case '1':
                    {
                        Console.WriteLine("Enter Equation\n");

                        string equation = Console.ReadLine();

                        SolveEquation solveEquation = new SolveEquation();
                        
                        solveEquation.solve(equation);

                        break;
                    }
                    case '2':
                    {
                        
                        
                        break;
                    }
                    case '3':
                    {
                        exit = true;

                        Console.WriteLine("Arrivederci!");

                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Wrong Command!");
                        break;
                    }
                }
            }
        }
    }
    
    public class SolveEquation
    {
        public void solve(string inFix)
        {
            Queue<string> queue = inFixToPostFix(inFix);
            Stack<string> stack = new Stack<string>();
            
            if (queue == null)
            {
                Console.WriteLine("Invalid Arguement!");
                
                return;
            }

            while(true)
            {
                if (queue.Rear == -1 || queue.peek() == null)
                {
                    break;
                }
                
                string current = queue.Dequeue();

                if (current == "")
                {
                    continue;
                }
                else if ("+-*/^".Contains(current))
                {
                    double num2 = double.Parse(stack.pop());
                    double num1 = double.Parse(stack.pop());
                    stack.push(doubleOperator(char.Parse(current) , num1 , num2).ToString());
                }
                else if ("scTC".Contains(current))
                {
                    
                    stack.push(singleOperator(char.Parse(current), double.Parse(stack.pop())).ToString());
                }
                else
                {
                    stack.push(current);
                }
            }

            Console.WriteLine(stack.Array[0]);
        }
        
        public Double singleOperator(char op , double num)
        {
            double answer = default;

            switch (op)
            {
                case 's':
                {
                    return Math.Sin(num);
                }
                case 'c':
                {
                    return Math.Cos(num);
                }
                case 'T':
                {
                    return Math.Tan(num);
                }
                case 'C':
                {
                    return (Math.Cos(num)/Math.Sin(num));
                }
            }

            return answer;
        }
        
        public Double doubleOperator(char op , double num1 , double num2)
        {
            double answer = default;

            switch (op)
            {
                case '+':
                {
                    return (num1 + num2);
                }
                case '-':
                {
                    return (num1 - num2);
                }
                case '*':
                {
                    return (num1 * num2);
                }
                case '/':
                {
                    return (num1 / num2);
                }
                case '^':
                {
                    return (Math.Pow(num1, num2));
                }
            }
            
            return answer;
        }

        public Queue<string> inFixToPostFix(string inFix)
        {
            string currentNum = "";
            Stack<char> operators = new Stack<char>();
            List<string> list = new List<string>();

            for (int i = 0; i < inFix.Length; i++)
            {
                char currentChar = inFix[i];
                
                if ((currentChar <= '9' && currentChar >= '0') || currentChar == '.' || currentChar == 'p' || currentChar == 'e')
                {
                    currentNum += currentChar;
                }
                else
                {
                    if (currentNum == "p")
                    {
                        currentNum = Math.PI.ToString();
                    }
                    if (currentNum == "e")
                    {
                        currentNum = Math.E.ToString();
                    }
                    if (currentNum != "")
                    {
                        list.Add(currentNum);
                    }
                    currentNum = "";
                    
                    if (currentChar == '(')
                    {
                        if (operators.peek() == '-')
                        {
                            list.Add("0");
                        }
                        
                        operators.push(currentChar);
                    }
                    else if (currentChar == ')')
                    {
                        while (operators.Count >= 0 && !operators.peek().Equals('('))
                        {
                            list.Add(operators.pop().ToString());
                        }
                        
                        operators.pop();
                    }
                    else if ("+-*/^scTC".Contains(currentChar))
                    {
                        while (operators.Count >= 0 && getPrecedence(currentChar) <= getPrecedence(operators.peek()))
                        {
                            list.Add(operators.pop().ToString());
                        }
                    
                        operators.push(currentChar);
                    }
                    else
                    {
                        return null;
                    }
                }

                if (inFix.Length - 1 == i)
                {
                    if (currentNum == "p")
                    {
                        currentNum = Math.PI.ToString();
                    }
                    if (currentNum == "e")
                    {
                        currentNum = Math.E.ToString();
                    }
                    list.Add(currentNum);
                }
            }

            while (operators.Count >= 0)
            {
                list.Add(operators.pop().ToString());
            }

            Queue<string> queue = new Queue<string>(list.Count + 1);
            
            for (int i = 0; i < list.Count; i++)
            {
                queue.Enqueue(list[i]);
            }

            return queue;
        }

        public int getPrecedence(char op)
        {
            if (op == '+' || op == '-')
            {
                return 1;
            } 
            
            if (op == '*' || op == '/')
            {
                return 2;
            }
            
            if (op == '^')
            {
                return 3;
            }
            
            if (op == 's' || op == 'c' || op == 'T' || op == 'C')
            {
                return 4;
            }
            
            return -1;
        }
    }
    
    public class Stack<T> : IEnumerable<T>
    {
        //-------------------------------Propeties-------------------------------
        public T[] Array { get; set; }
        public int Count { get; set; }
        
        //-------------------------------Constructor-------------------------------
        public Stack ()
        {
            Array = new T[1000000];
            Count = -1;
        }
        
        //-------------------------------Functions-------------------------------

        //finds the highest element of the stack and returns it
        public T peek()
        {
            if (Count == -1)
            {
                return default;
            }
            
            return Array[Count];
        }

        //finds the highest element in the stack and removes it while also returning it
        public T pop()
        {
            if (Count == -1)
            {
                return default;
            }
            
            T temp = Array[Count];
            Array[Count] = default(T);
            Count--;
            return temp;
        }

        //gets a new element and puts it in the highest element of the stack
        public void push(T data) 
        {
            if (Count == Array.Length)
            {
                return;
            }
            Count++;
            Array[Count] = data;
        }

        //return whether the stack is empty or not
        public bool isEmpty()
        {
            if (Count ==-1)
            {
                return true;
            }

            return false;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return Array.Take(Count).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Queue<T>
    {
        //-------------------------------Propeties-------------------------------
        
        public T[] Array { get; set; }
        public int Rear { get; set; }
        public int Capacity { get; set; }
        
        //-------------------------------Constructor-------------------------------

        public Queue(int capacity)
        {
            Capacity = capacity;
            Array = new T[Capacity];
            Rear = -1;
        }
        
        //-------------------------------Functions-------------------------------

        //Adds The Given Element To The End Of The Queue (If The Queue Is Not Full)
        public void Enqueue(T data)
        {
            if (Rear == Capacity - 1)
            {
                Console.WriteLine("Queue Is Full Ya Dumb Fuck!");
                Console.WriteLine("Rear : " + Rear);
                Console.WriteLine("Capacity : " + Capacity);
            }
            else
            {
                Rear++;
                Array[Rear] = data;
            }
        }
        
        //Removes The First Element Of The Queue (If The Queue Is Not Empty)
        public T Dequeue()
        {
            T data = Array[0];

            for (int i = 0; i < Capacity - 1; i++)
            {
                Array[i] = Array[i + 1];
            }

            Array[Capacity - 1] = default;
            Rear--;

            return data;
        }

        public T peek()
        {
            return Array[0];
        }

        //Prints All Elements In The Queue
        public void Print()
        {
            while (true)
            {
                if (Rear == 0)
                {
                    Console.WriteLine(Dequeue());
                    break;
                }
                Console.WriteLine(Dequeue());
            }
        }
    }
}