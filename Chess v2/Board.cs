﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chess_v2
{
    public class Board
    {
        private readonly Piece[] _pieces;

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


            SetPiece(0, 0, new Piece(Piece.PieceType.Rook, Piece.PieceColor.Black, new Bitmap(Properties.Resources.king_black));
            SetPiece(1, 0, new Piece(Piece.PieceType.Knight, Piece.PieceColor.Black));
            SetPiece(2, 0, new Piece(Piece.PieceType.Bishop, Piece.PieceColor.Black));
            SetPiece(3, 0, new Piece(Piece.PieceType.Queen, Piece.PieceColor.Black));
            SetPiece(4, 0, new Piece(Piece.PieceType.King, Piece.PieceColor.Black));
            SetPiece(5, 0, new Piece(Piece.PieceType.Bishop, Piece.PieceColor.Black));
            SetPiece(6, 0, new Piece(Piece.PieceType.Knight, Piece.PieceColor.Black));
            SetPiece(7, 0, new Piece(Piece.PieceType.Rook, Piece.PieceColor.Black));

            SetPiece(0, 7, new Piece(Piece.PieceType.Rook, Piece.PieceColor.White));
            SetPiece(1, 7, new Piece(Piece.PieceType.Knight, Piece.PieceColor.White));
            SetPiece(2, 7, new Piece(Piece.PieceType.Bishop, Piece.PieceColor.White));
            SetPiece(3, 7, new Piece(Piece.PieceType.Queen, Piece.PieceColor.White));
            SetPiece(4, 7, new Piece(Piece.PieceType.King, Piece.PieceColor.White));
            SetPiece(5, 7, new Piece(Piece.PieceType.Bishop, Piece.PieceColor.White));
            SetPiece(6, 7, new Piece(Piece.PieceType.Knight, Piece.PieceColor.White));
            SetPiece(7, 7, new Piece(Piece.PieceType.Rook, Piece.PieceColor.White));


            for (int i = 0; i < 8; i++)
            {

                SetPiece(i, 1, new Piece(Piece.PieceType.Pawn, Piece.PieceColor.Black));
                SetPiece(i, 6, new Piece(Piece.PieceType.Pawn, Piece.PieceColor.White));
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
