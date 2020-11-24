using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    public sealed class King : Piece
    {
        protected override short Value => throw new NotImplementedException();

        protected override sbyte[] PieceSquareTable => new sbyte[]
        {
            -30,-40,-40,-50,-50,-40,-40,-30,
            -30,-40,-40,-50,-50,-40,-40,-30,
            -30,-40,-40,-50,-50,-40,-40,-30,
            -30,-40,-40,-50,-50,-40,-40,-30,
            -20,-30,-30,-40,-40,-30,-30,-20,
            -10,-20,-20,-20,-20,-20,-20,-10,
             20, 20,  0,  0,  0,  0, 20, 20,
             20, 30, 10,  0,  0, 10, 30, 20
        };

        public King(Color c) : base(typeof(King), c) { }
        public King(Color c, byte p) : base(typeof(King), c, p) { }

        public override char GetTypeChar() => IsWhite() ? 'K' : 'k';

        public override bool IsMoveValid(Square dest, List<Square> between)
        {
            foreach(Square s in between)
                return false;
            if (Math.Abs(dest.Row - Row) <= 1 && Math.Abs(dest.Column - Column) <= 1)
                return true;
            return false;
        }
    }
}
