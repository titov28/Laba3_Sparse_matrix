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
                                         {0 , 50 , 0  , 0 ,  0, 0},
                                         {40, 14, 0  , 0 ,  0, 0}};

            int[,] testArr2 = new int[,] {{0 , 10, 0  , 20,  0, 0},
                                          {0 , 11, 100, 0 , 12, 0},
                                          {30, 0 , 0  , 0 , 13, 0},
                                          {0 , 0 , 0  , 15,  0, 0},
                                          {0 , 50 , 0  , 0 ,  0, 0},
                                          {40, 14, 0  , 0 ,  0, 0}};


            RingRM ringRM = new RingRM(testArr);
            RingRM ringRm2 = new RingRM(testArr2);

            Console.WriteLine("Умножение стандартный метод");

            StdMatrixMultiplication std = new StdMatrixMultiplication();
            std.SetMatrix(testArr, testArr2);
            std.Calculation();
            std.Print();

            Console.Write("\n");

            //ringRM.Print();

            //#################################################################

            Console.WriteLine("Кольцевая Рейнбольдта-Местеньи");

            //Кольцевая: Умножение
            RingRM ring1 = ringRM.Multiplication(ringRm2);

            Console.WriteLine("Умножение");
            ring1.Print();
            ring1.HardPrint();

            //Кольцевая: Сложение
            RingRM ring2 = ringRM.Addition(ringRm2);

            Console.WriteLine("Сложение");
            ring2.Print();
            ring2.HardPrint();


            //#################################################################

            Console.WriteLine("Схема Чанга и Густавсона");
            // Чанг
            SchemeCHG chg1 = new SchemeCHG(testArr);
            SchemeCHG chg2 = new SchemeCHG(testArr2);

            SchemeCHG chg = new SchemeCHG();

            chg = chg1.Addition(chg2);

            Console.WriteLine("Сложение");
            chg.Print();
            chg.HardPrint();

            Console.ReadLine();

        }
    }
}
