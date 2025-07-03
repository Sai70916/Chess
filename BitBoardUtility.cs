namespace Chess
{
    public static class BitBoardUtility
    {
        public static (bool isPiecePresent, int piecePresent, bool? isWhite) GetPieceAtSquare(int squareIndex)
        {
            ulong mask = 1UL << squareIndex;
            if ((Board.WhitePawns & mask) != 0) return (true, Piece.Pawn | Piece.White, true);
            if ((Board.WhiteRooks & mask) != 0) return (true, Piece.Rook | Piece.White, true);
            if ((Board.WhiteKnights & mask) != 0) return (true, Piece.Knight | Piece.White, true);
            if ((Board.WhiteBishops & mask) != 0) return (true, Piece.Bishop | Piece.White, true);
            if ((Board.WhiteQueens & mask) != 0) return (true, Piece.Queen | Piece.White, true);
            if ((Board.WhiteKing & mask) != 0) return (true, Piece.King | Piece.White, true);

            if ((Board.BlackPawns & mask) != 0) return (true, Piece.Pawn | Piece.Black, false);
            if ((Board.BlackRooks & mask) != 0) return (true, Piece.Rook | Piece.Black, false);
            if ((Board.BlackKnights & mask) != 0) return (true, Piece.Knight | Piece.Black, false);
            if ((Board.BlackBishops & mask) != 0) return (true, Piece.Bishop | Piece.Black, false);
            if ((Board.BlackQueens & mask) != 0) return (true, Piece.Queen | Piece.Black, false);
            if ((Board.BlackKing & mask) != 0) return (true, Piece.King | Piece.Black, false);

            return (false, Piece.None, null);
        }
    }
}