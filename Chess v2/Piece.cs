using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Chess_v2
{
    public class Piece
    {
       
        private readonly PieceColor _color;
        private readonly PieceType _type;

        public Piece(PieceType type, PieceColor color)
        {
            _type = type;
            _color = color;
        }

        public PieceType Type
        {
            get { return _type; }
        }

        public PieceColor Color
        {
            get { return _color; }
        }

        protected bool Equals(Piece other)
        {
            return _color == other._color && _type == other._type;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Piece)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)_color * 397) ^ (int)_type;
            }
        }

        public static bool operator ==(Piece left, Piece right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Piece left, Piece right)
        {
            return !Equals(left, right);
        }

       
       
        public enum PieceType
        {
            Pawn,
            King,
            Queen

        }

        public enum PieceColor
        {
            Black,
            White
        }
    }


    
}
