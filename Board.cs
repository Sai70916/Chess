namespace Chess
{
    using System.Collections.Generic;
    public static class Board
    {
        // --BIT BOARDS--
        // White bitboards
        public static ulong WhitePawns;
        public static ulong WhiteRooks;
        public static ulong WhiteKnights;
        public static ulong WhiteBishops;
        public static ulong WhiteQueens;
        public static ulong WhiteKing;
        // Black bitboards
        public static ulong BlackPawns;
        public static ulong BlackRooks;
        public static ulong BlackKnights;
        public static ulong BlackBishops;
        public static ulong BlackQueens;
        public static ulong BlackKing;
        // Grouped piece bitsboard
        public static ulong WhitePieces => WhitePawns | WhiteRooks | WhiteKnights | WhiteBishops | WhiteQueens | WhiteKing;
        public static ulong BlackPieces => BlackPawns | BlackRooks | BlackKnights | BlackBishops | BlackQueens | BlackKing;
        public static ulong AllPieces => WhitePieces | BlackPieces;


        public static void LoadStartPosition()
        {
            LoadPosition(FenUtility.startingFen);
        }

        public static void LoadPosition(string fen)
        {
            Initialize();
            // Get the position info
            var loadedPositon = FenUtility.PositionFromFen(fen);

            // Load the pieces in a array
            // Use 63 and check if equal to zero since it is 0-indexed
            for (int squareIndex = 0; squareIndex < 64; squareIndex++)
            {
                var piece = loadedPositon.squares[squareIndex];
                // Check if the square is empty since any empty spaces should be 0 so do not change anything then
                if (piece == Piece.None) continue;

                // this make a u long with just a single 1. Where the one is is based on 
                // the number next to the left shift operator <<. If its 3, the ulong 
                // will be a set of 63 zeroes but with a 1 three places to the left of 
                // the end of the ulong, so 000...1000. This with the && operator 
                // can assign a single bit to a bit board 
                ulong mask = 1UL << squareIndex;
                bool isWhite = Piece.IsColor(piece, Piece.White);
                int type = Piece.GetPieceType(piece);

                if (isWhite)
                {
                    switch (type)
                    {
                        case Piece.Pawn: WhitePawns |= mask; break;
                        case Piece.Rook: WhiteRooks |= mask; break;
                        case Piece.Knight: WhiteKnights |= mask; break;
                        case Piece.Bishop: WhiteBishops |= mask; break;
                        case Piece.Queen: WhiteQueens |= mask; break;
                        case Piece.King: WhiteKing |= mask; break;
                    }
                }
                else
                {
                    switch (type)
                    {
                        case Piece.Pawn: BlackPawns |= mask; break;
                        case Piece.Rook: BlackRooks |= mask; break;
                        case Piece.Knight: BlackKnights |= mask; break;
                        case Piece.Bishop: BlackBishops |= mask; break;
                        case Piece.Queen: BlackQueens |= mask; break;
                        case Piece.King: BlackKing |= mask; break;
                    }
                }
            }
        }


        static void Initialize()
        {
            WhitePawns = WhiteRooks = WhiteKnights = WhiteBishops = WhiteQueens = WhiteKing = 0;
            BlackPawns = BlackRooks = BlackKnights = BlackBishops = BlackQueens = BlackKing = 0;
        }
    }
}