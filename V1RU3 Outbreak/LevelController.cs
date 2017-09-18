using System;
using System.Collections.Generic;

namespace V1RU3_Outbreak
{
    public class LevelController
    {
        //define global variables
        public List<LevelData> levels { get; } = new List<LevelData>();

        //constructor
        public LevelController()
        {
            //level 1
            List<Virus> viruses = new List<Virus>();
            viruses.Add(new Virus(10, 20));

            List<Block> corruption = new List<Block>();
            corruption.Add(new Block(10, 10));

            LevelData level1 = new LevelData(20, viruses, new List<Block>(), corruption);
            levels.Add(level1);

            //level 2
            viruses = new List<Virus>();
            viruses.Add(new Virus(10, 20));

            corruption = new List<Block>();
            corruption.Add(new Block(10, 10));
            corruption.Add(new Block(11, 10));
            corruption.Add(new Block(12, 10));
            corruption.Add(new Block(13, 10));
            corruption.Add(new Block(14, 10));
            corruption.Add(new Block(15, 10));
            corruption.Add(new Block(15, 11));

            LevelData level2 = new LevelData(20, viruses, new List<Block>(), corruption);
            levels.Add(level2);
        }
    }
}
