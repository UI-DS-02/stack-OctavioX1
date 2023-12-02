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

            solveEquation.inFixToPostFix("(2.1+5)+3^356.54+4-(52*3^((3+2)))+s(33)");
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
                Console.WriteLine("Now, What Can I Do For You? \n1.Calculate \n2.Create New Operator" +
                                  "\n3.Show History \n4.Draw A Graph \n5.Exit (The Only Right Choice Here)");

                int cmd = Console.Read();

                switch (cmd)
                {
                    case '1':
                    {
                        Console.WriteLine("Enter Equation\n");

                        string equation = Console.ReadLine();

                        break;
                    }
                    case '2':
                    {
                        
                        
                        break;
                    }
                    case '3':
                    {
                        
                        
                        break;
                    }
                    case '4':
                    {
                        
                        
                        break;
                    }
                    case '5':
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
        public void solve()
        {
            string inFix = default;
            string firstNum = default;
            string secNum = default;
            Queue<string> queue = inFixToPostFix(inFix);

            for (int i = 0; i < queue.Capacity; i++)
            {
                if (queue.peek() == "+" || queue.peek() == "-" || queue.peek() == "*" || queue.peek() == "/" || queue.peek() == "^" ||
                    queue.peek() == "s" || queue.peek() == "c" || queue.peek() == "T" || queue.peek() == "C" || queue.peek() == "L")
            }
        }

        public Queue<string> inFixToPostFix(string inFix)
        {
            string currentNum = "";
            Stack<char> operators = new Stack<char>();
            List<string> list = new List<string>();

            for (int i = 0; i < inFix.Length; i++)
            {
                char currentChar = inFix[i];
                
                if ((currentChar <= '9' && currentChar >= '0') || currentChar == '.')
                {
                    currentNum += currentChar;
                }
                else
                {
                    list.Add(currentNum);
                    currentNum = "";
                    
                    if (currentChar == '(')
                    {
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
                    else
                    {
                        while (operators.Count >= 0 && getPrecedence(currentChar) <= getPrecedence(operators.peek()))
                        {
                            list.Add(operators.pop().ToString());
                        }
                    
                        operators.push(currentChar);
                    }
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
                Console.Write(list[i]);
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
            
            if (op == 's' || op == 'c' || op == 'T' || op == 'C' || op == 'l')
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
            Rear = 0;
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