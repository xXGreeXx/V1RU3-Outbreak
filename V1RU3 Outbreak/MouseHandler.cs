using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace V1RU3_Outbreak
{
    public class MouseHandler
    {
        //define global variables
        public static int mouseX { get; set; } = 0;
        public static int mouseY { get; set; } = 0;
        public static Boolean mouseDown { get; set; } = false;

        //constructor
        public MouseHandler()
        {

        }

        //register mouse
        public void RegisterMouseEvent(int x, int y, Boolean down, MouseButtons button)
        {
            mouseDown = down;

            Graphics g = Graphics.FromImage(V1RU3_Outbreak.Properties.Resources.title);
            float widthScale = RenderingEngine.scaleX;
            float heightScale = RenderingEngine.scaleY;
            int width = RenderingEngine.canvasWidth;
            int height = RenderingEngine.canvasHeight;
            Font f = new Font(FontFamily.GenericSansSerif, 15 * Math.Min(widthScale, heightScale), FontStyle.Bold);
            Font fSmall = new Font(FontFamily.GenericSansSerif, 12 * Math.Min(widthScale, heightScale), FontStyle.Bold);
            Font fontForText = new Font(FontFamily.GenericSansSerif, 10 * Math.Min(widthScale, heightScale), FontStyle.Bold);
            Font fTiny = new Font(FontFamily.GenericMonospace, 5.25F * Math.Min(widthScale, heightScale), FontStyle.Regular | FontStyle.Bold);
            float heightBaseForText = V1RU3_Outbreak.Properties.Resources.title.Height * heightScale + 30;

            #region MainMenu
            if (Game.state.Equals(EnumHandler.GameState.MainMenu) && !down)
            {
                if (mouseX >= RenderingEngine.canvasWidth / 2 - g.MeasureString("Play", f).Width / 2 && mouseX <= RenderingEngine.canvasWidth / 2 + g.MeasureString("Play", f).Width / 2)
                {
                    if (mouseY >= heightBaseForText + (20 * Math.Min(widthScale, heightScale)) && mouseY <= heightBaseForText + (20 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", f).Height)
                    {
                        Game.state = EnumHandler.GameState.Game;
                        Game.subState = EnumHandler.SubStates.None;

                        if (Game.levelIndex == 0) Game.subState = EnumHandler.SubStates.Tutorial;

                        if (Game.levelIndex >= Game.levelController.levels.Count)
                        {
                            Game.levelIndex = 0;
                            Game.turnsUsed = 0;
                            Game.subState = EnumHandler.SubStates.None;
                            Game.levelController = new LevelController();
                            RenderingEngine.textOnScreen = new List<String>();
                            RenderingEngine.textOnScreenRotation = new List<int>();
                            RenderingEngine.textAddCycle = 0;
                            Game.CPUcycles = Game.maxCPUCycles;
                        }

                        Game.levelData = new LevelController().levels[Game.levelIndex];
                    }
                }

                if (mouseX >= RenderingEngine.canvasWidth / 2 - g.MeasureString("Options", f).Width / 2 && mouseX <= RenderingEngine.canvasWidth / 2 + g.MeasureString("Options", f).Width / 2)
                {
                    if (mouseY >= heightBaseForText + (60 * Math.Min(widthScale, heightScale)) && mouseY <= heightBaseForText + (60 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", f).Height)
                    {
                        Game.state = EnumHandler.GameState.OptionsMenu;
                    }
                }

                if (mouseX >= RenderingEngine.canvasWidth / 2 - g.MeasureString("Quit", f).Width / 2 && mouseX <= RenderingEngine.canvasWidth / 2 + g.MeasureString("Quit", f).Width / 2)
                {
                    if (mouseY >= heightBaseForText + (150 * Math.Min(widthScale, heightScale)) && mouseY <= heightBaseForText + (150 * Math.Min(widthScale, heightScale)) + g.MeasureString("Options", f).Height)
                    {
                        Application.Exit();
                    }
                }
            }
            #endregion

            #region OptionsMenu
            else if (Game.state.Equals(EnumHandler.GameState.OptionsMenu) && !down)
            {
                if (x <= RenderingEngine.canvasWidth / 2 - (200 * RenderingEngine.scaleX) / 2 
                    || x >= RenderingEngine.canvasWidth / 2 + ( 200 * RenderingEngine.scaleX) / 2 
                    || y <= RenderingEngine.canvasHeight / 2 - (200 * RenderingEngine.scaleY) / 2
                    || y >= RenderingEngine.canvasHeight / 2 + (200 * RenderingEngine.scaleY) / 2)
                {
                    Game.state = EnumHandler.GameState.MainMenu;
                }
                
                if (mouseX >= width / 2 - (165 * Math.Min(widthScale, heightScale)) / 2 + g.MeasureString("Fullscreen: ", fontForText).Width && mouseX <= width / 2 - (165 * Math.Min(widthScale, heightScale)) / 2 + g.MeasureString("Fullscreen: ", fontForText).Width + 15 * Math.Min(widthScale, heightScale))
                {
                    if (mouseY >= height / 2 - (183 * Math.Min(widthScale, heightScale)) / 2 && mouseY <= height / 2 - (183 * Math.Min(widthScale, heightScale)) / 2 + 10 * Math.Min(widthScale, heightScale))
                    {
                        Game.fullscreen = !Game.fullscreen;
                    }
                }
            }
            #endregion

            #region Game
            else if (Game.state.Equals(EnumHandler.GameState.Game) && !down)
            {
                //win menu
                if (Game.subState.Equals(EnumHandler.SubStates.Win))
                {
                    if (mouseX >= width / 2 - (40 * Math.Min(widthScale, heightScale)) && mouseX <= width / 2 - (40 * Math.Min(widthScale, heightScale)) + g.MeasureString("Next Level ->", fSmall).Width)
                    {
                        if (mouseY >= height / 2 + (50 * Math.Min(widthScale, heightScale)) && mouseY <= height / 2 + (50 * Math.Min(widthScale, heightScale)) + g.MeasureString("Next Level ->", fSmall).Height)
                        {
                            if (Game.levelIndex >= Game.levelController.levels.Count)
                            {
                                Game.subState = EnumHandler.SubStates.GameWin;
                            }

                            else if (Game.levelIndex < Game.levelController.levels.Count)
                            {
                                Game.levelData = Game.levelController.levels[Game.levelIndex];
                                Game.turnsUsed = 0;
                                Game.subState = EnumHandler.SubStates.None;
                                Game.CPUcycles = Game.maxCPUCycles;
                                RenderingEngine.screenFade = 255;
                                RenderingEngine.textOnScreen = new List<String>();
                                RenderingEngine.textOnScreenRotation = new List<int>();
                                RenderingEngine.textAddCycle = 0;
                                RenderingEngine.menuDropInCycle = 0;
                            }
                        }
                    }

                    if (mouseX >= width / 2 - (85 * Math.Min(widthScale, heightScale)) && mouseX <= width / 2 - (85 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Width)
                    {
                        if (mouseY >= height / 2 + (50 * Math.Min(widthScale, heightScale)) && mouseY <= height / 2 + (50 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Height)
                        {
                            Game.state = EnumHandler.GameState.MainMenu;
                            Game.levelData = Game.levelController.levels[Game.levelIndex];
                            Game.turnsUsed = 0;
                            Game.subState = EnumHandler.SubStates.None;
                            Game.CPUcycles = Game.maxCPUCycles;
                            RenderingEngine.screenFade = 255;
                            RenderingEngine.textOnScreen = new List<String>();
                            RenderingEngine.textOnScreenRotation = new List<int>();
                            RenderingEngine.textAddCycle = 0;
                            RenderingEngine.menuDropInCycle = 0;
                            Game.money -= Game.score;
                        }
                    }

                    if (mouseX >= width / 2 + (20 * Math.Min(widthScale, heightScale)) && mouseX <= width / 2 + (20 * Math.Min(widthScale, heightScale)) + g.MeasureString("Shop", f).Width)
                    {
                        if (mouseY >= height / 2 + (75 * Math.Min(widthScale, heightScale)) && mouseY <= height / 2 + (75 * Math.Min(widthScale, heightScale)) + g.MeasureString("Shop", f).Height)
                        {
                            Game.subState = EnumHandler.SubStates.Shop;
                        }
                    }
                }

                //loss menu
                else if (Game.subState.Equals(EnumHandler.SubStates.Loss))
                {
                    if (mouseX >= width / 2 - (10 * Math.Min(widthScale, heightScale)) && mouseX <= width / 2 - (10 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart", f).Width)
                    {
                        if (mouseY >= height / 2 + (-35 * Math.Min(widthScale, heightScale)) && mouseY <= height / 2 + (-35 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart", f).Height)
                        {
                            Game.RestartGame();
                        }
                    }

                    if (mouseX >= width / 2 - (85 * Math.Min(widthScale, heightScale)) && mouseX <= width / 2 - (85 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Width)
                    {
                        if (mouseY >= height / 2 + (-35 * Math.Min(widthScale, heightScale)) && mouseY <= height / 2 + (-35 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Height)
                        {
                            Game.state = EnumHandler.GameState.MainMenu;
                            Game.subState = EnumHandler.SubStates.None;
                        }
                    }
                }

                //shop menu
                else if (Game.subState.Equals(EnumHandler.SubStates.Shop))
                {
                    if (mouseX >= width / 2 - (100 * Math.Min(widthScale, heightScale)) && mouseX <= width / 2 - (100 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Width)
                    {
                        if (mouseY >= height / 2 + (-100 * Math.Min(widthScale, heightScale)) && mouseY <= height / 2 + (-100 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Height)
                        {
                            Game.subState = EnumHandler.SubStates.Win;
                        }
                    }

                    float yOfItemOffset = 0;
                    foreach (Tuple<EnumHandler.Items, int> item in Game.itemsForPurchase)
                    {
                        if (mouseX >= width / 2 - (50 * Math.Min(widthScale, heightScale)) / 2 && mouseX <= width / 2 - (50 * Math.Min(widthScale, heightScale)) / 2 + 125 * Math.Min(widthScale, heightScale))
                        {
                            if (mouseY >= height / 2 - (198 * Math.Min(widthScale, heightScale)) / 2 + yOfItemOffset && mouseY <= height / 2 - (198 * Math.Min(widthScale, heightScale)) / 2 + yOfItemOffset + 35 * Math.Min(widthScale, heightScale))
                            {
                                if (Game.money >= item.Item2)
                                {
                                    Game.itemsForPurchase.Remove(item);
                                    Game.itemsUnlocked.Add(item.Item1);
                                    Game.money -= item.Item2;

                                    if (item.Item1.Equals(EnumHandler.Items.PCUpgrade1) || item.Item1.Equals(EnumHandler.Items.PCUpgrade2) || item.Item1.Equals(EnumHandler.Items.PCUpgrade3))
                                    {
                                        Game.maxCPUCycles += 75;
                                    }

                                    break;
                                }
                            }
                        }


                        yOfItemOffset += 40 * Math.Min(widthScale, heightScale);
                    }
                }

                //pause menu
                else if (Game.subState.Equals(EnumHandler.SubStates.Pause))
                {
                    float heightBase = (height / 2 + (50 * heightScale) / 2) - (125 * Math.Min(widthScale, heightScale));

                    if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Restart", f).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Restart", f).Width / 2)
                    {
                        if (MouseHandler.mouseY >= heightBase + (10 * Math.Min(widthScale, heightScale)) && MouseHandler.mouseY <= heightBase + (10 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart", f).Height)
                        {
                            Game.RestartGame();
                        }
                    }

                    if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Main Menu", f).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Main Menu", f).Width / 2)
                    {
                        if (MouseHandler.mouseY >= heightBase + 30 * Math.Min(widthScale, heightScale) && MouseHandler.mouseY <= heightBase + 30 * Math.Min(widthScale, heightScale) + g.MeasureString("Main Menu", f).Height)
                        {
                            Game.state = EnumHandler.GameState.MainMenu;
                        }
                    }

                    if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Exit To Desktop", f).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Exit To Desktop", f).Width / 2)
                    {
                        if (MouseHandler.mouseY >= heightBase + 100 * Math.Min(widthScale, heightScale) && MouseHandler.mouseY <= heightBase + 100 * Math.Min(widthScale, heightScale) + g.MeasureString("Exit To Desktop", f).Height)
                        {
                            Application.Exit();
                        }
                    }
                }

                //puzzles
                else if (Game.subState.Equals(EnumHandler.SubStates.Puzzle))
                {
                    float xBase = width / 2 - (100 * Math.Min(widthScale, heightScale));
                    float yBase = height / 2 - (100 * Math.Min(widthScale, heightScale));

                    //pipes
                    if (Game.loadedPuzzle.Equals(EnumHandler.PuzzleTypes.Pipes))
                    {
                        float pipeSize = 10 * Math.Min(widthScale, heightScale);

                        float pipeX = xBase + pipeSize;
                        float pipeY = yBase;
                        foreach (Pipe p in Pipes.pipes)
                        {
                            if (mouseX >= pipeX && mouseX <= pipeX + pipeSize)
                            {
                                if (mouseY >= pipeY && mouseY <= pipeY + pipeSize)
                                {
                                    p.rotation += 90;
                                    if (p.rotation == 360)
                                    {
                                        p.rotation = 0;
                                    }
                                    break;
                                }
                            }

                            pipeX += pipeSize;
                            if (pipeX >= width / 2 - (100 * Math.Min(widthScale, heightScale)) + 200 * Math.Min(widthScale, heightScale) - (pipeSize / 2))
                            {
                                pipeX = xBase;
                                pipeY += pipeSize;
                            }
                        }
                    }

                    //binary
                    if (Game.loadedPuzzle.Equals(EnumHandler.PuzzleTypes.Binary))
                    {
                        float pipeSize = 10 * Math.Min(widthScale, heightScale);

                        float xOfNumber = xBase;
                        float yOfNumber = yBase;
                        int totalRows = 0;
                        for (int i = 0; i < BinaryPuzzle.currentBin.Length; i++)
                        {
                            String c = BinaryPuzzle.currentBin[i].ToString();

                            if (x >= xOfNumber && x <= xOfNumber + g.MeasureString(c, fTiny).Width)
                            {
                                if (y >= yOfNumber && y <= yOfNumber + g.MeasureString(c, fTiny).Height)
                                {
                                    if (BinaryPuzzle.targetBin[totalRows].ToString().Equals(c))
                                    {
                                        for (int row = 0; row < 20; row++)
                                        {
                                            BinaryPuzzle.lockedLocations[totalRows * 20 + row] = 1;
                                        }
                                    }
                                }
                            }

                            xOfNumber += pipeSize;
                            if (xOfNumber >= width / 2 - (100 * Math.Min(widthScale, heightScale)) + 200 * Math.Min(widthScale, heightScale) - (pipeSize / 2))
                            {
                                totalRows++;
                                xOfNumber = xBase;
                                yOfNumber += pipeSize;
                            }
                        }
                    }
                }

                else if (Game.subState.Equals(EnumHandler.SubStates.GameWin))
                {
                    if (mouseX >= width / 2 - (40 * Math.Min(widthScale, heightScale)) && mouseX <= width / 2 - (40 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart Game", f).Width)
                    {
                        if (mouseY >= height / 2 + (70 * Math.Min(widthScale, heightScale)) && mouseY <= height / 2 + (70 * Math.Min(widthScale, heightScale)) + g.MeasureString("Restart Game", f).Height)
                        {
                            Game.FullRestartGame();
                        }
                    }

                    if (mouseX >= width / 2 - (85 * Math.Min(widthScale, heightScale)) && mouseX <= width / 2 - (85 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Width)
                    {
                        if (mouseY >= height / 2 + (70 * Math.Min(widthScale, heightScale)) && mouseY <= height / 2 + (70 * Math.Min(widthScale, heightScale)) + g.MeasureString("Back", fSmall).Height)
                        {
                            Game.state = EnumHandler.GameState.MainMenu;
                        }
                    }
                }

                if (Game.playerTurn && RenderingEngine.screenFade <= 150 && Game.subState.Equals(EnumHandler.SubStates.None))
                {
                    //hud
                    if (mouseX >= 32 * widthScale - 3 && mouseX <= 32 * widthScale - 3 + (30 * widthScale))
                    {
                        if (mouseY >= 5 * heightScale && mouseY <= 5 * heightScale + (15 * heightScale))
                        {
                            if(Game.playerTurn) Game.HandleAITurn();
                        }
                    }

                    if (Game.itemsUnlocked.Contains(EnumHandler.Items.Antivirus))
                    {
                        if (mouseX >= 64 * widthScale - 3 && mouseX <= 64 * widthScale - 3 + (30 * widthScale))
                        {
                            if (mouseY >= 5 * heightScale && mouseY <= 5 * heightScale + (15 * heightScale))
                            {
                                if (Game.playerTurn)
                                {
                                    Game.loadedPuzzle = EnumHandler.PuzzleTypes.Pipes;
                                    Game.subState = EnumHandler.SubStates.Puzzle;
                                    Pipes.GenerateLevel(20, 19);
                                    Game.puzzleStart = DateTime.Now;
                                }
                            }
                        }
                    }

                    if (Game.itemsUnlocked.Contains(EnumHandler.Items.DiskDefragger))
                    {
                        if (mouseX >= 96 * widthScale - 3 && mouseX <= 96 * widthScale - 3 + (30 * widthScale))
                        {
                            if (mouseY >= 5 * heightScale && mouseY <= 5 * heightScale + (15 * heightScale))
                            {
                                if (Game.playerTurn)
                                {
                                    if (Game.CPUcycles >= 200)
                                    {
                                        Game.CPUcycles -= 200;
                                        Game.DefragDisk();
                                    }
                                }
                            }
                        }
                    }

                    if (Game.itemsUnlocked.Contains(EnumHandler.Items.DataEncrypter))
                    {
                        if (mouseX >= 128 * widthScale - 3 && mouseX <= 128 * widthScale - 3 + (30 * widthScale))
                        {
                            if (mouseY >= 5 * heightScale && mouseY <= 5 * heightScale + (15 * heightScale))
                            {
                                if (Game.playerTurn)
                                {
                                    Game.loadedPuzzle = EnumHandler.PuzzleTypes.Binary;
                                    Game.subState = EnumHandler.SubStates.Puzzle;
                                    BinaryPuzzle.GenerateLevel();
                                    Game.puzzleStart = DateTime.Now;
                                }
                            }
                        }
                    }
                    if (Game.itemsUnlocked.Contains(EnumHandler.Items.Firewall))
                    {
                        if (mouseX >= 160 * widthScale - 3 && mouseX <= 160 * widthScale - 3 + (30 * widthScale))
                        {
                            if (mouseY >= 5 * heightScale && mouseY <= 5 * heightScale + (15 * heightScale))
                            {
                                Game.loadedPuzzle = EnumHandler.PuzzleTypes.Matrix;
                                Game.subState = EnumHandler.SubStates.Puzzle;
                                MatrixPuzzle.GeneratePuzzle();
                                Game.puzzleStart = DateTime.Now;
                            }
                        }
                    }
                    if (Game.itemsUnlocked.Contains(EnumHandler.Items.Sandbox))
                    {
                        if (mouseX >= 192 * widthScale - 3 && mouseX <= 192 * widthScale - 3 + (30 * widthScale))
                        {
                            if (mouseY >= 5 * heightScale && mouseY <= 5 * heightScale + (15 * heightScale))
                            {
                                Game.loadedPuzzle = EnumHandler.PuzzleTypes.Encryption;
                                Game.subState = EnumHandler.SubStates.Puzzle;
                                EncryptionPuzzle.GeneratePuzzle();
                                Game.puzzleStart = DateTime.Now;
                            }
                        }
                    }


                    //blocks
                    float tileSize = (15 - Game.cameraZoom) * Math.Min(widthScale, heightScale);

                    foreach (GridData grid in Game.levelData.grids)
                    {
                        float baseX = width / 2 - ((grid.gridSize * tileSize) + Game.cameraX) / 2 + (grid.x * tileSize);
                        float baseY = height / 2 - ((grid.gridSize * tileSize) + Game.cameraY) / 2 + (grid.y * tileSize);

                        float newX = x - baseX;
                        float newY = y - baseY;

                        newX /= tileSize;
                        newX = (float)Math.Ceiling(newX);

                        newY /= tileSize;
                        newY = (float)Math.Ceiling(newY);

                        if (newX > 0 && newX < grid.gridSize + 1 && !Game.blockPlaced)
                        {
                            if (newY > 0 && newY < grid.gridSize + 1)
                            {
                                Boolean pass = true;

                                foreach (Virus s in grid.viruses)
                                {
                                    if (s.x == newX && s.y == newY)
                                    {
                                        pass = false;
                                        break;
                                    }
                                }

                                foreach (Block b in grid.blocks.Concat(grid.corruption).Concat(grid.importantData))
                                {
                                    if (b.x == newX && b.y == newY)
                                    {
                                        pass = false;
                                        break;
                                    }
                                }

                                if (pass)
                                {
                                    //place block
                                    grid.blocks.Add(new Block((int)newX, (int)newY));
                                    Game.blockPlaced = true;
                                }
                            }
                        }
                    }
                }

                //tutorial menu
                if (Game.subState.Equals(EnumHandler.SubStates.Tutorial))
                {
                    if (RenderingEngine.tutorialState < 6)
                    {
                        RenderingEngine.tutorialState++;
                    }
                    else
                    {
                        RenderingEngine.tutorialState = 0;
                        Game.subState = EnumHandler.SubStates.None;
                    }
                }
            }
            #endregion
        }

        //register mouse move
        public void RegisterMouseMove(int x, int y)
        {
            mouseX = x;
            mouseY = y;
        }
    }
}
