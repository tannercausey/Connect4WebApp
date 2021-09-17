function play() {
	document.getElementById("displayGameID").innerHTML = "Game ID: " + localStorage.GameID;
	//localStorage.GameID = 1;
	//localStorage.lastTurnColor = "Red"; // this is not technically correct
	getBoard();
}
function getBoard() {
	const boardAPIURL = "https://localhost:5001/api/connectFour/" + localStorage.GameID;
	fetch(boardAPIURL)
	.then(async function(response) {
		console.log("Got board: ");console.log(response);
		var boardString = buildBoard();
		document.getElementById("board").innerHTML = boardString;
		if (response.status == 200) {
			var json = await response.json();
			console.log(json);
			json.forEach((piece) => {
				piece.color = (piece.color == 1) ? "Red" : "Black";
				var className = piece.color.toLowerCase() + "circle";
				//console.log(className);
				document.getElementById("board-col-" + piece.col + "-row-" + piece.row).className = className;
			});
		}
	}).catch(function(error) {
		console.log(error);
	}).then(checkGameOver());
}
function newGame() {
	if (confirm("Are you sure you want to create a new game?\nThis game will be deleted.")) {
		const gameAPIURL = "https://localhost:5001/api/gameStatus/";
		fetch(gameAPIURL + parseInt(localStorage.GameID), {
			method: "DELETE",
			headers: {
				"Accept": 'application/json',
				"Content-Type": 'application/json'
			},
			body: parseInt(localStorage.GameID)
		}).catch(function(error) {
			console.log(error);
		}).then(function(response) {
			console.log(response);
		});
		fetch (gameAPIURL, {
			method: "POST",
			headers: {
				"Accept": 'application/json',
				"Content-Type": 'application/json'
			}
		}).then(async (response) => {
			//console.log(await response.json());
			localStorage.GameID = await response.json();
		}).then(function() {
			localStorage.lastTurnColor = "Red";
			play();
		});
	}
}
function buildBoard() {
	document.getElementById("gameMessage").innerHTML = "<br>" + localStorage.lastTurnColor + "'s turn...";
	var boardString = "<table class = \"table table-borderless\"><thead><tr>";
	var col, row;
	for (col = 0; col < 7; col++) {
		boardString += "<th id = \"board-header-" + col + "\" scope = \"col\">";
		boardString += "<button class = \"btn put-piece\" role = \"button\" onclick = \"postPiece("+col+")\">Place here</button></th>";
	}
	boardString += "</tr></thead>";
	boardString += "<tbody style = \"background-color: yellow\">";
	for (row = 0; row < 6; row++) {
		boardString += "<tr>";
		for (col = 0; col < 7; col++) {
			boardString += "<td><div id = \"board-col-" + col + "-row-" + row + "\" class = \"whitecircle\"></div></td>";
		}
		boardString += "</tr>";
	}
	boardString += "</tbody></table>";
	return boardString;
}
function postPiece(col) {
	var colorInt;
	if (localStorage.lastTurnColor == "Black") colorInt = 2;
	else if (localStorage.lastTurnColor == "Red") colorInt = 1;
	else colorInt = 0;
	const boardAPIURL = "https://localhost:5001/api/connectFour";
	fetch(boardAPIURL, {
		method: "POST",
		headers: {
			"Accept": 'application/json',
			"Content-Type": 'application/json'
		},
		body: JSON.stringify({
		/*	PieceID: autoincrement,
			Row: added by backend	*/
			Col: col,
			Color: colorInt,
			GameID: parseInt(localStorage.GameID)
		})
	}).catch(function(error) {
		console.log(error);
	}).then((response) => {
		console.log("Posted piece"); console.log(response);
		getBoard();
	});
	setTurn();
}
function checkGameOver() {
	console.log("checking winner?");
	const gameStatusAPIURL = "https://localhost:5001/api/gameStatus/" + localStorage.GameID;
	fetch(gameStatusAPIURL)
	.then(async function(response) {
		console.log(response);
		var color = await response.json();
		/* return response.json();
	}).then(function (color) { */
		//let color = response;
		if (!(color.value == "White")) {
			console.log(color.value);
			document.getElementById("gameMessage").innerHTML = "<br><b>" + color.value + " wins!</b>";
			var buttons = document.getElementsByClassName("put-piece");
			for (var i = 0; i < buttons.length; i++) {
				buttons[i].disabled = true;
				buttons[i].classList.add("disabled");
				buttons[i].style.visibility = "hidden";
				document.getElementById("board-header-"+ i).style.display = "none";
			}
			//document.getElementsByTagName("thead").style.display = "none";
		}
	});
}
function setTurn() {
	localStorage.lastTurnColor = (localStorage.lastTurnColor == "Red") ? "Black" : "Red";
}
$(document).ready(function(){
	$('[data-toggle="tooltip"]').tooltip();
});