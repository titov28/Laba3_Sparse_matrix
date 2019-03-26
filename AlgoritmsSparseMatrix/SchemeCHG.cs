using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmsSparseMatrix
{
    public class SchemeCHG
    {
        public int[] AN;
        public int[] JA;
        public int[] JR;

        public int columns;

        public SchemeCHG()
        {
            AN = null;
            JA = null;
            JR = null;
        }

        public SchemeCHG(int[,] scheme)
        {
            List<int> listAN = new List<int>();
            List<int> listJA = new List<int>();
            List<int> listJR = new List<int>();

            bool firtstInput = true;

            columns = scheme.GetUpperBound(1) + 1;

            int count = 0;
            for(int i = 0; i < scheme.GetUpperBound(0) + 1; i++)
            {

                
                for(int j = 0; j < scheme.GetUpperBound(1) + 1; j++)
                {
                    if(scheme[i, j] != 0)
                    {
                        listAN.Add(scheme[i, j]);
                        listJA.Add(j);

                        if (firtstInput)
                        {
                            listJR.Add(count);
                            firtstInput = false;
                        }

                        count++;
                    }
                }

                listJR.Add(count);
            }

            AN = listAN.ToArray();
            JA = listJA.ToArray();
            JR = listJR.ToArray();

        }


        public SchemeCHG Addition(SchemeCHG chg)
        {
            SchemeCHG temp = new SchemeCHG();

            if(this.JR.Length - 1 != chg.JR.Length - 1 | this.columns != chg.columns)
            {
                throw new Exception();
            }

            List<int> listAN = new List<int>();
            List<int> listJA = new List<int>();
            List<int> listJR = new List<int>();


            bool firtstInput = true;
            int countFirst = 0;
            int countSecond = 0;

            int count = 0;

            for (int i = 0; i < this.JR.Length - 1; i++)
            {
                int[] temporaryArray = new int[this.columns];

                

                if(this.JR[i + 1] - this.JR[i] != 0)
                {
                    int locJRLenght = this.JR[i + 1] - this.JR[i];
                    for (int j = 0; j < locJRLenght; j++)
                    {
                        temporaryArray[this.JA[countFirst]] += this.AN[countFirst];
                        countFirst++;
                    }
                }

                if (chg.JR[i + 1] - chg.JR[i] != 0)
                {
                    int locJRLenght = chg.JR[i + 1] - chg.JR[i];
                    for (int j = 0; j < locJRLenght; j++)
                    {
                        temporaryArray[chg.JA[countSecond]] += chg.AN[countSecond];
                        countSecond++;
                    }
                }

                
                for(int j = 0; j < this.columns; j++)
                {
                    if(temporaryArray[j] != 0)
                    {
                        listAN.Add(temporaryArray[j]);

                        listJA.Add(j);

                        if (firtstInput)
                        {
                            listJR.Add(count);
                            firtstInput = false;
                        }

                        count++;
                    }
                }

                listJR.Add(count);


            }

            temp.AN = listAN.ToArray();
            temp.JA = listJA.ToArray();
            temp.JR = listJR.ToArray();

            temp.columns = this.columns;

            return temp;
        }

        public void HardPrint()
        {
            string temp = "{0, 8}";

            int[,] locArray = new int[JR.Length - 1 , columns];

            int count = 0;

            for (int i = 0; i < JR.Length - 1; i++)
            {
                int locJRLenght = JR[i + 1] - JR[i];

                for(int j = 0; j < locJRLenght; j++)
                {
                    locArray[i, JA[count]] = AN[count];
                    count++;
                }
            }


            Console.Write("\n");
            for (int i = 0; i < JR.Length - 1; i++)
            {
                for (int j = 0; j < columns; j++)
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
            Console.Write("JA: ");
            for (int i = 0; i < JA.Length; i++)
            {
                Console.Write(temp, JA[i]);
            }
            Console.Write("\n");


            //вывод в консоль массива JR
            Console.Write("JR: ");
            for (int i = 0; i < JR.Length; i++)
            {
                Console.Write(temp, JR[i]);
            }
            Console.Write("\n");

            Console.Write("\n");
        }

    }
}
