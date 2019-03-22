using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmsSparseMatrix
{
    public class RingRM
    {
        public int[] AN;
        public int[] NR;
        public int[] NC;

        public int[] JR;
        public int[] JC;

        public RingRM()
        {
            AN = null;
            NR = null;
            NC = null;

            JR = null;
            JC = null;
        }

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
                if(preElementJC[i] >= 0)
                    NC[preElementJC[i]] = JC[i];
            }

        }

        public RingRM Multiplication(RingRM ring)
        {
            RingRM temp = new RingRM();

            // проверка условия для перемножения матриц
            if (this.JC.Length != ring.JR.Length)
            {
                throw new Exception();
            }

            bool firstInputJR = true;
            int indexNR = -1; // индекс строки
            int indexNC = -1; // индекс столбца

            List<int> listAN = new List<int>(); //вектор AN
            List<int> listNR = new List<int>(); //вектор NR
            List<int> listNC = new List<int>(); //вектор NC

            int[] locJR = new int[this.JR.Length]; //вектор JR
            int[] locJC = new int[ring.JC.Length]; //вектор JC
            int[] prelocJC = new int[ring.JC.Length]; //предыдущие элементы в столбце

            for (int i = 0; i < locJC.Length; i++)
            {
                locJC[i] = -1;
                prelocJC[i] = -1;
            }

            int buf = 0;
            for (int i = 0; i < this.JR.Length; i++)// цикл перерохода на новую строку
            {
                firstInputJR = true;

                for (int j = 0; j < ring.JC.Length; j++)// цикл перехода на новый столбец в ring
                {
                    if (this.JR[i] != -1) // если попалась строка -1 , то continue
                    {
                        indexNR = this.JR[i]; // начало выббраной строки
                    }
                    else
                    {
                        continue;
                    }

                    for (int k = 0; k < this.JR.Length; k++)// цикл прохода по выбраной строке
                    {
                        if (ring.JC[j] != -1)// если попалася столбец -1 , то continue
                        {
                            indexNC = ring.JC[j]; //начало выбраного столбца
                        }
                        else
                        {
                            continue;
                        }
                        for (int m = 0; m < ring.JC.Length; m++)// цикл прохода по выбраному стобцу в ring
                        {
                            if (getIndexColumn(indexNR) == ring.getIndexRow(indexNC)) //проерка индекса строки и стобца
                            {
                                buf += this.AN[indexNR] * ring.AN[indexNC];
                            }

                            indexNC = ring.NC[indexNC]; // следующий индекс столбца


                            if (indexNC == ring.JC[j]) //если прошли круг - выход
                            {
                                break;
                            }

                        }

                        indexNR = this.NR[indexNR]; // следующий индекс строки

                        if(indexNR == this.JR[i])//если прошли круг - выход
                        {
                            break;
                        }
                    }

                    if (buf != 0)
                    {
                        listAN.Add(buf);// заполнение AN
                        listNC.Add(-1);

                        if (firstInputJR)
                        {
                            locJR[i] = listAN.Count - 1;  //Заполняем JR
                            firstInputJR = false;
                        }
                        else
                        {
                            listNR.Add(listAN.Count - 1);//Заполняем NR
                        }

                        if (locJC[j] == -1)
                        {
                            locJC[j] = listAN.Count - 1; // заполняем JC
                        }

                        if (prelocJC[j] != -1)
                        {
                            listNC[prelocJC[j]] = listAN.Count - 1; // заполняем NC
                        }

                        prelocJC[j] = listAN.Count - 1;

                        buf = 0;
                    }


                }

                if (firstInputJR)
                {
                    locJR[i] = -1; // если нет элемнтов строке
                }
                else
                {
                    listNR.Add(locJR[i]); // если есть , то в конец NR добавляю первый элемент строки
                }
            }

            for (int i = 0; i < locJC.Length; i++)
            {
                if(locJC[i] >= 0)
                    listNC[prelocJC[i]] = locJC[i];
            }

            //заполняем локальнный объект
            temp.AN = listAN.ToArray();
            temp.NR = listNR.ToArray();
            temp.NC = listNC.ToArray();

            temp.JR = locJR;
            temp.JC = locJC;

            return temp;

        }

        public int getIndexRow(int indexColumn)// возвращает номер строки из JR по индексу столбца из NC
        {
            int temp = -2;
           
            while(temp == -2)
            {
                for(int i = 0; i < JR.Length; i++)
                {
                    if(JR[i] == indexColumn)
                    {
                        temp = i;
                        break;
                    }
                }
               
                indexColumn = NR[indexColumn];
            }

            return temp;
        }

        public int getIndexColumn(int indexRow) // возвращает номер столбца из JC по индексу строки из NR
        {
            int temp = -2;

            while (temp == -2)
            {
                for (int i = 0; i < JC.Length; i++)
                {
                    if (JC[i] == indexRow)
                    {
                        temp = i;
                        break;
                    }
                }
                indexRow = NC[indexRow];
            }
            return temp;
        }

        public RingRM Addition(RingRM ring)
        {
            RingRM temp = new RingRM();

            if ((this.JC.Length != ring.JC.Length) & (this.JR.Length != ring.JR.Length))
            {
                throw new Exception();
            }

            bool firstInputJR = true;
            int indexNR = -1; // индекс строки
            int indexNC = -1; // индекс столбца

            List<int> listAN = new List<int>(); //вектор AN
            List<int> listNR = new List<int>(); //вектор NR
            List<int> listNC = new List<int>(); //вектор NC

            int[] locJR = new int[this.JR.Length]; //вектор JR
            int[] locJC = new int[ring.JC.Length]; //вектор JC
            int[] prelocJC = new int[ring.JC.Length]; //предыдущие элементы в столбце

            for (int i = 0; i < locJC.Length; i++)
            {
                locJC[i] = -1;
                prelocJC[i] = -1;
            }

            int count = 0;

            while(this.AN.Length > count || ring.AN.Length > count)
            {
                if()

                count++;
            }
            

        }

        public bool  IsThereAnIndexRow(int i)
        {
            bool flag = false;

            for(int k = 0; k < AN.Length; k++)
            {
                if(i == getIndexRow(k))
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        public bool IsThereAnIndexColumn(int i)
        {
            bool flag = false;

            for (int k = 0; k < AN.Length; k++)
            {
                if (i == getIndexColumn(k))
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }



        public void HardPrint()
        {
            string temp = "{0, 8}";

            int[,] locArray = new int[JR.Length, JC.Length];

            for(int i = 0; i < AN.Length; i++)
            {
                locArray[getIndexRow(i), getIndexColumn(i)] = AN[i];
            }


            Console.Write("\n");
            for (int i = 0; i < JR.Length; i++)
            {
                for(int j = 0; j < JC.Length; j++)
                {
                    Console.Write(temp, locArray[i, j]);
                }
                Console.Write("\n");
            }

            Console.Write("\n");
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
            Console.Write("\n");
        }
    }
}
