namespace Chess
{
    public class BitBoardUtility
    {

        public Board boardInstance = new Board();

        public (bool isPiecePresent, int piecePresent, bool? isWhite) GetPieceAtSquare(int squareIndex)
        {
            ulong mask = 1UL << squareIndex;
            if ((boardInstance.WhitePawns & mask) != 0) return (true, Piece.Pawn | Piece.White, true);
            if ((boardInstance.WhiteRooks & mask) != 0) return (true, Piece.Rook | Piece.White, true);
            if ((boardInstance.WhiteKnights & mask) != 0) return (true, Piece.Knight | Piece.White, true);
            if ((boardInstance.WhiteBishops & mask) != 0) return (true, Piece.Bishop | Piece.White, true);
            if ((boardInstance.WhiteQueens & mask) != 0) return (true, Piece.Queen | Piece.White, true);
            if ((boardInstance.WhiteKing & mask) != 0) return (true, Piece.King | Piece.White, true);

            if ((boardInstance.BlackPawns & mask) != 0) return (true, Piece.Pawn | Piece.Black, false);
            if ((boardInstance.BlackRooks & mask) != 0) return (true, Piece.Rook | Piece.Black, false);
            if ((boardInstance.BlackKnights & mask) != 0) return (true, Piece.Knight | Piece.Black, false);
            if ((boardInstance.BlackBishops & mask) != 0) return (true, Piece.Bishop | Piece.Black, false);
            if ((boardInstance.BlackQueens & mask) != 0) return (true, Piece.Queen | Piece.Black, false);
            if ((boardInstance.BlackKing & mask) != 0) return (true, Piece.King | Piece.Black, false);

            return (false, Piece.None, null);
        }
    }
}