using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmsSparseMatrix
{
    public class RingRM
    {
        int[] AN;
        int[] NR;
        int[] NC;

        int[] JR;
        int[] JC;

        int[,] indexArr;

        public RingRM(int[,] arr)
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

            //копирование маасива Arr 

            InitIndexArr(arr);

            //инициализация массива AN
            InitAN(arr);

            //инициализация массива NR
            InitNR();

            //инициализация массива NC
            InitNC();

            //инициализация массива JR
            InitJR();

            //инициализация массива JC
            InitJC();

            // осовобождение маассива 
            indexArr = null;

        }

        private void InitIndexArr(int[,] arr)
        {
            int counter = 0; // счетчик элементов

            indexArr = new int[arr.GetUpperBound(0) + 1, arr.GetUpperBound(1) + 1];

            for (int i = 0; i < arr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < arr.GetUpperBound(1) + 1; j++)
                {
                    // копируем массив
                    indexArr[i, j] = arr[i, j];
                }
            }


            for (int i = 0; i < arr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < arr.GetUpperBound(1) + 1; j++)
                {
                    if (arr[i, j] != 0)
                    {
                        // находим ненулевые элементы и заменяем их индексами из AN
                        indexArr[i, j] = counter++;
                    }
                    else
                    { // все нули заменяем на -1, т.к в AN есть индекс равный нулю
                        indexArr[i, j] = -1;
                    }

                }
            }
        }

        private void InitAN(int[,] arr)
        {
            int counter = 0;

            for (int i = 0; i < arr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < arr.GetUpperBound(1) + 1; j++)
                {
                    if (arr[i, j] != 0)
                    {
                        AN[counter++] = arr[i, j];
                    }
                }
            }
        }

        private void InitNR()
        {
            int temp = -1; // индекс первого лемента в строке
            int preElement = 0; // индекс предыдущего элемента

            for (int i = 0; i < indexArr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < indexArr.GetUpperBound(1) + 1; j++)
                {
                    if (indexArr[i, j] >= 0)// найден элеент
                    {

                        if (temp >= 0)// присваиваем предыдущему элементу индекс следующего
                        {
                            NR[preElement] = indexArr[i, j];
                            preElement = indexArr[i, j];
                        }

                        if (temp == -1) // сохранение индекса первого элемнта в строке
                        {
                            temp = indexArr[i, j];
                            preElement = temp;
                        }
                    }
                }

                if (temp >= 0)// если элементов в строке больше одного
                {
                    NR[preElement] = temp;
                }
                else // если элемент в строке один
                {
                    NR[preElement] = preElement;
                }

                temp = -1;
            }

        }

        private void InitNC()
        {
            int temp = -1; // индекс первого элемента в столбце
            int preElement = 0; // индекс предыдущего элемента

           
            for (int i = 0; i < indexArr.GetUpperBound(1) + 1; i++)
            {
                for (int j = 0; j < indexArr.GetUpperBound(0) + 1; j++)
                {
                    if (indexArr[j, i] >= 0)// нашли элемент в столбце 
                    {
                        if(temp >= 0)
                        {
                            NC[preElement] = indexArr[j, i];
                            preElement = indexArr[j, i];
                        }

                        if(temp < 0) // сохранение индекса первого элемента в столбце 
                        {
                            temp = indexArr[j, i];
                            preElement = temp; // первый элемент стал предыдущим
                        }

                    }
                }

                if (temp >= 0) { // если элементов в столбце больше одного
                    NC[preElement] = temp;
                }
                else // если элемент в столбце один
                {
                    NC[preElement] = preElement;
                }

                temp = -1;

            }

        }

        private void InitJR()
        {
            int counter = 0;
            for (int i = 0; i < indexArr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < indexArr.GetUpperBound(1) + 1; j++)
                {
                    if (indexArr[i, j] >= 0)// найден элеент
                    {
                        JR[counter++] = indexArr[i, j];
                        break; 
                    }

                    if(j == indexArr.GetUpperBound(1))
                    {
                        JR[counter++] = -1;
                    }

                }

                
            }
        }

        private void InitJC()
        {
            int counter = 0;
            for (int i = 0; i < indexArr.GetUpperBound(1) + 1; i++)
            {
                for (int j = 0; j < indexArr.GetUpperBound(0) + 1; j++)
                {
                    if (indexArr[j, i] >= 0)// найден элеент
                    {
                        JC[counter++] = indexArr[j, i];
                        break;
                    }
                }
            }
        }


        public void Print()
        {
            Console.Write("\n");
            //вывод в консоль массива AN
            Console.Write("AN: ");
            for(int i = 0; i < AN.Length; i++)
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
