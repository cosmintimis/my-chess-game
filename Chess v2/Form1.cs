using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Chess_v2
{
    public partial class Form1 : Form
    {

        public Button Queen, Knight, Rook, Bishop;

        public Game game = new Game();

        static Form f0;
        public Form1()
        {
            InitializeComponent();
            f0 = this;
            InitButtons();
            pictureBox1.MouseDown += PictureBox1_MouseDown;

           
        }

        private void InitButtons()
        {
            Queen = new Button();
            Knight = new Button();
            Rook = new Button();
            Bishop = new Button();
            Queen.Tag = 1;
            Knight.Tag = 2;
            Rook.Tag = 3;
            Bishop.Tag = 4;
            Queen.Click += Button_Click;
            Knight.Click += Button_Click;
            Rook.Click += Button_Click;
            Bishop.Click += Button_Click;
            tableLayoutPanel1.Controls.Add(Queen);
            tableLayoutPanel1.Controls.Add(Knight);
            tableLayoutPanel1.Controls.Add(Rook);
            tableLayoutPanel1.Controls.Add(Bishop);
        }

        private void PromotePawnMenu(int x, int y, Piece.PieceColor PawnColor)
        {
            tableLayoutPanel1.Location = new Point(x, y);
            tableLayoutPanel1.Size = new Size(game.board.TileWidth, game.board.TileHeight * 4);

            foreach (var button in tableLayoutPanel1.Controls.OfType<Button>())
            {
                button.FlatStyle = FlatStyle.Flat;
                button.Dock = DockStyle.Fill;
                button.FlatAppearance.BorderSize = 0;
                SetImageToButton(button, PawnColor);
                button.BackgroundImageLayout = ImageLayout.Stretch;
            }
            tableLayoutPanel1.Visible = true;
            pictureBox1.Enabled = false;

        }

        private void Button_Click(object sender, EventArgs e)
        {
           switch ((sender as Button).Tag)
           {
                case 1:
                    if(game.WhitePawnPromotion != -1)
                    {
                        game.board._pieces[game.WhitePawnPromotion]._type = Piece.PieceType.Queen;
                        game.PieceBitmaps[game.board._pieces[game.WhitePawnPromotion]] = new Bitmap(Properties.Resources.queen_white);
                    }
                    else
                    {
                        game.board._pieces[game.BlackPawnPromotion + 56]._type = Piece.PieceType.Queen;
                        game.PieceBitmaps[game.board._pieces[game.BlackPawnPromotion + 56]] = new Bitmap(Properties.Resources.queen_black);
                    }
                    break;
                case 2:
                    if (game.WhitePawnPromotion != -1)
                    {
                        game.board._pieces[game.WhitePawnPromotion]._type = Piece.PieceType.Knight;
                        game.PieceBitmaps[game.board._pieces[game.WhitePawnPromotion]] = new Bitmap(Properties.Resources.knight_white);
                    }
                    else
                    {
                        game.board._pieces[game.BlackPawnPromotion + 56]._type = Piece.PieceType.Knight;
                        game.PieceBitmaps[game.board._pieces[game.BlackPawnPromotion + 56]] = new Bitmap(Properties.Resources.knight_black);
                    }
                    break;
                case 3:
                    if (game.WhitePawnPromotion != -1)
                    {
                        game.board._pieces[game.WhitePawnPromotion]._type = Piece.PieceType.Rook;
                        game.PieceBitmaps[game.board._pieces[game.WhitePawnPromotion]] = new Bitmap(Properties.Resources.rook_white);
                    }
                    else
                    {
                        game.board._pieces[game.BlackPawnPromotion + 56]._type = Piece.PieceType.Rook;
                        game.PieceBitmaps[game.board._pieces[game.BlackPawnPromotion + 56]] = new Bitmap(Properties.Resources.rook_black);
                    }
                    break;
                case 4:
                    if (game.WhitePawnPromotion != -1)
                    {
                        game.board._pieces[game.WhitePawnPromotion]._type = Piece.PieceType.Bishop;
                        game.PieceBitmaps[game.board._pieces[game.WhitePawnPromotion]] = new Bitmap(Properties.Resources.bishop_white);
                    }
                    else
                    {
                        game.board._pieces[game.BlackPawnPromotion + 56]._type = Piece.PieceType.Bishop;
                        game.PieceBitmaps[game.board._pieces[game.BlackPawnPromotion + 56]] = new Bitmap(Properties.Resources.bishop_black);
                    }
                    break;

                default:
                    break;
           }
            tableLayoutPanel1.Visible = false;
            pictureBox1.Image = game.DrawGame();
            pictureBox1.Enabled = true;
            game.WhitePawnPromotion = -1;
            game.BlackPawnPromotion = -1;


        }

        private void SetImageToButton(Button button, Piece.PieceColor color)
        {

            if (color == Piece.PieceColor.White)
            {
                switch (button.Tag)
                {
                    case 1:
                        button.BackgroundImage = new Bitmap(Properties.Resources.queen_white);
                        break;
                    case 2:
                        button.BackgroundImage = new Bitmap(Properties.Resources.knight_white);
                        break;
                    case 3:
                        button.BackgroundImage = new Bitmap(Properties.Resources.rook_white);
                        break;
                    case 4:
                        button.BackgroundImage = new Bitmap(Properties.Resources.bishop_white);
                        break;

                    default:
                        break;

                }
            }
            else
            {
                switch (button.Tag)
                {
                    case 1:
                        button.BackgroundImage = new Bitmap(Properties.Resources.queen_black);
                        break;
                    case 2:
                        button.BackgroundImage = new Bitmap(Properties.Resources.knight_black);
                        break;
                    case 3:
                        button.BackgroundImage = new Bitmap(Properties.Resources.rook_black);
                        break;
                    case 4:
                        button.BackgroundImage = new Bitmap(Properties.Resources.bishop_black);
                        break;

                    default:
                        break;
                }
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Visible = false;
            game.InitGame();
            ClientSize = new Size(game.board.TileWidth * 8, game.board.TileHeight * 8);
            pictureBox1.Image = game.DrawGame();
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(game.board.TileWidth * 8, game.board.TileHeight * 8);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            tableLayoutPanel1.Visible = false;
 
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            game.InitGame();
            ClientSize = new Size(game.board.TileWidth * 8, game.board.TileHeight * 8);
            pictureBox1.Image = game.DrawGame();
            button1.Visible = false;
            pictureBox1.Enabled = true;

        }

        private void SquareDrawingOnClick(MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Brush brush = new SolidBrush(Color.FromArgb(50, Color.Red));
            int x = e.Location.X;
            int y = e.Location.Y;
            while (x % game.board.TileWidth != 0)
                x--;
            while (y % game.board.TileHeight != 0)
                y--;
            g.FillRectangle(brush, x, y, game.board.TileWidth, game.board.TileHeight);

        }

        private void GameOver()
        {
            if (game.board.CheckMate())
            {
                if (game.board.currentTurn == Board.Player.black)
                {
                    MessageBox.Show("Alb a castigat!");
                }
                else
                {
                   
                    MessageBox.Show("Negru a castigat!");
                }
                pictureBox1.Enabled = false;
                button1.Visible = true;
            }
            else if (game.board.Stalemate())
            {
                pictureBox1.Enabled = false;
                button1.Visible = true;
                MessageBox.Show("Egalitate!");
            }
        }

        private void DrawingAllPossibleMoves()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    
                    if (game.board.CanDoCastle(game.board.oldX, game.board.oldY, x, y) || game.board.CanDoEnPassant(game.board.oldX, game.board.oldY, x, y) || (game.board.PieceCanMove(game.board.oldX, game.board.oldY, x, y) && game.board.LegalMove(game.board.oldX, game.board.oldY, x, y)))
                    {
                        Graphics g = Graphics.FromImage(pictureBox1.Image);
                        Brush brush = new SolidBrush(Color.FromArgb(80, Color.Yellow));
                        g.FillRectangle(brush, x * game.board.TileWidth, y * game.board.TileHeight, game.board.TileWidth, game.board.TileHeight);

                    }
                }
            }
        }


        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            game.board.PickOrDropPiece(e, game.board.TileWidth, game.board.TileHeight);

            if (game.board.CurrentPiece != null)
            {
                pictureBox1.Image = game.DrawGame();
                DrawingAllPossibleMoves();
            }

            if (game.board.PieceWasMoved == true)
            {
                GameOver();
                pictureBox1.Image = game.DrawGame();
                game.board.PieceWasMoved = false;
            }

            SquareDrawingOnClick(e);

            if (game.WhitePawnPromotion != -1)
            {
                PromotePawnMenu(game.WhitePawnPromotion * game.board.TileWidth, 0, Piece.PieceColor.White);
            }
            if (game.BlackPawnPromotion != -1)
            {
                PromotePawnMenu(game.BlackPawnPromotion * game.board.TileWidth, game.board.TileHeight * 4, Piece.PieceColor.Black);
            }

            f0.Text = "Chess          Turn: " + game.board.currentTurn;    
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            game.board.TileWidth = ClientSize.Width / 8;
            game.board.TileHeight = ClientSize.Height / 8;
            if (game.WhitePawnPromotion != -1)
            {
                tableLayoutPanel1.Size = new Size(game.board.TileWidth, game.board.TileHeight * 4);
                tableLayoutPanel1.Location = new Point(game.WhitePawnPromotion * game.board.TileWidth, 0);
            }
            if (game.BlackPawnPromotion != -1)
            {
                tableLayoutPanel1.Size = new Size(game.board.TileWidth, game.board.TileHeight * 4);
                tableLayoutPanel1.Location = new Point(game.BlackPawnPromotion * game.board.TileWidth, game.board.TileHeight * 4);
            }

        }
    }

}
