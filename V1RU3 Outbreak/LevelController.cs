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
            corruption.Add(new Block(11, 20));
            corruption.Add(new Block(11, 19));
            corruption.Add(new Block(11, 18));
            corruption.Add(new Block(11, 17));
            corruption.Add(new Block(10, 17));
            corruption.Add(new Block(9, 17));
            corruption.Add(new Block(8, 17));

            List<Block> importantData = new List<Block>();

            LevelData level1 = new LevelData(20, viruses, new List<Block>(), corruption, importantData);
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

            importantData = new List<Block>();
            importantData.Add(new Block(10, 9));

            LevelData level2 = new LevelData(20, viruses, new List<Block>(), corruption, importantData);
            levels.Add(level2);
        }
    }
}
