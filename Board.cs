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

            // Load the pieces in a array and draw the pieces to the board??
            for (int squareIndex = 0; squareIndex < 64; squareIndex++)
            {
                var piece = loadedPositon.squares[squareIndex];
                Square[squareIndex] = piece;

                if (piece != Piece.None)
                {
                    var pieceType = Piece.PieceType(piece);
                    var pieceColor = (Piece.IsColor(piece, Piece.White)) ? Piece.White : Piece.Black;
                }
            }
        }

        static void Initialize()
        {
            Square = new int[64];
        }
    }
}