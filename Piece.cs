namespace Chess
{
    public class Piece
    {
        // This looks weird at first, but it is like this so that whenever looking at the
        //  binary values of the pieces, first 2 digits are either 01 for white or 10 for 
        // a black piece, and the last 3 denote the type of piece
        public const int None = 0; // 0b00000000
        public const int King = 1; // 0b00000001
        public const int Pawn = 2; // 0b00000010
        public const int Knight = 3; // 0b00000011
        public const int Bishop = 5; // Skipped 4 to follow Sebastion Lagues code, he did it to avoid any confusion while making binary operations, so 0b00000101
        public const int Rook = 6; // 0b00000110
        public const int Queen = 7; // 0b00000111

        public const int White = 8; // 0b00001000
        public const int Black = 16; // 0b00100000

        // no public or private since we donts need them outside of here
        const int typeMask = 0b00111;
        const int blackMask = 0b10000;
        const int whiteMask = 0b01000;
        const int colorMask = whiteMask | blackMask; //0b11000

        // Since these are classes, we do not need to make a instance of this class 
        // to use them
        public bool IsColor(int piece, int color)
        {
            // Used to check if a certain piece is a certain color.
            // They and operator only passes 1 if both have a 1 in that spot, so if the 
            // piece is white, it will be 01, and the color mask being 11 means output=01 
            // then you check if the color is equal to the 10 or 01 the user passed.
            return (piece & colorMask) == color;
        }

        public int GetColor(int piece)
        {
            return piece & colorMask;
        }

        public int GetPieceType(int piece)
        {
            return piece & typeMask;
        }

        public string GetPieceSymbol(int piece)
        {
            int type = GetPieceType(piece);
            bool isWhite = IsColor(piece, White);

            return type switch
            {
                Pawn => isWhite ? "P" : "p",
                Rook => isWhite ? "R" : "r",
                Knight => isWhite ? "N" : "n",
                Bishop => isWhite ? "B" : "b",
                Queen => isWhite ? "Q" : "q",
                King => isWhite ? "K" : "k",
                _ => ""
            };
        }
    }
}
