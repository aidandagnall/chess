using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    public sealed class Bishop : Piece
    {
        protected override short Value => throw new NotImplementedException();

        protected override sbyte[] PieceSquareTable =>  new sbyte[] 
        {
            -20,-10,-10,-10,-10,-10,-10,-20,
            -10,  0,  0,  0,  0,  0,  0,-10,
            -10,  0,  5, 10, 10,  5,  0,-10,
            -10,  5,  5, 10, 10,  5,  5,-10,
            -10,  0, 10, 10, 10, 10,  0,-10,
            -10, 10, 10, 10, 10, 10, 10,-10,
            -10,  5,  0,  0,  0,  0,  5,-10,
            -20,-10,-10,-10,-10,-10,-10,-20
        };

        public Bishop(Color c) : base(typeof(Bishop), c) { }
        public Bishop(Color c, byte p) : base(typeof(Bishop), c, p) { }


        public override char GetTypeChar() => IsWhite() ? 'B' : 'b';

        public override bool IsMoveValid(Square dest, List<Square> between)
        {
            foreach (Square s in between)
                if (s.Piece != null)
                    return false;
            if (Math.Abs(dest.Pos- Pos) % (Game.BoardSize - 1) == 0)
                return true;
            if (Math.Abs(dest.Pos- Pos) % (Game.BoardSize + 1) == 0)
                return true;
            return false;
        }
    }
}
