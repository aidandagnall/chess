using System.Collections.Generic;
namespace Chess.Model.Pieces
{
    public sealed class Pawn : Piece
    {
        protected override short Value => 100;
        protected override sbyte[] PieceSquareTable => new sbyte[]
         {
            0,  0,  0,  0,  0,  0,  0,  0,
            50, 50, 50, 50, 50, 50, 50, 50,
            10, 10, 20, 30, 30, 20, 10, 10,
            5,  5, 10, 25, 25, 10,  5,  5,
            0,  0,  0, 20, 20,  0,  0,  0,
            5, -5,-10,  0,  0,-10, -5,  5,
            5, 10, 10,-20,-20, 10, 10,  5,
            0,  0,  0,  0,  0,  0,  0,  0
        };

        public override bool IsMoveValid(Square dest, List<Square> between)
        {
            sbyte row = (sbyte) (IsWhite() ? Game.BoardSize : -Game.BoardSize);
            if (Moves == 0)
                if (dest.Pos == Pos + 2 * row) 
                    return true;
            if (dest.Piece == null)
                return dest.Pos== Pos + row ? true : false;
            return dest.Pos== Pos + row - 1 || dest.Pos== Pos + row + 1 ? true : false;
        }

        public override char GetTypeChar() => IsWhite() ? 'P' : 'p';

        public Pawn(Color c) : base(typeof(Pawn), c) {  }
        public Pawn(Color c, byte p) : base(typeof(Pawn), c, p) {  }

    }
}
