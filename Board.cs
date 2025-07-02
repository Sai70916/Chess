namespace Chess
{
    using System.Collections.Generic;
    public static class Board
    {
        public static int[] Square = new int[64];

        public static void LoadStartPosition()
        {
            LoadPosition(FenUtility.startingFen);
        }

        public static void LoadPosition(string fen)
        {
            Initialize();
            var loadedPositon = FenUtility.PositionFromFen(fen);

            // Load the pieces in a array
            // Use 63 and check if equal to zero since it is 0-indexed
            for (int squareIndex = 63; squareIndex >= 0; squareIndex--)
            {
                var piece = loadedPositon.squares[squareIndex];
                Square[squareIndex] = piece;

                if (piece != Piece.None)
                {
                    var pieceType = Piece.GetPieceType(piece);
                    var pieceColor = (Piece.IsColor(piece, Piece.White)) ? Piece.White : Piece.Black;
                }
            }
        }

        static void Initialize()
        {
            Array.Fill(Square, Piece.None);
        }

        public static bool SquareContainsPiece(int row, int col)
        {
            return Square[row * 8 + col] != Piece.None;
        }
    }
}