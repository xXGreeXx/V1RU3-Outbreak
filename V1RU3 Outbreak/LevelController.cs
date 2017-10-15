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
            #region Level1
            List<GridData> grids = new List<GridData>();

            //grid 1
            List<GridConnection> gridConnections = new List<GridConnection>();

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
            importantData.Add(new Block(11, 16));

            grids.Add(new GridData(20, viruses, new List<Block>(), corruption, importantData, gridConnections, 0, 0));
            LevelData level = new LevelData(grids);
            levels.Add(level);
            #endregion

            #region Level2
            grids = new List<GridData>();

            //grid 1
            gridConnections = new List<GridConnection>();

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

            grids.Add(new GridData(20, viruses, new List<Block>(), corruption, importantData, gridConnections, 0, 0));
            level = new LevelData(grids);
            levels.Add(level);
            #endregion

            #region Level3
            grids = new List<GridData>();

            //grid 1
            gridConnections = new List<GridConnection>();

            viruses = new List<Virus>();
            viruses.Add(new Virus(1, 20));
            viruses.Add(new Virus(20, 20));

            corruption = new List<Block>();
            corruption.Add(new Block(10, 10));
            corruption.Add(new Block(11, 10));
            corruption.Add(new Block(12, 10));
            corruption.Add(new Block(9, 10));
            corruption.Add(new Block(8, 10));
            corruption.Add(new Block(10, 9));
            corruption.Add(new Block(10, 8));
            corruption.Add(new Block(10, 11));
            corruption.Add(new Block(10, 12));

            importantData = new List<Block>();
            importantData.Add(new Block(10, 1));
            importantData.Add(new Block(1, 1));
            importantData.Add(new Block(20, 1));

            grids.Add(new GridData(20, viruses, new List<Block>(), corruption, importantData, gridConnections, 0, 0));
            level = new LevelData(grids);
            levels.Add(level);
            #endregion

            #region Level4
            grids = new List<GridData>();

            //grid 1
            gridConnections = new List<GridConnection>();

            viruses = new List<Virus>();
            viruses.Add(new Virus(20, 20));

            corruption = new List<Block>();
            corruption.Add(new Block(10, 9));
            corruption.Add(new Block(10, 10));
            corruption.Add(new Block(10, 11));
            corruption.Add(new Block(10, 12));
            corruption.Add(new Block(9, 12));
            corruption.Add(new Block(8, 12));
            corruption.Add(new Block(7, 12));
            corruption.Add(new Block(11, 12));

            importantData = new List<Block>();
            importantData.Add(new Block(1, 1));
            importantData.Add(new Block(10, 1));
            importantData.Add(new Block(20, 1));

            grids.Add(new GridData(20, viruses, new List<Block>(), corruption, importantData, gridConnections, 0, 0));
            level = new LevelData(grids);
            levels.Add(level);
            #endregion

            #region Level5
            grids = new List<GridData>();

            //grid 1
            gridConnections = new List<GridConnection>();

            viruses = new List<Virus>();
            viruses.Add(new Virus(20, 20));
            viruses.Add(new Virus(1, 20));

            corruption = new List<Block>();
            corruption.Add(new Block(10, 6));
            corruption.Add(new Block(11, 6));
            corruption.Add(new Block(13, 6));
            corruption.Add(new Block(7, 6));
            corruption.Add(new Block(7, 5));
            corruption.Add(new Block(7, 2));
            corruption.Add(new Block(7, 1));

            importantData = new List<Block>();
            importantData.Add(new Block(1, 1));
            importantData.Add(new Block(10, 1));
            importantData.Add(new Block(20, 1));
            importantData.Add(new Block(1, 10));
            importantData.Add(new Block(1, 5));
            importantData.Add(new Block(1, 5));

            grids.Add(new GridData(20, viruses, new List<Block>(), corruption, importantData, gridConnections, 0, 0));
            level = new LevelData(grids);
            levels.Add(level);
            #endregion

            #region Level6
            grids = new List<GridData>();

            //grid 1
            gridConnections = new List<GridConnection>();

            gridConnections.Add(new GridConnection(0, 1, new int[] { 20, 15}, new int[] { 0, 9 } ));

            viruses = new List<Virus>();
            viruses.Add(new Virus(1, 20));

            corruption = new List<Block>();
            corruption.Add(new Block(1, 6));
            corruption.Add(new Block(2, 6));
            corruption.Add(new Block(2, 5));
            corruption.Add(new Block(2, 4));
            corruption.Add(new Block(2, 3));

            importantData = new List<Block>();
            importantData.Add(new Block(1, 1));
            importantData.Add(new Block(1, 3));

            grids.Add(new GridData(20, viruses, new List<Block>(), corruption, importantData, gridConnections, 0, 0));

            //grid 2
            gridConnections = new List<GridConnection>();

            viruses = new List<Virus>();

            corruption = new List<Block>();
            corruption.Add(new Block(6, 6));
            corruption.Add(new Block(5, 6));
            corruption.Add(new Block(5, 5));
            corruption.Add(new Block(5, 4));
            corruption.Add(new Block(5, 3));
            corruption.Add(new Block(4, 3));

            importantData = new List<Block>();
            importantData.Add(new Block(1, 1));
            importantData.Add(new Block(3, 1));
            importantData.Add(new Block(5, 1));
            importantData.Add(new Block(7, 3));

            grids.Add(new GridData(10, viruses, new List<Block>(), corruption, importantData, gridConnections, 30, 0));

            level = new LevelData(grids);
            levels.Add(level);
            #endregion

            #region Level7
            grids = new List<GridData>();

            //grid 1
            gridConnections = new List<GridConnection>();

            viruses = new List<Virus>();
            viruses.Add(new Virus(1, 1));
            viruses.Add(new Virus(1, 20));

            corruption = new List<Block>();
            corruption.Add(new Block(8, 3));
            corruption.Add(new Block(7, 3));
            corruption.Add(new Block(6, 3));
            corruption.Add(new Block(6, 4));
            corruption.Add(new Block(6, 5));
            corruption.Add(new Block(6, 6));
            corruption.Add(new Block(6, 7));
            corruption.Add(new Block(5, 7));
            corruption.Add(new Block(6, 20));
            corruption.Add(new Block(6, 19));
            corruption.Add(new Block(6, 18));
            corruption.Add(new Block(6, 17));
            corruption.Add(new Block(6, 16));
            corruption.Add(new Block(7, 16));

            importantData = new List<Block>();
            importantData.Add(new Block(20, 1));
            importantData.Add(new Block(20, 10));
            importantData.Add(new Block(20, 20));

            grids.Add(new GridData(20, viruses, new List<Block>(), corruption, importantData, gridConnections, 0, 0));
            level = new LevelData(grids);
            levels.Add(level);
            #endregion

            #region Level8
            grids = new List<GridData>();

            //grid 1
            gridConnections = new List<GridConnection>();

            viruses = new List<Virus>();
            viruses.Add(new Virus(1, 20));

            corruption = new List<Block>();
            corruption.Add(new Block(4, 5));
            corruption.Add(new Block(6, 5));
            corruption.Add(new Block(5, 5));
            corruption.Add(new Block(6, 6));
            corruption.Add(new Block(7, 6));
            corruption.Add(new Block(8, 6));
            corruption.Add(new Block(9, 6));
            corruption.Add(new Block(9, 7));
            corruption.Add(new Block(9, 8));
            corruption.Add(new Block(9, 9));
            corruption.Add(new Block(20, 12));
            corruption.Add(new Block(19, 12));
            corruption.Add(new Block(18, 12));
            corruption.Add(new Block(17, 12));

            importantData = new List<Block>();
            importantData.Add(new Block(1, 1));
            importantData.Add(new Block(1, 3));
            importantData.Add(new Block(3, 5));
            importantData.Add(new Block(20, 20));

            grids.Add(new GridData(20, viruses, new List<Block>(), corruption, importantData, gridConnections, 0, 0));
            level = new LevelData(grids);
            levels.Add(level);
            #endregion
        }
    }
}
