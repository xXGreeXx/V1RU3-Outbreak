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

        public static Boolean fullscreen { get; set; } = true;
        public static EnumHandler.GameState state { get; set; } = EnumHandler.GameState.MainMenu;

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
            int widthScale = 1;
            int heightScale = 1;

            if (state.Equals(EnumHandler.GameState.MainMenu))
            {
                renderer.DrawMainMenu(g, width, height, widthScale, heightScale);
            }
        }
    }
}
