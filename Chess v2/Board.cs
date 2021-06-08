using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Chess_v2
{
    class Board
    {
        private readonly Piece[] _pieces;


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
        public Board()
        {
            _pieces = new Piece[8 * 8];
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

        private void PopulatePieces()
        {
            for (int i = 0; i < 8; i++)
            {
                SetPiece(i, 1, new Piece(Piece.PieceType.Pawn, Piece.PieceColor.Black));
                SetPiece(i, 6, new Piece(Piece.PieceType.Pawn, Piece.PieceColor.White));
            }
        }



        public void PickOrDropPiece()
        {
           
        }

    }
}
