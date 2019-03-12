using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmsSparseMatrix
{
    public class RingRM_CP
    {
        int[] AN;
        int[] NR;
        int[] NC;

        int[] JR;
        int[] JC;

        public RingRM_CP(int[,] arr)
        {

            //подсчет количества ненулевых элементов в исходном массиве
            int temp = 0;
            for (int i = 0; i < arr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < arr.GetUpperBound(1) + 1; j++)
                {
                    if (arr[i, j] != 0)
                    {
                        temp++;
                    }
                }
            }


            AN = new int[temp];
            NR = new int[temp];
            NC = new int[temp];

            JR = new int[arr.GetUpperBound(0) + 1];
            JC = new int[arr.GetUpperBound(1) + 1];


            for (int i = 0; i < JR.Length; i++)
            {
                JR[i] = -1;
            }

            for (int i = 0; i < JC.Length; i++)
            {
                JC[i] = -1;
            }

            //инициализация массива AN
            Init(arr);

        }



        private void Init(int[,] arr)
        {
            int counter = 0; // счетчик
            bool firstItemFlag = true; // флаг первого входа
            int preElement = 0; // предыдущий индекс в строке

            int[] preElementJC = new int[JC.Length];// Индексы предыдущих элементов в столбцах

            for(int i =0; i < JC.Length; i++)
            {
                preElementJC[i] = -1;
            }


            for (int i = 0; i < arr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < arr.GetUpperBound(1) + 1; j++)
                {
                    if (arr[i, j] != 0)
                    {
                        AN[counter] = arr[i, j]; //инициализация массива AN
                        preElement = counter;

                        
                        if (firstItemFlag)
                        {
                            JR[i] = counter; // инициализация массива JR

                            firstItemFlag = false;
                        }
                        else
                        {
                            NR[counter - 1] = preElement; //инициализация массива NR
                        }

                        if (!firstItemFlag && JC[j] == -1) // инициализация массива JC
                        {
                            JC[j] = counter;
                        }

                        if(preElementJC[j] != -1)
                        {
                            NC[preElementJC[j]] = counter;//инициализация массива NC
                        }

                        preElementJC[j] = counter;


                        counter++;
                    }
                }

                if (firstItemFlag)// когда в строке не было элементов
                {
                    NR[counter - 1] = preElement;// запись индексов первых элементов стоки в последние
                }
                else
                {
                    NR[counter - 1] = JR[i]; // запись индексов первых элементов строки в последние
                }
                firstItemFlag = true;
            }

            for(int i = 0; i < JC.Length; i++)// запись индексов первых элементов столбца в последние 
            {
                NC[preElementJC[i]] = JC[i];
            }

        }



        public void Print()
        {
            Console.Write("\n");
            //вывод в консоль массива AN
            Console.Write("AN: ");
            for (int i = 0; i < AN.Length; i++)
            {
                Console.Write("{0} ", AN[i]);
            }
            Console.Write("\n");

            //вывод в консоль массива NR
            Console.Write("NR: ");
            for (int i = 0; i < NR.Length; i++)
            {
                Console.Write("{0} ", NR[i]);
            }
            Console.Write("\n");

            //вывод в консоль массива NC
            Console.Write("NC: ");
            for (int i = 0; i < NC.Length; i++)
            {
                Console.Write("{0} ", NC[i]);
            }
            Console.Write("\n");

            //вывод в консоль массива JR
            Console.Write("JR: ");
            for (int i = 0; i < JR.Length; i++)
            {
                Console.Write("{0} ", JR[i]);
            }
            Console.Write("\n");

            //вывод в консоль массива JC
            Console.Write("JC: ");
            for (int i = 0; i < JC.Length; i++)
            {
                Console.Write("{0} ", JC[i]);
            }
            Console.Write("\n");
        }
    }
}
