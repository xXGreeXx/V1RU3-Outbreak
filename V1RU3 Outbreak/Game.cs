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

        public static Boolean fullscreen { get; set; } = true;


        public static EnumHandler.GameState state { get; set; } = EnumHandler.GameState.MainMenu;
        public static Boolean inPause { get; set; } = false;
        public static Boolean winScreen { get; set; } = false;

        public static int levelIndex { get; set; } = 0;
        public static LevelData levelData { get; set; }

        public static Boolean playerTurn = true;
        public static Random r { get; } = new Random();

        public static int turnsUsed { get; set; } = 0;

        //constructor
        public Game()
        {
            //initialize
            InitializeComponent();

            //set game to fullscreen/windows
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
            }

            //load start level
            levelData = levelController.levels[levelIndex];

            //start rendering loop
            timer.Start();
        }

        //render timer update
        private void timer_Tick(object sender, EventArgs e)
        {
            canvas.Refresh();
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
    }
}
