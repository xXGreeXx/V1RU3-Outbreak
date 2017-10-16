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
                List<int[]> possible = new List<int[]>{ new int[]{-1, 1}, new int[] {0, 1}, new int[] {1, 1}, new int[] {1, 0}, new int[] {1, 1}, new int[] {0, -1}, new int[] {-1, -1}, new int[] {-1, 0} };
                Boolean pass = true;

                do
                {
                    //define variables
                    pass = true;

                    int possibleIndex = Game.r.Next(0, possible.Count);

                    int virusX = possible[possibleIndex][0];
                    int virusY = possible[possibleIndex][1];

                    float newX = v.x - virusX;
                    float newY = v.y - virusY;

                    possible.RemoveAt(possibleIndex);

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
                    }

                    //check if spot connects with grid
                    //TODO\\
                    foreach (GridConnection grid in data.gridConnections)
                    {
                        int startX = grid.baseOffset[0];
                        int startY = grid.baseOffset[1];

                        if (virusX == startX && virusY == startY)
                        {
                            virusX = grid.targetOffset[0];
                            virusY = grid.targetOffset[1];
                        }
                    }

                    //add virus if spot is valid
                    Virus vToAdd = new Virus(v.x, v.y, v.type);
                    vToAdd.targetX = newX;
                    vToAdd.targetY = newY;
                    if (pass) { virusesToReturn.Add(vToAdd); break; }
                }
                while (possible.Count > 0);
            }

            return virusesToReturn;
        }
    }
}
