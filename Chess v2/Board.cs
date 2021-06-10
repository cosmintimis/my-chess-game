using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chess_v2
{
    public class Board
    {
        public readonly Piece[] _pieces;
        private Piece CurrentPiece;

        public Bitmap CreateBoard(Size tileSize)
        {
            int tileWidth = tileSize.Width;
            int tileHeight = tileSize.Height;
            var bitmap = new Bitmap(tileWidth * 8, tileHeight * 8);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        Brush brush = (x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0) ? Brushes.Beige : Brushes.BurlyWood;
                        graphics.FillRectangle(brush, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }
                }
            }
            return bitmap;
        }
        public  Board()
        {
            _pieces = new Piece[8 * 8];
            InitPieces();
            PopulatePieces();
        }

        public Piece GetPiece(int x, int y)
        {
            int i = y * 8 + x;
            return _pieces[i];
        }

        public void SetPiece(int x, int y, Piece piece)
        {
            int i = y * 8 + x;
            _pieces[i] = piece;
        }

        private void InitPieces()
        {
            /// Init Black Pieces
            
            _pieces[0] = new Piece(Piece.PieceType.Rook, Piece.PieceColor.Black);
            _pieces[1] = new Piece(Piece.PieceType.Knight, Piece.PieceColor.Black);
            _pieces[2] = new Piece(Piece.PieceType.Bishop, Piece.PieceColor.Black);
            _pieces[3] = new Piece(Piece.PieceType.Queen, Piece.PieceColor.Black);
            _pieces[4] = new Piece(Piece.PieceType.King, Piece.PieceColor.Black);
            _pieces[5] = new Piece(Piece.PieceType.Bishop, Piece.PieceColor.Black);
            _pieces[6] = new Piece(Piece.PieceType.Knight, Piece.PieceColor.Black);
            _pieces[7] = new Piece(Piece.PieceType.Rook, Piece.PieceColor.Black);

            for (int i = 8; i <= 15; i++)
                _pieces[i] = new Piece(Piece.PieceType.Pawn, Piece.PieceColor.Black);

            /// Init White Pieces

            _pieces[56] = new Piece(Piece.PieceType.Rook, Piece.PieceColor.White);
            _pieces[57] = new Piece(Piece.PieceType.Knight, Piece.PieceColor.White);
            _pieces[58] = new Piece(Piece.PieceType.Bishop, Piece.PieceColor.White);
            _pieces[59] = new Piece(Piece.PieceType.Queen, Piece.PieceColor.White);
            _pieces[60] = new Piece(Piece.PieceType.King, Piece.PieceColor.White);
            _pieces[61] = new Piece(Piece.PieceType.Bishop, Piece.PieceColor.White);
            _pieces[62] = new Piece(Piece.PieceType.Knight, Piece.PieceColor.White);
            _pieces[63] = new Piece(Piece.PieceType.Rook, Piece.PieceColor.White);

            for (int i = 48; i <= 55; i++)
                _pieces[i] = new Piece(Piece.PieceType.Pawn, Piece.PieceColor.White);
        }

        private void PopulatePieces()
        {


            SetPiece(0, 0, _pieces[0]);
            SetPiece(1, 0, _pieces[1]);
            SetPiece(2, 0, _pieces[2]);
            SetPiece(3, 0, _pieces[3]);
            SetPiece(4, 0, _pieces[4]);
            SetPiece(5, 0, _pieces[5]);
            SetPiece(6, 0, _pieces[6]);
            SetPiece(7, 0, _pieces[7]);

            SetPiece(0, 7, _pieces[56]);
            SetPiece(1, 7, _pieces[57]);
            SetPiece(2, 7, _pieces[58]);
            SetPiece(3, 7, _pieces[59]);
            SetPiece(4, 7, _pieces[60]);
            SetPiece(5, 7, _pieces[61]);
            SetPiece(6, 7, _pieces[62]);
            SetPiece(7, 7, _pieces[63]);

            int j = 7, jj = 47;
            for (int i = 0; i < 8; i++)
            {

                SetPiece(i, 1, _pieces[++j]);
                SetPiece(i, 6, _pieces[++jj]);
            }

        }



        public void PickOrDropPiece(MouseEventArgs e)
        {
            Point location = e.Location;
            int x = location.X / 100;
            int y = location.Y / 100;
            bool pickOrDrop = CurrentPiece == null;
            if (pickOrDrop)
            {
                // Pick a piece
                Piece piece = GetPiece(x, y);
                SetPiece(x, y, null);
                if (piece != null)
                {
                  //  test.label1.Text = string.Format("You picked a {0} {1} at location {2},{3}", piece.Color, piece.Type, x,
                    //   y);
                }
                else
                {
                   // test.label1.Text = "Nothing there !";
                }
                CurrentPiece = piece;
            }
            else
            {
                // Drop picked piece
                SetPiece(x, y, CurrentPiece);
             //  test.label1.Text = string.Format("You dropped a {0} {1} at location {2},{3}", CurrentPiece.Color,
              //     CurrentPiece.Type, x,
             //      y);
                CurrentPiece = null;
            }
        }

    }
}
