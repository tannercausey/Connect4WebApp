using System;
using System.Data.SQLite;
using api.Models;
using api.Models.Interfaces;

namespace api.Database.ConnectFour {
	public class ReadGameData : IGetGame {
		public static string cs = @"URI=file:Data/ConnectFour.db";

		public Game GetGame(int GameID) {
			using var connection = new SQLiteConnection(cs);
			connection.Open();
			string query = "select * from Game where GameID = \"" + GameID + "\"";
			using var cmd = new SQLiteCommand(query, connection);
			
			using SQLiteDataReader readGame = cmd.ExecuteReader();
			
			Game game = new Game();
			if (readGame.HasRows) {
				readGame.Read();
				game.GameID = readGame.GetInt32(0);
				//game.StartTime = (readGame.IsDBNull(1)) ? DateTime.Now : readGame.GetDateTime(1);
				game.Finished = readGame.GetBoolean(2);
			}
			return game;
		}
	}
}