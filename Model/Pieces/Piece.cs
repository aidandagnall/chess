using System;
using System.Collections.Generic;
namespace Chess.Model.Pieces
{
    public abstract class Piece
    {
        protected Type Type;
        public readonly Color PieceColor;
        protected byte _Pos;
        protected byte _Moves;
        public byte Pos 
        {
            get { return _Pos; }
            set { _Pos = value; }
        }
        public byte Row
        {
            get { return (byte) (_Pos / Game.BoardSize); }
        }
        public byte Column
        {
            get { return (byte) (_Pos % Game.BoardSize); }
        }
        public byte Moves
        {
            get { return _Moves; }
            set { _Moves = value; }
        }
        public void IncrementMoves() { Moves++; }

        protected abstract short Value { get; }
        protected abstract sbyte[] PieceSquareTable { get; }

        public string Image 
        {
            get
            {
                return "pack://application:,,,/Resources/Images/" + PieceColor + GetTypeString(GetTypeChar()) + ".png";
            }
        } 
            
        protected Piece(Type t, Color c)
        {
            Type = t;
            PieceColor = c;
            Pos = Game.BoardSize * Game.BoardSize;
            if (!IsWhite())
                Array.Reverse(PieceSquareTable);
        }

        protected Piece(Type t, Color c, byte p)
        {
            Type = t;
            PieceColor = c;
            Pos = p;
            if (!IsWhite())
                Array.Reverse(PieceSquareTable);
        }

        public bool IsWhite() => PieceColor == Color.White;

        public abstract bool IsMoveValid(Square dest, List<Square> between);

        public byte GetRow() => (byte) (Pos / Game.BoardSize);

        public byte GetCol() => (byte) (Pos % Game.BoardSize);

        public abstract char GetTypeChar();

        public static string GetTypeString(char c)
        {
            return char.ToUpper(c) switch
            {
                'P' => "Pawn",
                'R' => "Rook",
                'N' => "Knight",
                'B' => "Bishop",
                'Q' => "Queen",
                'K' => "King",
                _ => null
            };
        }

        public static Type GetType(char c)
        {
            return char.ToUpper(c) switch
            {
                'P' => typeof(Pawn),
                'R' => typeof(Rook),
                'N' => typeof(Knight),
                'B' => typeof(Bishop),
                'Q' => typeof(Queen),
                'K' => typeof(King),
                _ => null
            };
        }
    }   

}