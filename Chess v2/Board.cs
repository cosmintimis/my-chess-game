using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess_v2
{
    public class Board
    {

        private Piece CurrentPiece, piece1, piece2;
        public readonly Piece[] _pieces;
        private int x, y, oldX, oldY, b_kingX = 4, b_kingY = 0, w_kingX = 4, w_kingY = 7;
        private int w_contKing=0, w_contRookL=0, w_contRookR=0, b_contKing=0, b_contRookL=0, b_contRookR=0;
      

        public enum Player
        {
            white,
            black
        }
        public Player currentTurn;
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
        private bool DiagonalMove(int x, int y, int newX, int newY)
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

        private bool HorizontalMove(int x, int y, int newX, int newY)
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

        private bool VerticalMove(int x, int y, int newX, int newY)
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
        private bool ValidMove(int fromX, int fromY, int toX, int toY)
        {
            switch (GetPiece(fromX, fromY)._type)
            {
                case Piece.PieceType.Knight:
                    if (Math.Abs(fromX - toX) * Math.Abs(fromY - toY) == 2)
                        return true;
                    return false;
                case Piece.PieceType.Pawn:

                    if (GetPiece(fromX, fromY)._color == Piece.PieceColor.White)
                    {
                        if (GetPiece(toX, toY) == null && fromX == toX && ((fromY - toY == 1) || (fromY == 1 || fromY == 6) && ((fromY - toY == 1) || (fromY - toY == 2 && GetPiece(fromX, fromY - 1) == null))))
                            return true;
                        if (fromY - 1 == toY && (toX + 1 == fromX || toX - 1 == fromX) && GetPiece(toX, toY) != null)
                            return true;

                    }

                    if (GetPiece(fromX, fromY)._color == Piece.PieceColor.Black)
                    {
                        if (GetPiece(toX, toY) == null && fromX == toX && ((fromY - toY == -1) || (fromY == 1 || fromY == 6) && ((fromY - toY == -1) || (fromY - toY == -2 && GetPiece(fromX, fromY + 1) == null))))
                            return true;
                        if (fromY + 1 == toY && (toX + 1 == fromX || toX - 1 == fromX) && GetPiece(toX, toY) != null)
                            return true;

                    }
                    return false;

                case Piece.PieceType.Bishop:
                    if (DiagonalMove(fromX, fromY, toX, toY))
                        return true;
                    return false;

                case Piece.PieceType.Rook:
                    if (HorizontalMove(fromX, fromY, toX, toY) || VerticalMove(fromX, fromY, toX, toY))
                        return true;
                    return false;
                case Piece.PieceType.King:
                    if ((toX + 1 == fromX || toX - 1 == fromX || toX == fromX) && (toY + 1 == fromY || toY - 1 == fromY || toY == fromY))
                        return true;
                    return false;

                case Piece.PieceType.Queen:
                    if (DiagonalMove(fromX, fromY, toX, toY) || HorizontalMove(fromX, fromY, toX, toY) || VerticalMove(fromX, fromY, toX, toY))
                        return true;
                    return false;

                default:
                    return false;

            }
        }

        private void ChangeTurn()
        {
            if (currentTurn == Player.white)
                currentTurn = Player.black;
            else currentTurn = Player.white;


        }

        private void updateKingPosition(Piece piece, int x, int y)
        {
            if (piece != null)
                if (piece._type == Piece.PieceType.King)
                {
                    if (piece._color == Piece.PieceColor.Black)
                    {
                        b_kingX = x;
                        b_kingY = y;
                    }
                    else
                    {
                        w_kingX = x;
                        w_kingY = y;
                    }
                }
        }

        private bool isInCheck()
        {
            if (currentTurn == Player.black)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        Piece piece = GetPiece(x, y);
                        if (piece != null && piece._color != Piece.PieceColor.Black)
                        {
                            if (ValidMove(x, y, b_kingX, b_kingY))
                                return true;
                        }

                    }
                }
                return false;
            }
            else
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        Piece piece = GetPiece(x, y);
                        if (piece != null && piece._color != Piece.PieceColor.White)
                        {
                            if (ValidMove(x, y, w_kingX, w_kingY))
                                return true;
                        }

                    }
                }
                return false;
            }

        }

        private bool ShortCastle()
        {
            return false;
        }

        private bool CanEscapeFromCheck()
        {

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Piece piece1 = GetPiece(x, y);

                    if (piece1 != null && ((piece1._color == Piece.PieceColor.Black && currentTurn == Player.black) || (piece1._color == Piece.PieceColor.White && currentTurn == Player.white)))
                    {
                        for (int yy = 0; yy < 8; yy++)
                        {
                            for (int xx = 0; xx < 8; xx++)
                            {
                                Piece piece2 = GetPiece(xx, yy);
                                if (ValidMove(x, y, xx, yy))
                                {
                                    SetPiece(x, y, null);
                                    SetPiece(xx, yy, piece1);
                                    updateKingPosition(piece1, xx, yy);
                                    if (isInCheck() || (piece2 != null && (piece2.SameColor(piece1) == true || piece2._type == Piece.PieceType.King)))
                                    {
                                        SetPiece(x, y, piece1);
                                        SetPiece(xx, yy, piece2);
                                        updateKingPosition(piece1, x, y);
                                    }
                                    else
                                    {
                                        SetPiece(x, y, piece1);
                                        SetPiece(xx, yy, piece2);
                                        updateKingPosition(piece1, x, y);
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckMate()
        {
            if (isInCheck() && !CanEscapeFromCheck())
                return true;
            else return false;
        }
        public bool Stalemate()
        {
            if (!isInCheck() && !CanEscapeFromCheck())
                return true;
            else return false;
        }

        private void MakeMove(Piece p1, Piece p2, int fromX, int fromY, int toX, int toY)
        {
            SetPiece(fromX, fromY, null);
            SetPiece(toX, toY, p1);
            updateKingPosition(p1, toX, toY);
            if (isInCheck() || (p2 != null && (p2.SameColor(p1) == true || p2._type == Piece.PieceType.King)))
            {
                SetPiece(fromX, fromY, p1);
                SetPiece(toX, toY, p2);
                // MessageBox.Show("Nu se poate!");
            }
            else
            {
                if(fromX == 4 && fromY == 0)
                    b_contKing++;
                if(fromX == 0 && fromY == 0)
                     b_contRookL++;
                if(fromX == 7 && fromY == 0)
                     b_contRookR++;
                if(fromX == 4 && fromY == 7)
                     w_contKing++;
                if(fromX == 0 && fromY == 7)
                      w_contRookL++;
                if(fromX == 7 && fromY == 7)
                      w_contRookR++;

                ChangeTurn();      
            }
        }

        public void PickOrDropPiece(MouseEventArgs e)
        {
            Point location = e.Location;
            x = location.X / 100;
            y = location.Y / 100;

            bool pickOrDrop = CurrentPiece == null;

            if (pickOrDrop)
            {

                piece1 = GetPiece(x, y);
                oldX = x;
                oldY = y;
                if (piece1 != null && ((piece1._color == Piece.PieceColor.White && currentTurn == Player.white) || (piece1._color == Piece.PieceColor.Black && currentTurn == Player.black)))
                    CurrentPiece = piece1;
                else CurrentPiece = null;
            }
            else
            {
                piece2 = GetPiece(x, y);

                if (ValidMove(oldX, oldY, x, y))
                {
                    MakeMove(piece1, piece2, oldX, oldY, x, y);
                }
                else
                    MessageBox.Show("Mutare invalidă!");

                CurrentPiece = null;
            }

        }
    }
}