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
        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseDown += pictureBox1_MouseDown;
        }


        public Dictionary<Piece, Bitmap> PieceBitmaps { get; set; }
        private Board Board { get; set; }
        private Piece CurrentPiece { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }



        private void Form1_Load(object sender, EventArgs e)
        {
            InitGame();
            DrawGame();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Board.PickOrDropPiece();
            DrawGame();
        }

    
        private void InitGame()
        {

           TileWidth = 100;
           TileHeight = 100; 

             Board = new Board();

           PieceBitmaps = new Dictionary<Piece, Bitmap>();
           PieceBitmaps.Add(new Piece(Piece.PieceType.Pawn, Piece.PieceColor.Black), new Bitmap(Properties.Resources.pawn_black));
           PieceBitmaps.Add(new Piece(Piece.PieceType.Pawn, Piece.PieceColor.White), new Bitmap(Properties.Resources.pawn_white));


        }
        private void DrawGame()
        {
            ClientSize = new Size(TileWidth * 8, TileHeight * 8);
            var tileSize = new Size(TileWidth, TileHeight);
            Bitmap bitmap = Board.CreateBoard(tileSize);
            DrawPieces(bitmap);
            pictureBox1.Image = bitmap;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(TileWidth * 8, TileHeight * 8);
        }

        private Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            using (Graphics g = Graphics.FromImage(result))
            {

                g.DrawImage(bmp, 0, 0, width, height);

            }

            return result;
        }
        private void DrawPieces(Bitmap bitmap)
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Board board = Board;
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        Piece piece = board.GetPiece(x, y);
                        if (piece != null)
                        {

                            Bitmap bitmap1 = PieceBitmaps[piece];
                            Bitmap bitmap2 = ResizeBitmap(bitmap1, TileWidth, TileHeight);
                            graphics.DrawImage(bitmap2, new Point(x * TileWidth, y * TileHeight));

                        }
                    }
                }
            }
        }





        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
    



}
