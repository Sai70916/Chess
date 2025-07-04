namespace Chess
{
    using System.Diagnostics;

    public class Board
    {
        // Array of the board, used for easy lookup and easier to use when rendering the board
        public int[] Squares = new int[64];

        // --BIT BOARDS--
        // Representation of the board in binary, a bit is flipped on if piece is present 
        // with each bit representing a square
        // Use them for move gen, legal moves gen and for bitwise operations
        // White bitboards
        public ulong WhitePawns;
        public ulong WhiteRooks;
        public ulong WhiteKnights;
        public ulong WhiteBishops;
        public ulong WhiteQueens;
        public ulong WhiteKing;
        // Black bitboards
        public ulong BlackPawns;
        public ulong BlackRooks;
        public ulong BlackKnights;
        public ulong BlackBishops;
        public ulong BlackQueens;
        public ulong BlackKing;
        // Grouped piece bitsboard
        public ulong WhitePieces => WhitePawns | WhiteRooks | WhiteKnights | WhiteBishops | WhiteQueens | WhiteKing;
        public ulong BlackPieces => BlackPawns | BlackRooks | BlackKnights | BlackBishops | BlackQueens | BlackKing;
        public ulong AllPieces => WhitePieces | BlackPieces;
        // The comma inbetween the brackets shows its a multi dimensional array, and each 
        // row thats 0 or 1 contains white or black boards, and each color has 6 boards
        public ulong[,] BitBoards = new ulong[2, 8];
        // This is an empty bitboard. Used in Initialize() and MakeMove()
        private ulong EmptyBitBoard;

        // Turn related variables
        public bool whiteToMove;

        public FenUtility fenUtilityInstance = new FenUtility();
        public Piece pieceInstance = new Piece();

        public void LoadStartPosition()
        {
            LoadPosition(FenUtility.startingFen);
        }

        public void LoadPosition(string fen)
        {
            Initialize();
            // Get the position info
            var loadedPositon = fenUtilityInstance.PositionFromFen(fen);

            // Load the pieces in a array
            // Use 63 and check if equal to zero since it is 0-indexed
            for (int squareIndex = 0; squareIndex < 64; squareIndex++)
            {
                var piece = loadedPositon.squares[squareIndex];
                // Check if the square is empty since any empty spaces should be 0 so do not change anything then
                if (piece == Piece.None) continue;

                // Update the Squares array along with the bitboards
                Squares[squareIndex] = piece;

                // This makes a u long with just a single 1. Where the one is is based on 
                // the number next to the left shift operator <<. If its 3, the ulong 
                // will be a set of 63 zeroes but with a 1 three places to the left of 
                // the end of the ulong, so 000...1000. This with the && operator 
                // can assign a single bit to a bit board 
                ulong mask = 1UL << squareIndex;
                bool isWhite = pieceInstance.IsColor(piece, Piece.White);
                int type = pieceInstance.GetPieceType(piece);

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

        void Initialize()
        {
            // Clear the board representation
            Array.Fill(Squares, Piece.None);
            WhitePawns = WhiteRooks = WhiteKnights = WhiteBishops = WhiteQueens = WhiteKing = 0;
            BlackPawns = BlackRooks = BlackKnights = BlackBishops = BlackQueens = BlackKing = 0;
            EmptyBitBoard = 0b000000;

            // Put all the bitboards in a list of bitboards
            BitBoards = new ulong[2, 8];
            BitBoards[0, 0] = EmptyBitBoard;
            BitBoards[0, 1] = WhiteKing;
            BitBoards[0, 2] = WhitePawns;
            BitBoards[0, 3] = WhiteKnights;
            BitBoards[0, 4] = EmptyBitBoard;
            BitBoards[0, 5] = WhiteBishops;
            BitBoards[0, 6] = WhiteRooks;
            BitBoards[0, 7] = WhiteQueens;
            BitBoards[1, 1] = BlackKing;
            BitBoards[1, 2] = BlackPawns;
            BitBoards[1, 3] = BlackKnights;
            BitBoards[1, 4] = EmptyBitBoard;
            BitBoards[1, 5] = BlackBishops;
            BitBoards[1, 6] = BlackRooks;
            BitBoards[1, 7] = BlackQueens;
        }

        public void MakeMove(Move move)
        {
            int fromSquare = move.StartSquare;
            int toSquare = move.TargetSquare;
            int movingPiece = Squares[fromSquare];
            int capturedPiece = Squares[toSquare];

            // Update the array
            Squares[fromSquare] = Piece.None;
            Squares[toSquare] = movingPiece;

            // Update the bitboards
            ulong toMask = 1UL << toSquare; // We want to remove this bit
            ulong fromMask = 1UL << fromSquare; // We want to add/replace this bit

            int pieceType = pieceInstance.GetPieceType(movingPiece);
            bool isWhite = pieceInstance.IsColor(movingPiece, Piece.White);
            int colorIndex = isWhite ? 0 : 1;

            // Remove the single start square bit from the right bitboard
            BitBoards[colorIndex, pieceType] &= ~fromMask;
            // Add/Replace the single end sqaure bit from the right bitboard
            BitBoards[colorIndex, pieceType] |= toMask;

            // Remove the captured piece from its bit board
            if (capturedPiece != Piece.None)
            {
                int capturedType = pieceInstance.GetPieceType(capturedPiece);
                bool capturedIsWhite = pieceInstance.IsColor(capturedPiece, Piece.White);
                int capturedColorIndex = capturedIsWhite ? 0 : 1;
                Debug.WriteLine($"Captured piece type: {capturedType}, captured color is white: {capturedIsWhite}, captured color index: {capturedColorIndex}");

                BitBoards[capturedColorIndex, capturedType] &= ~toMask;
            }
        }
    }
}