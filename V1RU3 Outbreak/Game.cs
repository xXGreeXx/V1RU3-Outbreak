using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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

        public static Boolean fullscreen { get; set; } = true;


        public static EnumHandler.GameState state { get; set; } = EnumHandler.GameState.MainMenu;
        public static EnumHandler.SubStates subState { get; set; } = EnumHandler.SubStates.None;
        public static EnumHandler.PuzzleTypes loadedPuzzle { get; set; } = EnumHandler.PuzzleTypes.None;

        public static int levelIndex { get; set; } = 0;
        public static LevelData levelData { get; set; }

        public static Boolean blockPlaced = false;
        public static Boolean playerTurn = true;
        public static int score { get; set; } = 0;
        public static int money { get; set; } = 0;
        public static int CPUcycles = 300;
        public static int maxCPUCycles = 300;
        public static DateTime puzzleStart { get; set; }
        public static int timeAllowedOnPuzzle { get; set; } = 60;

        public static Random r { get; } = new Random();

        public static int turnsUsed { get; set; } = 0;

        //constructor
        public Game()
        {
            //initialize
            InitializeComponent();

            //set game to fullscreen/windows
            UpdateOptions();

            //load start level
            levelData = levelController.levels[levelIndex];

            //start rendering loop
            timer.Start();
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
            else if (state.Equals(EnumHandler.GameState.Game))
            {
                renderer.DrawGame(g, width, height, widthScale, heightScale, levelData);
                particleEngine.DrawParticles(g, width, height, widthScale, heightScale);
                particleEngine.SimulateParticles();
            }
        }

        //register mouse move
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            mouseHandler.RegisterMouseMove(e.X, e.Y);
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
            foreach (Virus v in ai.SimulateAI(Game.levelData))
            {
                Game.levelData.viruses.Add(v);

                foreach (Block b in Game.levelData.importantData)
                {
                    if (b.x == v.targetX && b.y == v.targetY)
                    {
                        Game.levelData.importantData.Remove(b);
                        break;
                    }
                }
            }

            //check if you lost/won the game
            //lose if your hard drive is 70 or more percent corrupted or if all important data is lost
            if (Game.levelData.importantData.Count == 0 && new LevelController().levels[Game.levelIndex].importantData.Count > 0)
            {
                Game.subState = EnumHandler.SubStates.Loss;
            }

            List<Virus> dataReturned = ai.SimulateAI(Game.levelData);
            if (dataReturned.Count == 0)
            {
                Game.subState = EnumHandler.SubStates.Win;
                Game.score = calculateScore();
            }

            Game.turnsUsed++;
            Game.blockPlaced = false;

            float amountOfTiles = (float)Math.Pow(Game.levelData.gridSize, 2);
            float amountOfViruses = Game.levelData.viruses.Count;

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
            int score = 0;



            return score;
        }

        //restart game
        public static void RestartGame()
        {
            levelData = new LevelController().levels[levelIndex];
            turnsUsed = 0;
            subState = EnumHandler.SubStates.None;
            RenderingEngine.screenFade = 255;
            RenderingEngine.textOnScreen = new List<String>();
            RenderingEngine.textAddCycle = 0;
        }

        //isolate viruses
        public static void IsolateViruses(int percent)
        {

        }
    }
}
