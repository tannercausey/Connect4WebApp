using System;
using System.Data.SQLite;
using api.Models;
using api.Models.Interfaces;

namespace api.Database.ConnectFour {
	public class ReadBoardData : IGetBoard {
		public static string cs = @"URI=file:Data/ConnectFour.db";
		public Board GetBoard(int GameID) {
			using var connection = new SQLiteConnection(cs);
			connection.Open();
			string query = "select * from Piece where GameID = \"" + GameID + "\"";
			using var cmd = new SQLiteCommand(query, connection);
			
			using SQLiteDataReader readBoard = cmd.ExecuteReader();
			
			Board board = new Board();
			if (readBoard.HasRows) {
				while (readBoard.Read()) {
					Piece piece = new Piece(readBoard.GetString(3));
					piece.PieceID = readBoard.GetInt32(0);
					piece.Row = readBoard.GetInt32(1);
					piece.Col = readBoard.GetInt32(2);
					//piece.Color = readBoard.GetString(3);
					piece.GameID = readBoard.GetInt32(4);
					//System.Console.WriteLine(piece);
					board[piece.Row, piece.Col] = piece;
					board.Pieces.Add(piece);
				}
			}
			//foreach (Piece p in board.Pieces) Console.WriteLine(p);
			return board;
		}
	}
}