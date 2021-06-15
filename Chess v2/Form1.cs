using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess_v2
{
    public partial class Form1 : Form
    {

        public Game game = new Game();
        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseDown += PictureBox1_MouseDown;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(game.TileWidth * 8, game.TileHeight * 8);
            game.InitGame();
            pictureBox1.Image = game.DrawGame();
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(game.TileWidth * 8, game.TileHeight * 8);

        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            game.board.PickOrDropPiece(e);
            pictureBox1.Image = game.DrawGame();
            if (game.board.winner == 1)
            {
                pictureBox1.Enabled = false;
                MessageBox.Show("Alb a castigat!");
               
            }
            else if(game.board.winner == 2)
            {
                pictureBox1.Enabled = false;
                MessageBox.Show("Negru a castigat!");
                
            }
            else if(game.board.winner == 0)
            {
                pictureBox1.Enabled = false;
                MessageBox.Show("Egalitate!");
            }

                
        }
    }

}
