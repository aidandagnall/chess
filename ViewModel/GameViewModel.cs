using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Controls;
using System;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using Chess.Model;
using Chess.Model.Pieces;
namespace Chess.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        public string str {get; set;}
        private ObservableCollection<Square> _Squares;
        public ObservableCollection<Square> Squares 
        {
            get { return _Squares; }
            set {_Squares = value;}
        }
        public Command<Square> SquareClickCommand {get; set;}
        private Game GameModel;
        private Square _selectedSquare;
        public Square SelectedSquare
        {
            get { return _selectedSquare; }
            set { _selectedSquare = value; }
        }
        
        public GameViewModel()
        {
            GameModel = new Game();
            Squares = new ObservableCollection<Square>(GameModel.Board as List<Square>);
            SquareClickCommand = new Command<Square>(OnSquareClick);
        }

        private void OnSquareClick(Square square)
        {
            Select(square);
        }

        private void Select(Square square)
        {
            foreach(Square s in Squares)
                if (s.SquareHighlight != Highlight.LastMove && s.SquareHighlight != Highlight.Check)
                    s.SquareHighlight = Highlight.None;
            if (!square.ContainsPiece)
                return;
            SelectedSquare = square;
            SelectedSquare.SquareHighlight = Highlight.Selected;
            foreach(Square s in Squares)
                if (GameModel.IsMoveValid(SelectedSquare, s))
                    if (s.ContainsPiece)
                        s.SquareHighlight = Highlight.AvailableMove;
                    else
                        s.SquareHighlight = Highlight.AvailableTake;
        }

    }

    public class Command<T> : ICommand
    {
        public Action<T> Action {get; set;}

        public void Execute(object p)
        {
            if (Action != null && p is T)
                Action((T)p);
        }

        public bool CanExecute(object p) { return isEnabled; }

        private bool _isEnabled = true;
        public bool isEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                if(CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler CanExecuteChanged;

        public Command(Action<T> action) { Action = action; }
    }

}