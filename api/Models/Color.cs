namespace api.Models {
	public enum Color {	White, Red, Black }
	public static class ColorExtensions {
		public static string White = "White";
		public static string Red = "Red";
		public static string Black = "Black";
		
		public static string ToString(this Color color) {
			switch (color) {
				case Color.Black:	return Black;
				case Color.Red:		return Red;
				default:	return White;
			}
		}
	}
}