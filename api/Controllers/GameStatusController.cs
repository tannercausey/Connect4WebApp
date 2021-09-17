using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Database.ConnectFour;
using api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class GameStatusController : ControllerBase {
		// GET: api/GameStatus
		/* [EnableCors("AnotherPolicy")]
		[HttpGet]
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		} */

		// GET: api/GameStatus/5
		[EnableCors("AnotherPolicy")]
		[HttpGet("{id}", Name = "GetGameStatus")]
		public string Get(int GameID) {
			Console.WriteLine("Checking Game Over???");
			Board board = new Board(){ReadBehavior = new ReadBoardData()}.ReadBehavior.GetBoard(GameID);
			Console.WriteLine("Board is :\n" + board.ToString());
			string x = new Game(){Board = board}.GameOver().ToString();
			x = "{\"value\":" + "\"" + x + "\"}";	// turn to json
			Console.WriteLine(x);
			return x;
		}

		// POST: api/GameStatus
		[EnableCors("AnotherPolicy")]
		[HttpPost]
		public int Post() {	// create new game and return its id to the frontend
			int newID = new CreateGameData().CreateGame();
			Console.WriteLine($"newID is {newID}");
			return newID;
		}

		// PUT: api/GameStatus/5
		[EnableCors("AnotherPolicy")]
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value) {
			
		}

		// DELETE: api/GameStatus/5
		[EnableCors("AnotherPolicy")]
		[HttpDelete("{id}")]
		public void Delete([FromBody] int gameID) {
			Console.WriteLine($"delete game {gameID}");
			new DeleteGameData().DeleteGame(gameID);
		}
	}
}
