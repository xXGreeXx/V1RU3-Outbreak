using System;
using System.Collections.Generic;
using System.Linq;

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
            List<Virus> virusesToReturn = new List<Virus>();
            Boolean nothingCanMove = true;

            foreach (Virus v in data.viruses)
            {
                Boolean pass = true;
                int tries = 0;

                do
                {
                    //define variables
                    pass = true;

                    int virusX = 0;
                    int virusY = 0;

                    virusX = Game.r.Next(-1, 2);
                    virusY = Game.r.Next(-1, 2);

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

                    List<Virus> virusesToIterate = data.viruses.Concat(virusesToReturn).ToList();
                    foreach (Virus virusToCheck in virusesToIterate)
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
                    if (pass) virusesToReturn.Add(new Virus(newX, newY)); nothingCanMove = false;

                    tries++;
                }
                while (tries < 4);
            }

            return virusesToReturn;
        }
    }
}
