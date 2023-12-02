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
            
            userInterface.mainMenu();
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

                        string[] equation = Console.ReadLine().Split();

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
    
    public class Stack<T> : IEnumerable<T>
    {
        //Properties
        public T[] Array { get; set; }
        public int Count { get; set; }
        
        //Constructor
        public Stack ()
        {
            Array = new T[1000000];
            Count = -1;
        }
        
        //Functions

        //finds the highest element
        //of the stack and returns it
        public T peek()
        {
            if (Count == -1)
            {
                return default;
            }
            
            return Array[Count];
        }

        //finds the highest element in the stack
        //and removes it while also returning it
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

        //gets a new element and puts it in
        //the highest element of the stack
        public void push(T data) 
        {
            if (Count == Array.Length)
            {
                return;
            }
            Count++;
            Array[Count] = data;
        }

        //return whether the
        //stack is empty or not
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
}