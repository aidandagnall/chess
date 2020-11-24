using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using Chess.Model.Pieces;

namespace Chess.Model
{
    public class Square : INotifyPropertyChanged
    {
        private byte _Pos;
        private Color _SquareColor;
        private Piece _Piece;
        private Highlight _squareHighlight;
        public Piece Piece
        {
            get { return _Piece; }
            set 
            { 
                _Piece = value;
                NotifyPropertyChanged();
            }
        }
        public byte Pos
        {
            get { return _Pos ; }
        }
        public byte Row
        {
            get { return (byte) (_Pos / Game.BoardSize); }
        }
        public byte Column 
        {
            get { return (byte) (_Pos % Game.BoardSize); }
        }
        public Highlight SquareHighlight
        {
            get { return _squareHighlight; }
            set 
            { 
                _squareHighlight = value;
                NotifyPropertyChanged(); 
            }
        }
        public Color SquareColor
        {
            get {return _SquareColor; }
            set 
            {
                _SquareColor = value; 
                NotifyPropertyChanged();
            }
        }
        public string Image
        {
            get 
            {
                if(Piece == null)
                    return null;
                return Piece.Image;
            }
        }
        public bool ContainsPiece
        {
            get { return Piece != null; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Square(byte x, byte y)
        {
            _Pos = (byte) (x * Game.BoardSize + y);
            _SquareColor = (Row + Column) % 2 == 0 ? Color.Black : Color.White;
        }

    }

    public enum Highlight
    {
        None,
        Selected,
        AvailableMove,
        AvailableTake,
        LastMove,
        Check
    }
}