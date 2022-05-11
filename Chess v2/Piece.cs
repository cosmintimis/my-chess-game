namespace Chess_v2
{
    public class Piece
    {
        public PieceColor _color;
        public PieceType _type;

        public Piece(PieceType type, PieceColor color)
        {
            _type = type;
            _color = color;
        }
        public bool SameColor(Piece piece)
        {
            if (piece != null && _color == piece._color)
                return true;
            return false;
        }
        public enum PieceType
        {
            King,
            Queen,
            Rook,
            Bishop,
            Knight,
            Pawn
        }
        public enum PieceColor
        {
            Black,
            White
        }

    }

}
