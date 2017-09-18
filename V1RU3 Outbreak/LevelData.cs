using System;
using System.Collections.Generic;
using System.Drawing;

namespace V1RU3_Outbreak
{
    public class LevelData
    {
        //define global variables
        public int gridSize { get; set; }
        public List<Virus> viruses { get; set; }
        public List<Block> blocks { get; set; }

        //constructor
        public LevelData(int gridSize, List<Virus> viruses, List<Block> blocks)
        {
            this.gridSize = gridSize;
            this.viruses = viruses;
            this.blocks = blocks;
        }
    }
}
