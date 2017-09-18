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
            viruses.Add(new Virus(1, 20));
            viruses.Add(new Virus(20, 20));

            LevelData level1 = new LevelData(20, viruses, new List<Block>());
            levels.Add(level1);
        }
    }
}
