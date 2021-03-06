﻿using System;
using System.Collections.Generic;

namespace V1RU3_Outbreak
{
    public class GridData
    {
        //define global variables
        public int x { get; set; }
        public int y { get; set; }
        public List<int> connections { get; set; } = new List<int>();
        public int gridSize { get; set; }
        public List<Virus> viruses { get; set; }
        public List<Block> blocks { get; set; }
        public List<Block> corruption { get; set; }
        public List<Block> importantData { get; set; }
        public List<GridConnection> gridConnections { get; set; }

        //constructor
        public GridData(int gridSize, List<Virus> viruses, List<Block> blocks, List<Block> corruption, List<Block> importantData, List<GridConnection> gridConnections, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.gridSize = gridSize;
            this.viruses = viruses;
            this.blocks = blocks;
            this.corruption = corruption;
            this.importantData = importantData;
            this.gridConnections = gridConnections;
        }
    }
}
