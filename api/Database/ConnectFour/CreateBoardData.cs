using System;
using System.Data.SQLite;
using api.Models;
using api.Models.Interfaces;

namespace api.Database.ConnectFour {
	public class CreateBoardData : IInsertPiece {
		public static string cs = @"URI=file:Data/ConnectFour.db";
		public void InsertPiece(Piece piece) { // what do we need here to send complete data to database
			Board Board = new Board() {ReadBehavior = new ReadBoardData()}.ReadBehavior.GetBoard(piece.GameID);
			piece.Row = Board.GetAvailableRow(piece.Col);
			if (piece.Row == -1) throw new Exception();
			using var connection = new SQLiteConnection(cs);
			connection.Open();
			
			using var query = new SQLiteCommand(connection);
			query.CommandText = @"insert into Piece (Row, Col, Color, GameID) values (@row, @col, @color, @gameid)";
			query.Parameters.AddWithValue("@row", piece.Row);
			query.Parameters.AddWithValue("@col", piece.Col);
			query.Parameters.AddWithValue("@color", piece.Color.ToString());
			query.Parameters.AddWithValue("@gameid", piece.GameID);
			query.Prepare();
			Console.WriteLine(piece.ToString());
			//Console.WriteLine($"about to post piece at ({piece.Row}, {piece.Col})");
			//return;
			query.ExecuteNonQuery();
		}
	}
}