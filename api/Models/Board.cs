using System.Collections.Generic;
using api.Models.Interfaces;

namespace api.Models {
	public class Board : IIndexable<Piece> {
		public int GameID { get; set; }
		public readonly int rows = 6;
		public readonly int cols = 7;
		private Piece[,] _index { get; set;}
		public List<Piece> Pieces { get; set; }
		public IGetBoard ReadBehavior { get; set; }
		public IInsertPiece SaveBehavior { get; set; }
		
		public Board() {
			_index = new Piece[rows, cols];
			Pieces = new List<Piece>();
			for (int row = 0; row < rows; row++) {
				for (int col = 0; col < cols; col++) {
					_index[row,col] = new Piece();
				}
			}
		}
		public void Insert(Piece piece, int col) {
			for (int i = rows-1; i >= 0; i--) {	// iterate up column col
				if (_index[i, col].Color == Color.White) {	// if you found an empty spot
					_index[i, col] = piece;	// place piece
					return;
				}
			}
		}
		public int GetAvailableRow(int col) {
			for (int row = rows-1; row >= 0; row--) {
				if (_index[row, col].Color == Color.White) {
					System.Console.WriteLine($"available row in col {col} is row {row}");
					return row;
				}
			}
			return -1;
		}
		public bool ValidCol(int col) {
			if (col >= cols) return false;
			else if (_index[0, col].Color == Color.White) return true;
			else return false;
		}
		public override string ToString() {
			string colors = string.Empty;
			for (int row = 0; row < rows; row++) {
				for (int col = 0; col < cols; col++) {
					colors += _index[row, col].ToChar(justColor: true);
				}
				colors += '\n';
			}
			return colors;
		}
		public Piece this[int x, int y] { get => _index[x, y]; set => _index[x, y] = value;}
	}
}