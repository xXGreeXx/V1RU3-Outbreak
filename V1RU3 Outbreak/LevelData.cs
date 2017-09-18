using System;
using System.Collections.Generic;
using System.Drawing;

namespace V1RU3_Outbreak
{
    public class LevelData
    {
        //define global variables
        public int gridSize { get; set; }
        public List<Virus> baseViruses { get; set; }

        //constructor
        public LevelData(int gridSize, List<Virus> baseViruses)
        {
            this.gridSize = gridSize;
            this.baseViruses = baseViruses;
        }
    }
}
