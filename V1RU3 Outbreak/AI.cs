using System;
using System.Collections.Generic;

namespace V1RU3_Outbreak
{
    public class AI
    {
        //constructor
        public AI()
        {

        }

        //simulate ai 
        public List<Virus> SimulateAI(LevelData data)
        {
            List<Virus> virsuesToReturn = new List<Virus>();
            Boolean nothingCanMove = true;

            foreach (Virus v in data.viruses)
            {
                Boolean pass = true;
                int tries = 0;

                do
                {
                    //define variables
                    pass = true;

                    int virusX = Game.r.Next(-1, 2);
                    int virusY = Game.r.Next(-1, 2);

                    if ((virusX == -1 && virusY == -1) || (virusX == 1 && virusY == 1) || (virusX == -1 && virusY == 1) || (virusX == 1 && virusX == -1)) continue;

                    int newX = v.x - virusX;
                    int newY = v.y - virusY;

                    //check if spot is not valid
                    foreach (Block b in data.blocks)
                    {
                        if (b.x == newX && b.y == newY)
                        {
                            pass = false;
                            break;
                        }
                    }

                    foreach (Virus virusToCheck in data.viruses)
                    {
                        if (virusToCheck.x == newX && virusToCheck.y == newY)
                        {
                            pass = false;
                            break;
                        }
                    }

                    if (newX < 1 || newY < 1 || newX > 20 || newY > 20)
                    {
                        pass = false;
                    }

                    //add virus if spot is valid
                    if (pass) virsuesToReturn.Add(new Virus(newX, newY)); nothingCanMove = false;

                    tries++;
                    if (tries >= 6)
                    {
                        break;
                    }
                }
                while (pass == false);
            }

            return virsuesToReturn;
        }
    }
}
