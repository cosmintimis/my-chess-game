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

            game.InitGame();
            ClientSize = new Size(game.board.TileWidth * 8, game.board.TileHeight * 8);
            pictureBox1.Image = game.DrawGame();
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(game.board.TileWidth * 8, game.board.TileHeight * 8);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            game.board.PickOrDropPiece(e, game.board.TileWidth, game.board.TileHeight);
            pictureBox1.Image = game.DrawGame();
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Brush brush = new SolidBrush(Color.FromArgb(90, Color.Red));
            int x = e.Location.X;
            int y = e.Location.Y;
            while (x % game.board.TileWidth != 0)
                x--;
            while (y % game.board.TileHeight != 0)
                y--;
            g.FillRectangle(brush, x, y, game.board.TileWidth, game.board.TileHeight);
            pictureBox1.Refresh();
 
            if (game.board.CheckMate())
            {
                    if (game.board.currentTurn == Board.Player.black)
                    {
                            pictureBox1.Enabled = false;
                            MessageBox.Show("Alb a castigat!");
                    }
                    else
                {
                            pictureBox1.Enabled = false;
                            MessageBox.Show("Negru a castigat!");
                }
            }
            else if (game.board.Stalemate())
            {
                 pictureBox1.Enabled = false;
                 MessageBox.Show("Egalitate!");
            }

        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            game.board.TileWidth = ClientSize.Width / 8;
            game.board.TileHeight = ClientSize.Height / 8;

            
        }
    }

}
