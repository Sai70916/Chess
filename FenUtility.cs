namespace Chess
{
    public static class FenUtility
    {
        // Dictionary for converting the letters in fen to piece names
        static Dictionary<char, int> pieceTypeFromSymbol = new Dictionary<char, int>()
        {
            ['k'] = Piece.King,
            ['p'] = Piece.Pawn,
            ['r'] = Piece.Rook,
            ['n'] = Piece.Knight,
            ['b'] = Piece.Bishop,
            ['q'] = Piece.Queen
        };

        // Board-Position To-Move(lowercase) Castling-Rights En-Passant-Target-Square Half-Move-Count Full-Move-Count
        public const string startingFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq -- 0 1";

        // Load the postion from a fen, the return value is a new board
        public static LoadedPositionInfo PositionFromFen(string fen)
        {
            LoadedPositionInfo loadedPositionInfo = new LoadedPositionInfo();
            string[] sections = fen.Split(" "); // Seperate the fen into a list

            int file = 0;
            int rank = 7;

            foreach (char symbol in sections[0])
            {
                if (symbol == '/')
                {
                    file = 0;
                    rank--;
                }
                else
                {
                    if (char.IsDigit(symbol))
                    {
                        file += (int)char.GetNumericValue(symbol);
                    }
                    else
                    {
                        int pieceColor = (char.IsUpper(symbol)) ? Piece.White : Piece.Black;
                        int pieceType = pieceTypeFromSymbol[char.ToLower(symbol)];
                        // We go fron top to bottom, so rank when zero indexed is 7, evertime we reach down, 
                        // remove 1 rank, so rank * 8 gives us the row from top
                        loadedPositionInfo.squares[rank * 8 + file] = pieceColor | pieceType;
                        file++;
                    }
                }
            }

            loadedPositionInfo.whiteToMove = (sections[1] == "w");

            // Get the string of letter(s) that show the castling right directly from the
            // fen string, but only if provided, i.e. the fen has more than just 
            // 1) Board-Postion and 2) Color-To-Move
            string castlingRights = (sections.Length > 2) ? sections[2] : "KQkq";
            loadedPositionInfo.whiteCastleKingside = castlingRights.Contains("K"); // No conversion, so can treat as string not char
            loadedPositionInfo.whiteCastleQueenside = castlingRights.Contains("Q");
            loadedPositionInfo.blackCastleKingside = castlingRights.Contains("k");
            loadedPositionInfo.blackCastleQueenside = castlingRights.Contains("q");

            if (sections.Length > 3 && sections[3] != "--")
            {
                char enPassantFileName = sections[3][0]; // We dont need rank
                if (BoardRepresentation.fileNames.Contains(enPassantFileName))
                {
                    loadedPositionInfo.epFile = BoardRepresentation.fileNames.IndexOf(enPassantFileName);
                }

            }

            // Ply moves, half clock moves
            if (sections.Length > 4)
            {
                // Try to convert the first parameter into the datatype in the place of Int 
                // at the start, and the second parameter with out makes it so that the 
                // output value if stored in the variable after out
                int.TryParse(sections[4], out loadedPositionInfo.plyCount);
            }

            return loadedPositionInfo;
        }

        public class LoadedPositionInfo
        {
            public int[] squares;
            public bool whiteToMove;
            public int epFile; // En-passant, can just check if for white of black to get the row
            public bool whiteCastleKingside;
            public bool whiteCastleQueenside;
            public bool blackCastleKingside;
            public bool blackCastleQueenside;
            public int plyCount;

            public LoadedPositionInfo()
            {
                squares = new int[64]; //Empty the board to fill in previous functions
            }
        }
    }
}