using System;
using System.Data.SQLite;
using api.Models;
using api.Models.Interfaces;

namespace api.Database.ConnectFour {
	public class CreateGameData : ICreateGame {
		public static string cs = @"URI=file:Data/ConnectFour.db";

		public int CreateGame() {
			using var connection = new SQLiteConnection(cs);
			connection.Open();
			
			using var createQuery = new SQLiteCommand(connection);
			createQuery.CommandText = @"insert into Game (StartTime, Finished) values (@starttime, @finished)";
			createQuery.Parameters.AddWithValue("@starttime", DateTime.Now);
			createQuery.Parameters.AddWithValue("@finished", false);
			createQuery.Prepare();
			createQuery.ExecuteNonQuery();
			
			using var getQuery = new SQLiteCommand(connection);
			getQuery.CommandText = @"select last_insert_rowid()";
			Int64 newID = (Int64)getQuery.ExecuteScalar();
			return (int)newID;
		}
	}
}