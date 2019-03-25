using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoritmsSparseMatrix;
using MatrixMultiplicationAlgorithms;

namespace Laba3_SparseMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] testArr = new int[,] {{0 , 10, 0  , 20,  0, 0},
                                         {0 , 11, 100, 0 , 12, 0},
                                         {30, 0 , 0  , 0 , 13, 0},
                                         {0 , 0 , 0  , 15,  0, 0},
                                         {0 , 0 , 0  , 0 ,  0, 0},
                                         {40, 14, 0  , 0 ,  0, 0}};

            int[,] testArr2 = new int[,] {{0 , 10, 0  , 20,  0, 0},
                                          {0 , 11, 100, 0 , 12, 0},
                                          {30, 0 , 0  , 0 , 13, 0},
                                          {0 , 0 , 0  , 15,  0, 0},
                                          {0 , 0 , 1  , 0 ,  0, 0},
                                          {40, 14, 0  , 0 ,  0, 0}};


            RingRM ringRM = new RingRM(testArr);
            RingRM ringRm2 = new RingRM(testArr2);

            
            ringRM.Print();

            //RingRM ring = ringRM.Multiplication(ringRm2);

            RingRM ring = ringRM.Addition(ringRm2);

            Console.WriteLine("Резудьтат упакованной матрицы");
            ring.Print();
            ring.HardPrint();


            Console.ReadLine();

        }
    }
}
