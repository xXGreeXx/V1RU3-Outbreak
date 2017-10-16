using System;
using System.Collections.Generic;

namespace V1RU3_Outbreak
{
    public class MatrixPuzzle
    {
        //define global variables
        public static int[][] matrix { get; set; }

        //constructor
        public MatrixPuzzle()
        {

        }

        //generate level
        public static void GeneratePuzzle()
        {
            //generate matrix
            matrix = new int[10][];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new int[10];
            }
        }
        
        //simulate puzzle
        public static void SimulatePuzzle()
        {

        }
    }
}
