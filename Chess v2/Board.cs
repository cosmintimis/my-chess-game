using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess_v2
{
    public class Board
    {
        public Piece CurrentPiece, PiecePicked, SquareToDrop, LastPieceMoved;
        public readonly Piece[] _pieces;
        public int x, y, oldX, oldY, b_kingX = 4, b_kingY = 0, w_kingX = 4, w_kingY = 7;
        public bool PieceWasMoved = false;
        private int[] LastPieceMovedCoordinates = new int[] { -1, -1, -1, -1 };
        private int w_contKing = 0, w_contRookL = 0, w_contRookR = 0, b_contKing = 0, b_contRookL = 0, b_contRookR = 0;


        public int TileWidth = 100;
        public int TileHeight = 100;

        public enum Player
        {
            white,
            black
        }
        public Player currentTurn = Player.white;
        public Piece.PieceColor OppositeColor = Piece.PieceColor.Black;

        public Board()
        {
            _pieces = new Piece[64];
            InitPieces();
            PopulatePieces();
        }

        /// Board drawing 
        public Bitmap CreateBoard()
        {
            var bitmap = new Bitmap(TileWidth * 8, TileHeight * 8);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        Brush brush = (x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0) ? Brushes.Beige : Brushes.BurlyWood;
                        graphics.FillRectangle(brush, new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight));
                    }
                }
            }
            return bitmap;
        }
        public Piece GetPiece(int x, int y)
        {
            if (x >= 0 && x <= 7 && y >= 0 && y <= 7)
            {
                int i = y * 8 + x;
                return _pieces[i];
            }
            else return null;
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

        private void ChangeTurn()
        {
            if (currentTurn == Player.white)
            {
                currentTurn = Player.black;
                OppositeColor = Piece.PieceColor.White;
            }
            else
            {
                currentTurn = Player.white;
                OppositeColor = Piece.PieceColor.Black;
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

        /// Verify if the move is legal
        public bool LegalMove(int fromX, int fromY, int toX, int toY)
        {
            Piece p1 = GetPiece(fromX, fromY), p2 = GetPiece(toX, toY);

            SetPiece(fromX, fromY, null);
            SetPiece(toX, toY, p1);
            UpdateKingPosition(p1, toX, toY);
            if (isInCheck() || (p2 != null && (p2.SameColor(p1) == true || p2._type == Piece.PieceType.King)))
            {
                SetPiece(fromX, fromY, p1);
                SetPiece(toX, toY, p2);
                UpdateKingPosition(p1, fromX, fromY);
                return false;
            }
            SetPiece(fromX, fromY, p1);
            SetPiece(toX, toY, p2);
            UpdateKingPosition(p1, fromX, fromY);
            return true;

        }
        public bool PieceCanMove(int fromX, int fromY, int toX, int toY)
        {

            Piece MovedPiece = GetPiece(fromX, fromY);
            Piece SquareToMove = GetPiece(toX, toY);

            if (MovedPiece == null)
                return false;
            if (SquareToMove != null && SquareToMove.SameColor(MovedPiece))
                return false;
            switch (MovedPiece._type)
            {
                case Piece.PieceType.Knight:
                    if (Math.Abs(fromX - toX) * Math.Abs(fromY - toY) == 2)
                        return true;
                    return false;

                case Piece.PieceType.Pawn:
                    if (MovedPiece._color == Piece.PieceColor.White)
                    {
                        if (SquareToMove == null && fromX == toX && (fromY - toY == 1 || (fromY == 6 && ((fromY - toY == 1) || (fromY - toY == 2 && GetPiece(fromX, fromY - 1) == null)))))
                            return true;

                        if (fromY - 1 == toY && (toX + 1 == fromX || toX - 1 == fromX) && SquareToMove != null)
                            return true;
                    }
                    if (MovedPiece._color == Piece.PieceColor.Black)
                    {
                        if (SquareToMove == null && fromX == toX && (fromY - toY == -1 || (fromY == 1 && ((fromY - toY == -1) || (fromY - toY == -2 && GetPiece(fromX, fromY + 1) == null)))))
                            return true;

                        if (fromY + 1 == toY && (toX + 1 == fromX || toX - 1 == fromX) && SquareToMove != null)
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

        /// All possible moves of a knight
        readonly int[] knight_X = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        readonly int[] knight_Y = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        private bool SquareIsAttacked(int x, int y)
        {
            /// Check if a white pawn attack
            Piece Pawn = GetPiece(x - 1, y + 1);
            if (Pawn != null && Pawn._type == Piece.PieceType.Pawn && Pawn._color == OppositeColor)
                return true;
            Pawn = GetPiece(x + 1, y + 1);
            if (Pawn != null && Pawn._type == Piece.PieceType.Pawn && Pawn._color == OppositeColor)
                return true;

            /// Check if a black pawn attack
            Pawn = GetPiece(x - 1, y - 1);
            if (Pawn != null && Pawn._type == Piece.PieceType.Pawn && Pawn._color == OppositeColor)
                return true;
            Pawn = GetPiece(x + 1, y - 1);
            if (Pawn != null && Pawn._type == Piece.PieceType.Pawn && Pawn._color == OppositeColor)
                return true;

            /// Check for knight possible attackk
            for (int i = 0; i <= 7; i++)
            {
                if (PieceCanMove(x + knight_X[i], y + knight_Y[i], x, y) && GetPiece(x + knight_X[i], y + knight_Y[i])._color == OppositeColor)
                    return true;
            }
            /// Check for vert and horizontally
            for (int i = 0; i <= 7; i++)
            {
                if (PieceCanMove(x, i, x, y) && GetPiece(x, i)._type != Piece.PieceType.Pawn && GetPiece(x, i)._color == OppositeColor)
                    return true;
                if (PieceCanMove(i, y, x, y) && GetPiece(i, y)._type != Piece.PieceType.Pawn && GetPiece(i, y)._color == OppositeColor)
                    return true;
            }
            /// Check for all diagonals
            int copyX = x; int copyY = y;
            while (copyX > 0 && copyY > 0)
            {
                if (PieceCanMove(copyX - 1, copyY - 1, x, y) && GetPiece(copyX - 1, copyY - 1)._color == OppositeColor)
                    return true;
                copyX--;
                copyY--;
            }
            copyX = x; copyY = y;
            while (copyX < 7 && copyY < 7)
            {
                if (PieceCanMove(copyX + 1, copyY + 1, x, y) && GetPiece(copyX + 1, copyY + 1)._color == OppositeColor)
                    return true;
                copyX++;
                copyY++;
            }
            copyX = x; copyY = y;
            while (copyX > 0 && copyY < 7)
            {
                if (PieceCanMove(copyX - 1, copyY + 1, x, y) && GetPiece(copyX - 1, copyY + 1)._color == OppositeColor)
                    return true;
                copyX--;
                copyY++;
            }
            copyX = x; copyY = y;
            while (copyX < 7 && copyY > 0)
            {
                if (PieceCanMove(copyX + 1, copyY - 1, x, y) && GetPiece(copyX + 1, copyY - 1)._color == OppositeColor)
                    return true;
                copyX++;
                copyY--;
            }

            return false;
        }
        private void UpdateKingPosition(Piece king, int x, int y)
        {
            if (king != null)
                if (king._type == Piece.PieceType.King)
                {
                    if (king._color == Piece.PieceColor.Black)
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
                if (SquareIsAttacked(b_kingX, b_kingY))
                    return true;
                return false;
            }
            else
            {
                if (SquareIsAttacked(w_kingX, w_kingY))
                    return true;
                return false;
            }

        }
        private bool CanEscapeFromCheck()
        {

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Piece piece1 = GetPiece(x, y);

                    if (piece1 != null && piece1._color != OppositeColor)
                    {
                        for (int yy = 0; yy < 8; yy++)
                        {
                            for (int xx = 0; xx < 8; xx++)
                            {
                                if (PieceCanMove(x, y, xx, yy) && LegalMove(x, y, xx, yy))
                                    return true;
                                         
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
            else
                return false;

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
            UpdateKingPosition(p1, toX, toY);

            if (fromX == 4 && fromY == 0)
                b_contKing++;
            if (fromX == 0 && fromY == 0)
                b_contRookL++;
            if (fromX == 7 && fromY == 0)
                b_contRookR++;
            if (fromX == 4 && fromY == 7)
                w_contKing++;
            if (fromX == 0 && fromY == 7)
                w_contRookL++;
            if (fromX == 7 && fromY == 7)
                w_contRookR++;
            ChangeTurn();

        }

        private void UpdateLastPieceMoved(Piece PieceMoved, int fromX, int fromY, int toX, int toY)
        {
            LastPieceMoved = PieceMoved;
            LastPieceMovedCoordinates[0] = fromX;
            LastPieceMovedCoordinates[1] = fromY;
            LastPieceMovedCoordinates[2] = toX;
            LastPieceMovedCoordinates[3] = toY;

        }

        public bool CanDoCastle(int fromX, int fromY, int toX, int toY)
        {
            Piece king = GetPiece(fromX, fromY);

            if (king._type != Piece.PieceType.King)
                return false;

            /// Check for LongCastelBlack
            if (GetPiece(0, 0) != null && GetPiece(0, 0)._type == Piece.PieceType.Rook && toX == 2 && toY == 0 && b_contKing == 0 && b_contRookL == 0 && GetPiece(toX - 1, toY) == null && GetPiece(toX, toY) == null && GetPiece(toX + 1, toY) == null)
            {
                if (SquareIsAttacked(b_kingX, b_kingY) == false && SquareIsAttacked(b_kingX - 1, b_kingY) == false && SquareIsAttacked(b_kingX - 2, b_kingY) == false)
                    return true;
            }
            /// Check for ShortCastelBlack
            if (GetPiece(7, 0) != null && GetPiece(7, 0)._type == Piece.PieceType.Rook && toX == 6 && toY == 0 && b_contKing == 0 && b_contRookR == 0 && GetPiece(toX - 1, toY) == null && GetPiece(toX, toY) == null)
            {
                if (SquareIsAttacked(b_kingX, b_kingY) == false && SquareIsAttacked(b_kingX + 1, b_kingY) == false && SquareIsAttacked(b_kingX + 2, b_kingY) == false)
                    return true;
            }
            /// Check for LongCastelWhite
            if (GetPiece(0, 7) != null && GetPiece(0, 7)._type == Piece.PieceType.Rook && toX == 2 && toY == 7 && w_contKing == 0 && w_contRookL == 0 && GetPiece(toX - 1, toY) == null && GetPiece(toX, toY) == null && GetPiece(toX + 1, toY) == null)
            {
                if (SquareIsAttacked(w_kingX, w_kingY) == false && SquareIsAttacked(w_kingX - 1, w_kingY) == false && SquareIsAttacked(w_kingX - 2, w_kingY) == false)
                    return true;
            }
            /// Check for ShortCastelWhite
            if (GetPiece(7, 7) != null && GetPiece(7, 7)._type == Piece.PieceType.Rook && toX == 6 && toY == 7 && w_contKing == 0 && w_contRookR == 0 && GetPiece(toX - 1, toY) == null && GetPiece(toX, toY) == null)
            {
                if (SquareIsAttacked(w_kingX, w_kingY) == false && SquareIsAttacked(w_kingX + 1, w_kingY) == false && SquareIsAttacked(w_kingX + 2, w_kingY) == false)
                    return true;
            }

            return false;

        }

        public bool CanDoEnPassant(int fromX, int fromY, int toX, int toY)
        {
            Piece PawnThatAttack = GetPiece(fromX, fromY);

            if (PawnThatAttack._type != Piece.PieceType.Pawn)
                return false;
            if (LastPieceMoved != null && LastPieceMoved._type != Piece.PieceType.Pawn)
                return false;
            if (Math.Abs(LastPieceMovedCoordinates[3] - LastPieceMovedCoordinates[1]) != 2) /// Check for double square move
                return false;
            if (PawnThatAttack._color == Piece.PieceColor.White)
            {
                if (fromY == 3 && LastPieceMovedCoordinates[3] == 3 && toX == LastPieceMovedCoordinates[2] && toY == LastPieceMovedCoordinates[1] + 1)
                    return true;
            }
            else
            {
                if (fromY == 4 && LastPieceMovedCoordinates[3] == 4 && toX == LastPieceMovedCoordinates[2] && toY == LastPieceMovedCoordinates[1] - 1)
                    return true;
            }

            return false;

        }

        private void DoEnPassant()
        {
            SetPiece(LastPieceMovedCoordinates[2], LastPieceMovedCoordinates[3], null);
            SetPiece(x, y, PiecePicked);
            SetPiece(oldX, oldY, null);
            UpdateLastPieceMoved(PiecePicked, oldX, oldY, x, y);
            ChangeTurn();
            CurrentPiece = null;
            PieceWasMoved = true;
        }

        private void DoCastle()
        {
            if (x < 4)
            {
                SetPiece(x, y, PiecePicked);
                SetPiece(oldX, oldY, null);
                SetPiece(x + 1, y, GetPiece(0, y));
                SetPiece(0, y, null);

            }
            else
            {
                SetPiece(x, y, PiecePicked);
                SetPiece(oldX, oldY, null);
                SetPiece(x - 1, y, GetPiece(7, y));
                SetPiece(7, y, null);

            }
            if (PiecePicked._color == Piece.PieceColor.Black)
                b_contKing++;
            else w_contKing++;
            UpdateKingPosition(PiecePicked, x, y);
            UpdateLastPieceMoved(PiecePicked, oldX, oldY, x, y);
            ChangeTurn();
            CurrentPiece = null;
            PieceWasMoved = true;
        }


        public void PickOrDropPiece(MouseEventArgs e, int tileWidth, int tileHeight)
        {

            Point location = e.Location;
            x = location.X / tileWidth;
            y = location.Y / tileHeight;

           

            if (GetPiece(x, y) != null && GetPiece(x, y) != GetPiece(oldX, oldY) && GetPiece(x, y)._color != OppositeColor)
                CurrentPiece = null;

            bool PickOrDrop = CurrentPiece == null;

            if (PickOrDrop)
            {

                PiecePicked = GetPiece(x, y);
                
                if (PiecePicked != null && PiecePicked._color != OppositeColor)
                {
                    CurrentPiece = PiecePicked;
                    oldX = x;
                    oldY = y;
                }
                else CurrentPiece = null;
                
            }
            else
            {

                SquareToDrop = GetPiece(x, y);

                if (CanDoCastle(oldX, oldY, x, y))
                    DoCastle();
                else if (CanDoEnPassant(oldX, oldY, x, y))
                    DoEnPassant();
                else
                {

                    if (PieceCanMove(oldX, oldY, x, y) && LegalMove(oldX, oldY, x, y))
                    {
                        MakeMove(PiecePicked, SquareToDrop, oldX, oldY, x, y);
                        UpdateLastPieceMoved(PiecePicked, oldX, oldY, x, y);
                        CurrentPiece = null;
                        PieceWasMoved = true;
                    }
                }

            }

        }
    }
}