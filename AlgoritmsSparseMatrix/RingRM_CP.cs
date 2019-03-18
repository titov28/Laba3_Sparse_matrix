using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmsSparseMatrix
{
    public class RingRM_CP
    {
        public int[] AN;
        public int[] NR;
        public int[] NC;

        public int[] JR;
        public int[] JC;

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

            for (int i = 0; i < JC.Length; i++)
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

                        if (preElementJC[j] != -1)
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

            for (int i = 0; i < JC.Length; i++)// запись индексов первых элементов столбца в последние 
            {
                NC[preElementJC[i]] = JC[i];
            }

        }

        public RingRM_CP Multiplication(RingRM_CP ring)
        {
            RingRM_CP temp;

            // проверка условия для перемножения матриц
            if (this.JC.Length != ring.JR.Length)
            {
                throw new Exception();
            }

            bool firstInputJR = true;
            bool firstInputJC = true;
            int indexNR = -1; // индекс строки
            int indexNC = -1; // индекс столбца

            List<int> listAN = new List<int>(); //вектор AN
            List<int> listNR = new List<int>(); //вектор NR
            List<int> listNC = new List<int>(); //вектор NC

            int[] locJR = new int[this.JC.Length]; //вектор JR
            int[] locJC = new int[ring.JC.Length]; //вектор JC

            int buf = 0;
            for (int i = 0; i < this.JR.Length; i++)// цикл перерохода на новую строку
            {
                for (int j = 0; j < ring.JC.Length; j++)// цикл перехода на новый столбец в ring
                {

                    indexNR = JR[i];
                    indexNC = ring.JC[j];

                    for (int k = 0; k < this.JR.Length; k++)// цикл прохода по выбранной строке
                    {
                        for (int m = 0; m < ring.JC.Length; m++)// цикл прохода по выбранному стобцу в ring
                        {
                            if (getIndexColumn(indexNR) == ring.getIndexRow(indexNC)) //проерка индекса строки и стобца
                            {
                                buf += AN[indexNR] * ring.AN[indexNC];
                            }

                            indexNC = ring.NC[indexNC];
                        }

                        if (buf != 0)
                        {
                            listAN.Add(buf);
                        }

                        if (!firstInputJR & buf != 0)// заполняем NR
                        {
                            listNR.Add(listAN.Count - 1);
                        }

                        if (firstInputJR & buf != 0) // заполняем JR 
                        {
                            locJR[i] = listAN.Count - 1;
                            firstInputJR = false;
                        }



                        if (j == ring.JC.Length - 1 & listAN.Count - 1 >= 0)//последний столбец? если да, то добавляем первый элемент строки в конец NR
                        {
                            listNR.Add(locJR[i]);
                        }



                        indexNR = NR[indexNR];
                    }
                }

                firstInputJR = true;
            }


        }

        public int getIndexRow(int indexColumn)// возвращает индекс строки по индексу столбца
        {
            int temp = -2;
            for (int i = 0; i < JC.Length; i++)
            {
                if (temp <= 0)
                {
                    for (int j = 0; j < JC.Length; j++)
                    {
                        if (JC[j] == indexColumn)
                        {
                            temp = j;
                            break;
                        }
                    }
                    indexColumn = NC[indexColumn];
                }
                else
                {
                    break;
                }
            }

            return temp;
        }

        public int getIndexColumn(int indexRow) // возвращает индекс столбца по индексу строки
        {
            int temp = -2;
            for (int i = 0; i < JR.Length; i++)
            {
                if (temp <= 0)
                {
                    for (int j = 0; j < JR.Length; j++)
                    {
                        if (JR[j] == indexRow)
                        {
                            temp = j;
                            break;
                        }
                    }
                    indexRow = NR[indexRow];
                }
                else
                {
                    break;
                }
            }

            return temp;
        }

        public void Print()
        {
            string temp = "{0, 5}";

            Console.Write("\n");
            //вывод в консоль массива AN
            Console.Write("AN: ");
            for (int i = 0; i < AN.Length; i++)
            {
                Console.Write(temp, AN[i]);
            }
            Console.Write("\n");

            //вывод в консоль массива NR
            Console.Write("NR: ");
            for (int i = 0; i < NR.Length; i++)
            {
                Console.Write(temp, NR[i]);
            }
            Console.Write("\n");

            //вывод в консоль массива NC
            Console.Write("NC: ");
            for (int i = 0; i < NC.Length; i++)
            {
                Console.Write(temp, NC[i]);
            }
            Console.Write("\n");

            //вывод в консоль массива JR
            Console.Write("JR: ");
            for (int i = 0; i < JR.Length; i++)
            {
                Console.Write(temp, JR[i]);
            }
            Console.Write("\n");

            //вывод в консоль массива JC
            Console.Write("JC: ");
            for (int i = 0; i < JC.Length; i++)
            {
                Console.Write(temp, JC[i]);
            }
            Console.Write("\n");
        }
    }
}
