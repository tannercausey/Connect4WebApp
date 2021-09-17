drop table if exists Piece;
drop table if exists Game;
create table if not exists Piece
(	PieceID	integer primary key	not null,
	Row		int		not null,
	Col		int		not null,
	Color	varchar(7)	null,
	GameID	int		not null,
	foreign key (GameID) references Game (GameID)
	on delete cascade
);

create table if not exists Game
(	GameID	integer primary key not null,
	StartTime	DateTime	null,
	Finished	boolean		null
);