using System;

namespace V1RU3_Outbreak
{
    public class BinaryPuzzle
    {
        //define global variables
        public static String targetBin { get; private set; } = "";
        public static int[] currentBin { get; set; }
        public static int[] lockedLocations { get; set; }
        public static String userBin { get; set; }
        private static int cycle = 0;

        //constructor
        public BinaryPuzzle()
        {

        }


        //generate level
        public static void GenerateLevel()
        {
            //generate target bin
            for (int i = 0; i < 20; i++)
            {
                targetBin += Game.r.Next(0, 10);
            }

            //generate current bin
            currentBin = new int[20 * 17];
            lockedLocations = new int[20 * 17];
            for (int i = 0; i < currentBin.Length; i++)
            {
                currentBin[i] = Game.r.Next(0, 10);
                lockedLocations[i] = -1;
            }
        }

        //simulate binary puzzle
        public static void Simulate()
        {
            if (cycle >= 30)
            {
                for (int i = 0; i < currentBin.Length; i++)
                {
                    if (lockedLocations[i] == -1) currentBin[i] = Game.r.Next(0, 10);
                }

                cycle = 0;
            }
            cycle++;
        }
    }
}
