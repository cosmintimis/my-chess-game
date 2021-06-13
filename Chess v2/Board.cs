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

        private Piece CurrentPiece, piece1, piece2;
        public readonly Piece[] _pieces;
        int x, y, oldX, oldY;

        enum Player
        {
            white,
            black
        }
        private Player currentTurn;
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
            _pieces = new Piece[64];
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


        public bool DiagonalMove(int x, int y, int newX, int newY)
        {
            Piece aux;
            bool ok;
            if (Math.Abs(x - newX) == Math.Abs(y - newY))
            {
                ok = false;
                if (newX > x)
                {
                    if (y < newY)
                    {
                        while (x < newX - 1 && y < newY - 1)
                        {
                            aux = GetPiece(x + 1, y + 1);
                            if (aux != null)
                                ok = true;
                            x++;
                            y++;
                        }
                        if (ok == false)
                            return true;

                    }
                    if (y > newY)
                    {
                        while (x < newX - 1 && y - 1 > newY)
                        {
                            aux = GetPiece(x + 1, y - 1);
                            if (aux != null)
                                ok = true;
                            x++;
                            y--;
                        }
                        if (ok == false)
                            return true;

                    }
                }
                else if (x > newX)
                {
                    if (y < newY)
                    {
                        while (x - 1 > newX && y < newY - 1)
                        {
                            aux = GetPiece(x - 1, y + 1);
                            if (aux != null)
                                ok = true;
                            x--;
                            y++;
                        }
                        if (ok == false)
                            return true;

                    }
                    if (y > newY)
                    {
                        while (x - 1 > newX && y - 1 > newY)
                        {
                            aux = GetPiece(x - 1, y - 1);
                            if (aux != null)
                                ok = true;
                            x--;
                            y--;
                        }
                        if (ok == false)
                            return true;

                    }
                }

            }
            return false;

        }

        public bool HorizontalMove(int x, int y, int newX, int newY)
        {
            bool ok = false;
            Piece aux;
            if (y == newY)
            {
                if (x < newX)
                {
                    for (int i = x + 1; i < newX; i++)
                    {
                        aux = GetPiece(i, y);
                        if (aux != null)
                            ok = true;
                    }
                    if (ok == false)
                        return true;
                }
                else
                {
                    for (int i = x - 1; i > newX; i--)
                    {
                        aux = GetPiece(i, y);
                        if (aux != null)
                            ok = true;
                    }
                    if (ok == false)
                        return true;

                }
            }
            return false;
        }

        public bool VerticalMove(int x, int y, int newX, int newY)
        {
            bool ok = false;
            Piece aux;
            if (x == newX)
            {
                if (newY < y)
                {
                    for (int i = y - 1; i > newY; i--)
                    {
                        aux = GetPiece(x, i);
                        if (aux != null)
                            ok = true;
                    }
                    if (ok == false)
                        return true;
                }
                else
                {
                    for (int i = newY - 1; i > y; i--)
                    {
                        aux = GetPiece(x, i);
                        if (aux != null)
                            ok = true;
                    }
                    if (ok == false)
                        return true;
                }
            }
            return false;

        }
        public bool ValidMove(int x, int y, int newX, int newY, Piece piece1, Piece piece2)
        {
            switch (piece1._type)
            {
                case Piece.PieceType.Knight:
                    if (Math.Abs(x - newX) * Math.Abs(y - newY) == 2)
                        return true;
                    return false;
                case Piece.PieceType.Pawn:

                    if (piece1._color == Piece.PieceColor.White)
                    {
                        if (piece2 == null && x == newX && ((y - newY == 1) || (y == 1 || y == 6) && ((y - newY == 1) || y - newY == 2)))
                            return true;
                        if (y - 1 == newY && (newX + 1 == x || newX - 1 == x) && piece2 != null)
                            return true;

                    }

                    if (piece1._color == Piece.PieceColor.Black)
                    {
                        if (piece2 == null && x == newX && ((y - newY == -1) || (y == 1 || y == 6) && ((y - newY == -1) || y - newY == -2)))
                            return true;
                        if (y + 1 == newY && (newX + 1 == x || newX - 1 == x) && piece2 != null)
                            return true;

                    }
                    return false;

                case Piece.PieceType.Bishop:
                    if (DiagonalMove(x, y, newX, newY))
                        return true;
                    return false;

                case Piece.PieceType.Rook:
                    if (HorizontalMove(x, y, newX, newY) || VerticalMove(x, y, newX, newY))
                        return true;
                    return false;
                case Piece.PieceType.King:
                    if ((newX + 1 == x || newX - 1 == x || newX == x) && (newY + 1 == y || newY - 1 == y || newY == y))
                        return true;
                    return false;

                case Piece.PieceType.Queen:
                    if (DiagonalMove(x, y, newX, newY) || HorizontalMove(x, y, newX, newY) || VerticalMove(x, y, newX, newY))
                        return true;
                    return false;

                default:
                    return false;

            }
        }

        public void ChangeTurn()
        {
            if (currentTurn == Player.white)
                currentTurn = Player.black;
            else currentTurn = Player.white;


        }
        public void PickOrDropPiece(MouseEventArgs e)
        {
            Point location = e.Location;
            x = location.X / 100;
            y = location.Y / 100;

            bool pickOrDrop = CurrentPiece == null;

            if (pickOrDrop)
            {
                // Pick a piece
                piece1 = GetPiece(x, y);
                oldX = x;
                oldY = y;

                if (piece1 != null && ((piece1._color == Piece.PieceColor.White && currentTurn == Player.white) || (piece1._color == Piece.PieceColor.Black && currentTurn == Player.black)))
                    CurrentPiece = piece1;
                else CurrentPiece = null;
            }
            else
            {
                // Drop picked piece
                piece2 = GetPiece(x, y);

                if (ValidMove(oldX, oldY, x, y, piece1, piece2))
                {
                    if (piece2 != null && piece2.SameColor(piece1) == false)
                    {
                        SetPiece(oldX, oldY, null);
                        SetPiece(x, y, CurrentPiece);
                        ChangeTurn();

                    }
                    if (piece2 == null)
                    {
                        SetPiece(oldX, oldY, null);
                        SetPiece(x, y, CurrentPiece);
                        ChangeTurn();
                    }

                }
                else
                {
                    SetPiece(oldX, oldY, CurrentPiece);
                    MessageBox.Show("Mutare invalidă!");
                }
                CurrentPiece = null;
            }
        }

    }
}
