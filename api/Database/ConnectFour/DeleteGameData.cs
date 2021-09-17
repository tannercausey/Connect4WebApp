using System.Data.SQLite;
using api.Models.Interfaces;

namespace api.Database.ConnectFour {
	public class DeleteGameData : IDeleteGame {
		public static string cs = @"URI=file:Data/ConnectFour.db";

		public void DeleteGame(int gameID) {
			using var connection = new SQLiteConnection(cs);
			connection.Open();
			
			using var cmd = new SQLiteCommand(connection);
			cmd.CommandText = @"delete from Game where GameID = @gameid";	// delete game by ID
			cmd.Parameters.AddWithValue("@gameid", gameID);

			cmd.Prepare();
			cmd.ExecuteNonQuery();
		}
	}
}