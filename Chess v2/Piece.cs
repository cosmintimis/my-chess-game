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
        private readonly Bitmap _image;


        public Piece(PieceType type, PieceColor color,  Bitmap image)
        {
            _type = type;
            _color = color;
            _image = image;
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
