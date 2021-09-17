using System;
using api.Database.ConnectFour;
using api.Models.Interfaces;

namespace api.Models {
	public class Game {
		public int GameID { get; set; }
		public DateTime StartTime { get; set;}
		public bool Finished { get; set; }
		public Board Board { get; set; }
		public ICreateGame CreateBehavior { get; set; }
		public IGetGame ReadBehavior { get; set; }
		public IDeleteGame DeleteBehavior { get; set; }

		public Game() {
			Board = new Board();
			Finished = false;
		}
		public int GetAvailableRow(int col) {
			for (int row = Board.rows-1; row >= 0; row--) {
				if (Board[row, col].Color == Color.White) {
					System.Console.WriteLine($"available row in col {col} is row {row}");
					return row;
				}
			}
			return -1;
		}
		public Color GameOver() {
			Board.ReadBehavior = new ReadBoardData();
			Board.ReadBehavior.GetBoard(GameID);
			for (int i = 0; i < Board.rows; i++) {
				for (int j = 0; j <= Board.cols-4; j++) {
					if (Board[i,j].Color == Color.White)
						continue;
					else if (Board[i,j].Color == Board[i,j+1].Color	// horizontal
							&& Board[i,j].Color == Board[i,j+2].Color
							&& Board[i,j].Color == Board[i,j+3].Color) {
						Finished = true;
						return Board[i,j].Color;
					}
				}
			}
			for (int i = 0; i <= Board.rows-4; i++) {
				for (int j = 0; j < Board.cols; j++) {
					if (Board[i,j].Color == Color.White)
						continue;
					else if (Board[i,j].Color == Board[i+1,j].Color	// vertical
							&& Board[i,j].Color == Board[i+2,j].Color
							&& Board[i,j].Color == Board[i+3,j].Color) {
						Finished = true;
						return Board[i,j].Color;
					}
				}
			}
			//Console.WriteLine("vertical/horizontal test passed");
			for (int i = 0; i <= Board.rows-4; i++) {
				for (int j = 0; j <= Board.cols-4; j++) {
					if (Board[i,j].Color == Color.White)
						continue;
					else if (Board[i,j].Color == Board[i+1, j+1].Color	// top left to bottom right
							&& Board[i,j].Color == Board[i+2, j+2].Color
							&& Board[i,j].Color == Board[i+3, j+3].Color) {
						Finished = true;
						return Board[i,j].Color;
					}
				}
			}
			for (int i = 0; i <= Board.rows-4; i++) {
				for (int j = 3; j < Board.cols; j++) {
					if (Board[i,j].Color == Color.White)
						continue;
					else if (Board[i,j].Color == Board[i+1, j-1].Color	// top right to bottom left
							&& Board[i,j].Color == Board[i+2, j-2].Color
							&& Board[i,j].Color == Board[i+3, j-3].Color) {
						Finished = true;
						return Board[i,j].Color;
					}
				}
			}
			return Color.White;
		}
	}
}