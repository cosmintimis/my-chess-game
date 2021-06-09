using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chess_v2
{
    public class Game
    {
        public Game()
        {

        }
        public enum GameStatus
        {
            ACTIVE,
            BLACK_WIN,
            WHITE_WIN,
            FORFEIT,
            STALEMATE,
            RESIGNATION
        }
        public enum Player
        {
            Player1,
            Player2
        }
        //  private Board board;
        //  private Player currentTurn;
        private GameStatus status;
        // private List<Move> movesPlayed;
        //  private  PictureBox pictureBox1;


        public Dictionary<Piece, Bitmap> PieceBitmaps;
        public Board board;

        public int TileWidth = 100;
        public int TileHeight = 100;

        public void setStatus(GameStatus status)
        {
            this.status = status;
        }

        public void InitGame()
        {

            board = new Board();

            PieceBitmaps = new Dictionary<Piece, Bitmap>();
            PieceBitmaps.Add(new Piece(Piece.PieceType.King, Piece.PieceColor.Black), new Bitmap(Properties.Resources.king_black));
            PieceBitmaps.Add(new Piece(Piece.PieceType.King, Piece.PieceColor.White), new Bitmap(Properties.Resources.king_white));
            PieceBitmaps.Add(new Piece(Piece.PieceType.Queen, Piece.PieceColor.Black), new Bitmap(Properties.Resources.queen_black));
            PieceBitmaps.Add(new Piece(Piece.PieceType.Queen, Piece.PieceColor.White), new Bitmap(Properties.Resources.queen_white));
            PieceBitmaps.Add(new Piece(Piece.PieceType.Rook, Piece.PieceColor.Black), new Bitmap(Properties.Resources.rook_black));
            PieceBitmaps.Add(new Piece(Piece.PieceType.Rook, Piece.PieceColor.White), new Bitmap(Properties.Resources.rook_white));
            PieceBitmaps.Add(new Piece(Piece.PieceType.Bishop, Piece.PieceColor.Black), new Bitmap(Properties.Resources.bishop_black));
            PieceBitmaps.Add(new Piece(Piece.PieceType.Bishop, Piece.PieceColor.White), new Bitmap(Properties.Resources.bishop_white));
            PieceBitmaps.Add(new Piece(Piece.PieceType.Knight, Piece.PieceColor.Black), new Bitmap(Properties.Resources.knight_black));
            PieceBitmaps.Add(new Piece(Piece.PieceType.Knight, Piece.PieceColor.White), new Bitmap(Properties.Resources.knight_white));
            PieceBitmaps.Add(new Piece(Piece.PieceType.Pawn, Piece.PieceColor.Black), new Bitmap(Properties.Resources.pawn_black));
            PieceBitmaps.Add(new Piece(Piece.PieceType.Pawn, Piece.PieceColor.White), new Bitmap(Properties.Resources.pawn_white));


        }
        public Bitmap DrawGame()
        {
            var tileSize = new Size(TileWidth, TileHeight);
            Bitmap bitmap = board.CreateBoard(tileSize);
            DrawPieces(bitmap);

            return bitmap;
        
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
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        Piece piece = board.GetPiece(x, y);
                        if (piece != null)
                        {

                         ///   Bitmap bitmap1 = PieceBitmaps[piece];
                       ///     Bitmap bitmap2 = ResizeBitmap(bitmap1, TileWidth, TileHeight);
                        ///    graphics.DrawImage(bitmap2, new Point(x * TileWidth, y * TileHeight));

                        }
                    }
                }
            }
        }
    }
}
