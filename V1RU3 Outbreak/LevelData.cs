using System;
using System.Collections.Generic;
using System.Drawing;

namespace V1RU3_Outbreak
{
    public class LevelData
    {
        //define global variables
        public List<GridData> grids { get; set; } = new List<GridData>();

        //constructor
        public LevelData(List<GridData> grids)
        {
            this.grids = grids;
        }

        //count viruses
        public int CountViruses()
        {
            int count = 0;
            foreach (GridData grid in grids)
            {
                count += grid.viruses.Count;
            }

            return count;
        }

        //count data
        public int CountData()
        {
            int count = 0;
            foreach (GridData grid in grids)
            {
                count += grid.importantData.Count;
            }

            return count;
        }

        //count grid tiles
        public int CountGridTiles()
        {
            int count = 0;

            foreach (GridData grid in grids)
            {
                count += grid.gridSize;
            }

            return count;
        }
    }
}
