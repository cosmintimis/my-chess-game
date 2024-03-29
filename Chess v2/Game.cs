﻿using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Chess_v2
{
    public class Game
    {
        public Game()
        {

        }
     
        public Dictionary<Piece, Bitmap> PieceBitmaps;
        public Board board;
        public int WhitePawnPromotion = -1, BlackPawnPromotion = -1;
        public void InitGame()
        {

            board = new Board();
        
            PieceBitmaps = new Dictionary<Piece, Bitmap>();

            PieceBitmaps.Add(board._pieces[0], new Bitmap(Properties.Resources.rook_black));
            PieceBitmaps.Add(board._pieces[1], new Bitmap(Properties.Resources.knight_black));
            PieceBitmaps.Add(board._pieces[2], new Bitmap(Properties.Resources.bishop_black));
            PieceBitmaps.Add(board._pieces[3], new Bitmap(Properties.Resources.queen_black));
            PieceBitmaps.Add(board._pieces[4], new Bitmap(Properties.Resources.king_black));
            PieceBitmaps.Add(board._pieces[5], new Bitmap(Properties.Resources.bishop_black));
            PieceBitmaps.Add(board._pieces[6], new Bitmap(Properties.Resources.knight_black));
            PieceBitmaps.Add(board._pieces[7], new Bitmap(Properties.Resources.rook_black));
            for (int i = 8; i <= 15; i++)
                PieceBitmaps.Add(board._pieces[i], new Bitmap(Properties.Resources.pawn_black));


            PieceBitmaps.Add(board._pieces[56], new Bitmap(Properties.Resources.rook_white));
            PieceBitmaps.Add(board._pieces[57], new Bitmap(Properties.Resources.knight_white));
            PieceBitmaps.Add(board._pieces[58], new Bitmap(Properties.Resources.bishop_white));
            PieceBitmaps.Add(board._pieces[59], new Bitmap(Properties.Resources.queen_white));
            PieceBitmaps.Add(board._pieces[60], new Bitmap(Properties.Resources.king_white));
            PieceBitmaps.Add(board._pieces[61], new Bitmap(Properties.Resources.bishop_white));
            PieceBitmaps.Add(board._pieces[62], new Bitmap(Properties.Resources.knight_white));
            PieceBitmaps.Add(board._pieces[63], new Bitmap(Properties.Resources.rook_white));
            for (int i = 48; i <= 55; i++)
                PieceBitmaps.Add(board._pieces[i], new Bitmap(Properties.Resources.pawn_white));



        }

        private void ChangePawns()
        {

            for (int i = 0; i <= 7; i++)
            {
                if (board._pieces[i] != null && board._pieces[i]._type == Piece.PieceType.Pawn)
                {
                    WhitePawnPromotion = i;

                }
            }
            for (int i = 56; i <= 63; i++)
            {
                if (board._pieces[i] != null && board._pieces[i]._type == Piece.PieceType.Pawn)
                {

                    BlackPawnPromotion = i - 56;

                }

            }
        }
        public Bitmap DrawGame()
        {
            Bitmap bitmap = board.CreateBoard();
            ChangePawns();
            DrawPieces(bitmap);

            return bitmap;

        }
        public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
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
                Bitmap bitmap1;
                Bitmap bitmap2;

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        Piece piece = board.GetPiece(x, y);

                        if (piece != null)
                        {
                            bitmap1 = PieceBitmaps[piece];
                            bitmap2 = ResizeBitmap(bitmap1, board.TileWidth, board.TileHeight);
                            graphics.DrawImage(bitmap2, new Point(x * board.TileWidth, y * board.TileHeight));
                        }
                    }
                }
            }
        }

    }
}
