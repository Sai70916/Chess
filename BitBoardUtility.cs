namespace Chess
{
    using System.Diagnostics;
    public static class BitBoardUtility
    {
        public static (bool piecePresent, int pieceType, bool? isWhite) GetPieceAtSquare(int squareIndex)
        {
            ulong mask = 1UL << squareIndex;
            Debug.WriteLine($"Reached");
            if ((Board.WhitePawns & mask) != 0) return (true, Piece.Pawn, true);
            if ((Board.WhiteRooks & mask) != 0) return (true, Piece.Rook, true);
            if ((Board.WhiteKnights & mask) != 0) return (true, Piece.Knight, true);
            if ((Board.WhiteBishops & mask) != 0) return (true, Piece.Bishop, true);
            if ((Board.WhiteQueens & mask) != 0) return (true, Piece.Queen, true);
            if ((Board.WhiteKing & mask) != 0) return (true, Piece.King, true);

            if ((Board.BlackPawns & mask) != 0) return (true, Piece.Pawn, false);
            if ((Board.BlackRooks & mask) != 0) return (true, Piece.Rook, false);
            if ((Board.BlackKnights & mask) != 0) return (true, Piece.Knight, false);
            if ((Board.BlackBishops & mask) != 0) return (true, Piece.Bishop, false);
            if ((Board.BlackQueens & mask) != 0) return (true, Piece.Queen, false);
            if ((Board.BlackKing & mask) != 0) return (true, Piece.King, false);

            return (false, Piece.None, null);
        }
    }
}