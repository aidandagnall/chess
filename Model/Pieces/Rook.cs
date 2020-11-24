using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    public sealed class Rook : Piece
    {
        protected override short Value => throw new NotImplementedException();

        protected override sbyte[] PieceSquareTable => new sbyte[]
        {
              0,  0,  0,  0,  0,  0,  0,  0,
              5, 10, 10, 10, 10, 10, 10,  5,
             -5,  0,  0,  0,  0,  0,  0, -5,
             -5,  0,  0,  0,  0,  0,  0, -5,
             -5,  0,  0,  0,  0,  0,  0, -5,
             -5,  0,  0,  0,  0,  0,  0, -5,
             -5,  0,  0,  0,  0,  0,  0, -5,
              0,  0,  0,  5,  5,  0,  0,  0
        };

        public Rook(Color c) : base(typeof(Rook), c) { }
        public Rook(Color c, byte p) : base(typeof(Rook), c, p) { }

        public override char GetTypeChar() => IsWhite() ? 'R' : 'r';

        public override bool IsMoveValid(Square dest, List<Square> between)
        {
            foreach (Square s in between)
                if (s.Piece != null)
                    return false;
            if (GetRow() == dest.Pos)
                return true;
            if (GetCol() == dest.Pos)
                return true;
            return false;
        }
    }
}
