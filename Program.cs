// Перечисление файлов и каталогов
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
//using System.Windows.Forms;

namespace ConsoleApp33
{
    class MyException : Exception
    {
        int number;
        public MyException(string str, int number) : base(str)
        {
            this.number = number;
        }

        public void Show()
        {
            Console.WriteLine("{0} {1}", Message, number);
        }
    }

    class MyArgs : EventArgs
    {
        public int value;

        public MyArgs(int v) : base()
        {
            value = v;
        }
    }

    class EventRaiser
    {
        public event EventHandler evhandler;

        public void Raise(int v)
        {
            if (evhandler != null)
                evhandler(this, new MyArgs(v));
        }
    }

    class MM
    {
        class Comp1 : Comparer<string>
        {
            public override int Compare(string x, string y)
            {
                return x.CompareTo(y);
            }
        }

        class Comp2 : Comparer<string>
        {
            public override int Compare(string x, string y)
            {
                int result;
                if (x.CompareTo(y) == 0)
                {
                    result = 0;
                }
                else if (x.CompareTo(y) == 1)
                {
                    result = -1;
                }
                else
                {
                    result = 1;
                }
                return result;
            }
        }


        class MySort
        {

            List<string> lastNameList;

            public MySort()
            {
                lastNameList = new List<string>();
                lastNameList.Add("Иванов");
                lastNameList.Add("Абрамов");
                lastNameList.Add("Сидоров");
                lastNameList.Add("Денисов");
                lastNameList.Add("Кокорин");

            }
            public void EventDriven(object sender, EventArgs margs)
            {
                if (((MyArgs)margs).value == 1)
                {
                    Comp1 c1 = new Comp1();
                    //MessageBox.Show("One");
                    lastNameList.Sort(c1);
                }
                else if (((MyArgs)margs).value == 2)
                {
                    Comp2 c2 = new Comp2();
                    //MessageBox.Show("Two");
                    lastNameList.Sort(c2);

                }

            }
            public void ShowNames()
            {
                foreach (string name in lastNameList)
                {
                    Console.WriteLine(name);
                }

                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            try
            {
                MySort srt = new MySort();
                srt.ShowNames();

                EventRaiser raiser = new EventRaiser();
                raiser.evhandler += srt.EventDriven;
                raiser.Raise(1);
                srt.ShowNames();
                raiser.Raise(2);
                srt.ShowNames();
            }
            catch (MyException ex)
            {
                ex.Show();
            }


            Console.Read();
        }
    }
}
