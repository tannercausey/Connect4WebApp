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
	public class ConnectFourController : ControllerBase {
		// GET: api/ConnectFour
		/*[EnableCors("AnotherPolicy")]
		[HttpGet]
		public GameBoard Get() {
			return new Board() {ReadBehavior = new ReadBoardData()}.ReadBehavior.GetBoard();
		} */

		// GET: api/ConnectFour/5
		[EnableCors("AnotherPolicy")]
		[HttpGet("{GameID}", Name = "GetGamePieces")]
		public List<Piece> Get(int GameID) {
			List <Piece> list = (new Board() {ReadBehavior = new ReadBoardData()}.ReadBehavior.GetBoard(GameID)).Pieces;
			//foreach (Piece p in list) Console.WriteLine(p.ToString());
			return list;
		}

		// POST: api/ConnectFour
		[EnableCors("AnotherPolicy")]
		[HttpPost]
		public void Post([FromBody] Piece piece) {
			new Board() {SaveBehavior = new CreateBoardData()}.SaveBehavior.InsertPiece(piece);
		}

		// PUT: api/ConnectFour/5
		[EnableCors("AnotherPolicy")]
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value) {
		}

		// DELETE: api/ConnectFour/5
		[EnableCors("AnotherPolicy")]
		[HttpDelete("{id}")]
		public void Delete(int id) {
		}
	}
}
