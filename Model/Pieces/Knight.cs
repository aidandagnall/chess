using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    public sealed class Knight : Piece
    {
        private static readonly byte[] ValidMoves =
        { // Calculates absolute values for knight moves
            Game.BoardSize * 2 - 1, Game.BoardSize * 2 + 1,
            Game.BoardSize - 2, Game.BoardSize + 2
        };
        protected override short Value => throw new NotImplementedException();

        protected override sbyte[] PieceSquareTable => new sbyte[]
        {
            -50,-40,-30,-30,-30,-30,-40,-50,
            -40,-20,  0,  0,  0,  0,-20,-40,
            -30,  0, 10, 15, 15, 10,  0,-30,
            -30,  5, 15, 20, 20, 15,  5,-30,
            -30,  0, 15, 20, 20, 15,  0,-30,
            -30,  5, 10, 15, 15, 10,  5,-30,
            -40,-20,  0,  5,  5,  0,-20,-40,
            -50,-40,-30,-30,-30,-30,-40,-50
        };

        public Knight(Color c) : base(typeof(Knight), c) { }
        public Knight(Color c, byte p) : base(typeof(Knight), c, p) { }

        public override char GetTypeChar() => IsWhite() ? 'N' : 'n';

        public override bool IsMoveValid(Square dest, List<Square> between)
        {
            if (Math.Abs(dest.Row - Row) == 2 && Math.Abs(dest.Column - Column) == 1)
                return true;
            if (Math.Abs(dest.Row - Row) == 1 && Math.Abs(dest.Column - Column) == 2)
                return true;
            return false;
        }
    }
}
