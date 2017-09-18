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
            LevelData level1 = new LevelData(20, new List<Virus> { new Virus(1, 20), new Virus(20, 20)});
            levels.Add(level1);
        }
    }
}
