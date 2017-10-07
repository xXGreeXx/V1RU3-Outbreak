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
        public List<Virus> SimulateAI(GridData data)
        {
            List<Virus> virusesToReturn = new List<Virus>();

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

                    float newX = v.x - virusX;
                    float newY = v.y - virusY;
                    if (v.targetX != -1 && v.targetY != -1)
                    {
                        newX = v.targetX - virusX;
                        newY = v.targetY - virusY;
                    }

                    //check if spot is not valid
                    foreach (Block b in data.blocks.Concat(data.corruption))
                    {
                        if (b.x == newX && b.y == newY)
                        {
                            pass = false;
                            break;
                        }
                    }

                    foreach (Virus virusToCheck in data.viruses.Concat(virusesToReturn))
                    {
                        if ((virusToCheck.x == newX || virusToCheck.targetX == newX) && (virusToCheck.y == newY || virusToCheck.targetY == newY))
                        {
                            pass = false;
                            break;
                        }
                    }

                    if (newX < 1 || newY < 1 || newX > 20 || newY > 20)
                    {
                        pass = false;
                        continue;
                    }

                    //add virus if spot is valid
                    Virus vToAdd = new Virus(v.x, v.y);
                    vToAdd.targetX = newX;
                    vToAdd.targetY = newY;
                    if (pass) { virusesToReturn.Add(vToAdd); break; }

                    tries++;
                }
                while (tries < 40);
            }

            return virusesToReturn;
        }
    }
}
