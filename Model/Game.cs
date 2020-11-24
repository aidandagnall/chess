using System;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Collections.Generic;
using Chess.Model.Pieces;
namespace Chess.Model
{
    public class Game
    {
        public const int BoardSize = 8;
        private const string Fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        private List<Square> _Board;
        public List<Square> Board => _Board;
        private bool[] Castle = new bool[4];
        private Color ToMove;
        private byte? EnPassantPosition;
        private byte FiftyMoveRule;
        private byte MoveCount;
        public Game() : this(Fen) {}

        public Game(string input)
        {
            InitialiseBoard();
            BuildBoard(input);
        }

        private void InitialiseBoard()
        {
            _Board = new List<Square>();
            for (var i = 7; i >= 0; i--)
                for (var j = 0; j < 8; j++)
                    Board.Add(new Square((byte) i, (byte) j));
        }

        private void BuildBoard(string fen)
        {
            var fenSplit = fen.Split(' ');
            ToMove = fenSplit[1] == "w" ? Color.White : Color.Black;

            Castle[0] = fenSplit[2].Contains('K');
            Castle[1] = fenSplit[2].Contains('Q');
            Castle[2] = fenSplit[2].Contains('k');
            Castle[3] = fenSplit[2].Contains('q');

            if (fenSplit[3] == "-")
                EnPassantPosition = null; 
            else
                EnPassantPosition = byte.Parse(fenSplit[3]);
            FiftyMoveRule = byte.Parse(fenSplit[4]);
            MoveCount = byte.Parse(fenSplit[5]);

            var i = 0;
            fenSplit[0] = fenSplit[0].Replace("/", "");
            foreach (var c in fenSplit[0])
            {
                var color = char.IsUpper(c) ? Color.White : Color.Black;
                if (Piece.GetType(c) != null)
                    Board[i].Piece = (Piece) Activator.CreateInstance(Piece.GetType(c), color, Board[i++].Pos);
                else
                    i += (byte) char.GetNumericValue(c);
            } 
        }

        public bool IsMoveValid(Square orig, Square dest)
        {
            if (orig == dest)
                return false;
            if (orig.Piece != null && dest.Piece != null)
                if (orig.Piece.PieceColor == dest.Piece.PieceColor)
                    return false;
            return orig.Piece.IsMoveValid(dest, GetSquaresBetween(orig, dest));
        }

        public List<Square> GetSquaresBetween(Square orig, Square dest)
        {
            List<Square> sb = new List<Square>();

            var rowMod = orig.Row == dest.Row ? 0 : 1;
            var colMod = orig.Column == dest.Column ? 0 : 1;
            var range = Math.Max(Math.Abs(orig.Row - dest.Row), Math.Abs(orig.Column - dest.Column));
            var rowStart = Math.Min(orig.Row, dest.Column);
            var colStart = Math.Min(orig.Column, dest.Column);

            for (var i = 1; i < range; i++)
                sb.Add(GetSquare((byte) (rowStart + (i * rowMod)), (byte) (colStart + (i * colMod))));

            return sb;
        }

        public Square GetSquare(byte row, byte column)
        {
            return GetSquare((byte) (row * BoardSize + column));
        }

        public Square GetSquare(byte pos)
        {
            foreach(Square s in Board)
                if (s.Pos == pos)
                    return s;
            return null;
        }        
    }
}