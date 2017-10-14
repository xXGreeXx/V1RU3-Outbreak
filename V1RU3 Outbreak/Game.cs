using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace V1RU3_Outbreak
{
    public partial class Game : Form
    {
        //define global variables
        public static RenderingEngine renderer { get; } = new RenderingEngine();
        public static MouseHandler mouseHandler { get; } = new MouseHandler();
        public static LevelController levelController { get; set; } = new LevelController();
        public static KeyboardHandler keyHandler { get; } = new KeyboardHandler();
        public static ParticleEngine particleEngine { get; } = new ParticleEngine();

        public static String optionsSavePath { get; set; } = "Options.txt";
        public static String gameSavePath { get; set; } = "GameSave.txt";
        public static Boolean fullscreen { get; set; } = true;


        public static EnumHandler.GameState state { get; set; } = EnumHandler.GameState.MainMenu;
        public static EnumHandler.SubStates subState { get; set; } = EnumHandler.SubStates.None;
        public static EnumHandler.PuzzleTypes loadedPuzzle { get; set; } = EnumHandler.PuzzleTypes.None;

        public static int levelIndex { get; set; } = 0;
        public static LevelData levelData { get; set; }
        public static Boolean cameFromLevelSelect { get; set; } = false;

        public static Boolean blockPlaced = false;
        public static Boolean playerTurn = true;
        public static int score { get; set; } = 0;
        public static int money { get; set; } = 0;
        public static List<EnumHandler.Items> itemsUnlocked = new List<EnumHandler.Items>();
        public static List<Tuple<EnumHandler.Items, int>> itemsForPurchase = new List<Tuple<EnumHandler.Items, int>>();
        public static int CPUcycles = 300;
        public static int maxCPUCycles = 300;
        public static DateTime puzzleStart { get; set; }
        public static int timeAllowedOnPuzzle { get; set; } = 60;
        public static float cameraZoom { get; set; } = 0.0F;
        public static int cameraX { get; set; } = 0;
        public static int cameraY { get; set; } = 0;
        public static int cameraXVel { get; set; } = 0;
        public static int cameraYVel { get; set; } = 0;
        public static int cameraMoveSpeed { get; set; } = 15;

        public static Random r { get; } = new Random();

        public static int turnsUsed { get; set; } = 0;

        //constructor
        public Game()
        {
            //initialize
            InitializeComponent();

            //load options save
            if (File.Exists(optionsSavePath))
            {
                StreamReader reader = new StreamReader(File.OpenRead(optionsSavePath));

                fullscreen = Boolean.Parse(reader.ReadLine());



                reader.Close();
            }

            //load game save
            if (File.Exists(gameSavePath))
            {
                StreamReader streamReader = new StreamReader(File.OpenRead(gameSavePath));

                streamReader.ReadLine();
                money = int.Parse(streamReader.ReadLine());
                levelIndex = int.Parse(streamReader.ReadLine());
                streamReader.ReadLine();

                String line = streamReader.ReadLine();
                while ((line = streamReader.ReadLine()) != "</items unlocked>")
                {
                    itemsUnlocked.Add((EnumHandler.Items)Enum.Parse(typeof(EnumHandler.Items), line));
                }

                line = streamReader.ReadLine();
                while ((line = streamReader.ReadLine()) != "</items for purchase>")
                {
                    EnumHandler.Items item = (EnumHandler.Items)Enum.Parse(typeof(EnumHandler.Items), line);
                    int cost = int.Parse(streamReader.ReadLine());

                    itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(item, cost));
                }
                streamReader.Close();
            }
            else
            {
                itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.Antivirus, 225));
                itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.DiskDefragger, 300));
                itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.Firewall, 250));
                itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.DataEncrypter, 200));
                itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.Sandbox, 500));
                itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.AntiMalware, 700));
                itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.PCUpgrade1, 100));
                itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.PCUpgrade2, 150));
                itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.PCUpgrade3, 200));
            }

            //set game to fullscreen/windowed
            UpdateOptions();

            //load start level
            if (levelIndex < levelController.levels.Count)
            {
                levelData = levelController.levels[levelIndex];
            }

            //start rendering loop
            timer.Start();

            //set application handlers
            Application.ApplicationExit += GameSaveBeforeExit;
            this.MouseWheel += Game_MouseWheel;
        }

        //mouse wheel handler
        private void Game_MouseWheel(object sender, MouseEventArgs e)
        {
            if (subState.Equals(EnumHandler.SubStates.None))
            {
                int direction = 0;

                if (e.Delta > 0)
                {
                    direction = -3;
                }
                else if(e.Delta < 0)
                {
                    direction = 3;
                }

                if (direction == 3 && cameraZoom < 9)
                {
                    cameraZoom += direction;
                }
                if (direction == -3 && cameraZoom > -12)
                {
                    cameraZoom += direction;
                }
            }
        }

        //save game before exit
        private void GameSaveBeforeExit(object sender, EventArgs e)
        {
            //options save
            StreamWriter writer = new StreamWriter(File.OpenWrite(optionsSavePath));

            writer.WriteLine(fullscreen.ToString());


            writer.Close();

            //game save
            StreamWriter gameWriter = new StreamWriter(File.OpenWrite(gameSavePath));

            gameWriter.WriteLine("<data>");
            gameWriter.WriteLine(money);
            if (cameFromLevelSelect) gameWriter.WriteLine(levelController.levels.Count);
            else gameWriter.WriteLine(levelIndex);
            gameWriter.WriteLine("</data>");

            gameWriter.WriteLine("<items unlocked>");
            foreach (EnumHandler.Items item in itemsUnlocked) gameWriter.WriteLine(item.ToString());
            gameWriter.WriteLine("</items unlocked>");

            gameWriter.WriteLine("<items for purchase>");
            foreach (Tuple<EnumHandler.Items, int> item in itemsForPurchase)
            {
                gameWriter.WriteLine(item.Item1.ToString());
                gameWriter.WriteLine(item.Item2.ToString());
            }
            gameWriter.WriteLine("</items for purchase>");

            gameWriter.Close();
        }

        //render timer update
        private void timer_Tick(object sender, EventArgs e)
        {
            canvas.Refresh();

            UpdateOptions();
        }

        //rendering engine
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int width = canvas.Width;
            int height = canvas.Height;
            float widthScale = width / 500F;
            float heightScale = height / 350F;

            if (state.Equals(EnumHandler.GameState.MainMenu))
            {
                renderer.DrawMainMenu(g, width, height, widthScale, heightScale);
            }
            else if (state.Equals(EnumHandler.GameState.OptionsMenu))
            {
                renderer.DrawOptionsMenu(g, width, height, widthScale, heightScale);
            }
            else if (state.Equals(EnumHandler.GameState.LevelSelect))
            {
                renderer.DrawLevelSelect(g, width, height, widthScale, heightScale);
            }
            else if (state.Equals(EnumHandler.GameState.Game))
            {
                renderer.DrawGame(g, width, height, widthScale, heightScale, levelData);
            }

            particleEngine.DrawParticles(g, width, height, widthScale, heightScale);
            particleEngine.SimulateParticles();
        }

        //register mouse move
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            mouseHandler.RegisterMouseMove(e.X, e.Y, e.Button);
        }

        //register mouse down
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            mouseHandler.RegisterMouseEvent(e.X, e.Y, true, e.Button);
        }

        //register mouse up
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            mouseHandler.RegisterMouseEvent(e.X, e.Y, false, e.Button);
        }

        //key down
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            keyHandler.RegisterKeyEvent(e.KeyData, true);
        }

        //key up
        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            keyHandler.RegisterKeyEvent(e.KeyData, false);
        }

        //update options
        private void UpdateOptions()
        {
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }

        //handle AI turn
        public static void HandleAITurn()
        {
            Game.playerTurn = false;

            //handle ai
            AI ai = new AI();
            foreach (GridData grid in levelData.grids)
            {
                foreach (Virus v in ai.SimulateAI(grid))
                {
                    grid.viruses.Add(v);

                    foreach (Block b in grid.importantData)
                    {
                        if (b.x == v.targetX && b.y == v.targetY)
                        {
                            grid.importantData.Remove(b);
                            break;
                        }
                    }
                }
            }

            //check if you lost/won the game
            //lose if your hard drive is 70 or more percent corrupted or if all important data is lost
            if (Game.levelData.CountData()== 0 && new LevelController().levels[Game.levelIndex].CountData() > 0)
            {
                Game.subState = EnumHandler.SubStates.Loss;
            }

            int virusCount = 0;

            foreach (GridData grid in levelData.grids)
            {
                virusCount += ai.SimulateAI(grid).Count;
            }
           
            if (virusCount == 0)
            {
                Game.levelIndex++;
                Game.subState = EnumHandler.SubStates.Win;
                Game.score = calculateScore();
                Game.money += Game.score;
            }

            Game.turnsUsed++;
            Game.blockPlaced = false;

            float amountOfTiles = (float)Math.Pow(Game.levelData.CountGridTiles(), 2);
            float amountOfViruses = Game.levelData.CountViruses();

            if ((amountOfViruses / amountOfTiles) * 100 >= 70)
            {
                Game.subState = EnumHandler.SubStates.Loss;
            }

        }

        //move viruses
        public static Boolean MoveVirus(Virus v)
        {
            float speed = 0.25F;

            if (v.targetX == -1 || v.targetY == -1) return false;
            if (v.targetX == v.x && v.targetY == v.y) return false;

            if (v.x < v.targetX) v.x += speed;
            if (v.x > v.targetX) v.x -= speed;

            if (v.y < v.targetY) v.y += speed;
            if (v.y > v.targetY) v.y -= speed;

            return true;
        }

        //calculate score
        private static int calculateScore()
        {
            return (150 * (Game.levelIndex + 1)) - (turnsUsed + (levelData.CountData()+ 1 * new LevelController().levels[levelIndex - 1].CountData() + 1) + Game.levelData.CountViruses());
        }

        //restart game
        public static void RestartGame()
        {
            levelData = new LevelController().levels[levelIndex];
            turnsUsed = 0;
            subState = EnumHandler.SubStates.None;
            CPUcycles = maxCPUCycles;
            RenderingEngine.screenFade = 255;
            RenderingEngine.textOnScreen = new List<String>();
            RenderingEngine.textAddCycle = 0;
        }

        //full game restart
        public static void FullRestartGame()
        {
            state = EnumHandler.GameState.MainMenu;
            RenderingEngine.textAddCycle = 0;
            RenderingEngine.textOnScreen = new List<String>();
            RenderingEngine.menuDropInCycle = 0;
            levelIndex = 0;
            money = 0;
            maxCPUCycles = 300;
            itemsUnlocked = new List<EnumHandler.Items>();

            itemsForPurchase = new List<Tuple<EnumHandler.Items, int>>();
            itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.Antivirus, 225));
            itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.DiskDefragger, 300));
            itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.Firewall, 250));
            itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.DataEncrypter, 200));
            itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.Sandbox, 500));
            itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.AntiMalware, 700));
            itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.PCUpgrade1, 100));
            itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.PCUpgrade2, 150));
            itemsForPurchase.Add(new Tuple<EnumHandler.Items, int>(EnumHandler.Items.PCUpgrade3, 200));
        }

        //defrag disk
        public static void DefragDisk()
        {
            int x = 1;
            int y = 1;

            foreach (GridData grid in levelData.grids)
            {
                foreach (Virus v in grid.viruses)
                {
                    v.x = x;
                    v.y = y;
                    v.targetX = v.x;
                    v.targetY = v.y;

                    x++;
                    if (x > 20)
                    {
                        x = 1;
                        y++;
                    }
                }
                foreach (Block b in grid.corruption)
                {
                    b.x = x;
                    b.y = y;

                    x++;
                    if (x > 20)
                    {
                        x = 1;
                        y++;
                    }
                }
                foreach (Block b in grid.importantData)
                {
                    b.x = x;
                    b.y = y;

                    x++;
                    if (x > 20)
                    {
                        x = 1;
                        y++;
                    }
                }
                foreach (Block b in grid.blocks)
                {
                    b.x = x;
                    b.y = y;

                    x++;
                    if (x > 20)
                    {
                        x = 1;
                        y++;
                    }
                }
            }
        }

        //isolate viruses
        public static void IsolateViruses(int percent)
        {

        }

        //encrypt data
        public static void EncryptData(int percent)
        {

        }
    }
}
