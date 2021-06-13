using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Chess_v2
{
    public partial class Form1 : Form
    {

        public Game game = new Game();
        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseDown += pictureBox1_MouseDown;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(game.TileWidth * 8, game.TileHeight * 8);
            game.InitGame();
            pictureBox1.Image = game.DrawGame();
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(game.TileWidth * 8, game.TileHeight * 8);

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            game.board.PickOrDropPiece(e);
            pictureBox1.Image = game.DrawGame();
        }
    }

}
