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


            RingRM_CP ringRM = new RingRM_CP(testArr);
            RingRM_CP ringRm2 = new RingRM_CP(testArr);

            StdMatrixMultiplication std = new StdMatrixMultiplication();

            std.SetMatrix(testArr, testArr);
            std.Calculation();

            ringRM.Print();

            RingRM_CP ring = ringRM.Multiplication(ringRm2);

            Console.WriteLine("Резудьтат упакованной матрицы");
            ring.Print();
            ring.HardPrint();



            Console.WriteLine("Резудьтат стд. матрицы");
            std.Print();
            


            Console.ReadLine();

        }
    }
}
