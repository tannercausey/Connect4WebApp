namespace api.Models.Interfaces {
	public interface IIndexable<Piece> {
		Piece this[int x, int y] { get; set; }
	}
}