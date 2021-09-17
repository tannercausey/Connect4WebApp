using System;
namespace api.Models {
	public class Piece {
		public int PieceID { get; set; }
		public int Row { get; set; }
		public int Col { get; set; }
		public Color Color { get; set; }
		public int GameID { get; set; }
		public Piece() {	Color = Color.White;	}
		public Piece(string color) {
			if (color == "Black")	Color = Color.Black;
			else if (color == "Red")	Color = Color.Red;
			else Color = Color.White;
			switch (color) {
				case "Black":	Color = Color.Black; break;
				case "Red":		Color = Color.Red; break;
				default:	Color = Color.White; break;
			}
		}
		public Piece(Color color, int row, int col) {
			Color = color; Row = row; Col = col;
		}
		public override string ToString() {
			return Color.ToString() + " col: " + Col + " row: " + Row + '\t';
		}
		public char ToChar(bool justColor) {
			return Color.ToString()[0];
		}
		public static bool operator == (Piece p1, Piece p2) {
			if (p1.Color == p2.Color)
				return true;
			else return false;
		}
		public static bool operator != (Piece p1, Piece p2) {
			if (p1.Color == p2.Color)
				return false;
			else return true;
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(this, obj))	{	return true;	}
			if (ReferenceEquals(obj, null))	{	return false;	}
			throw new NotImplementedException();
		}
		public override int GetHashCode() {	throw new NotImplementedException();	}
	}
}