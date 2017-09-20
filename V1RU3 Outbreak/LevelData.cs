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
        public List<Block> corruption { get; set; }
        public List<Block> importantData { get; set; }

        //constructor
        public LevelData(int gridSize, List<Virus> viruses, List<Block> blocks, List<Block> corruption, List<Block> importantData)
        {
            this.gridSize = gridSize;
            this.viruses = viruses;
            this.blocks = blocks;
            this.corruption = corruption;
            this.importantData = importantData;
        }
    }
}
