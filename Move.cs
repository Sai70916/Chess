/*
Like we use bitboards to save memory, moves are stored in a 16 bit number

bits 0-5 show the start square
bits 6-11 show the end square
bits 12-15 show a flag to represent different types of moves.
*/
namespace Chess
{
    public readonly struct Move
    {
        public readonly struct Flag
        {
            public const int None = 0; // If its just a normal move
            public const int PawnTwoForward = 1; // If a pawn is moving 2 squares
            public const int EnPassantCapture = 2; // If en passant happened
            public const int WhiteKingsideCastling = 3;
            public const int WhiteQueensideCastling = 4;
            public const int BlackKingsideCastling = 5;
            public const int BlackQueensideCastling = 6;
            public const int PromoteToQueen = 7;
            public const int PromoteToRook = 8;
            public const int PromoteToKnight = 9;
            public const int PromoteToBishop = 10;
        }

        readonly ushort moveValue; // The 16 bits that mean show the move

        const ushort startSquareMask = 0b0000000000111111;
        const ushort targetSquareMask = 0b0000111111000000;
        const ushort flagMask = 0b1111000000000000;

        // Used to create a new move structure
        public Move(ushort moveValue)
        {
            this.moveValue = moveValue;
        }
        // Used to make a move using start and end square
        public Move(ushort startSquare, ushort targetSquare)
        {
            moveValue = (ushort)(startSquare | targetSquare << 6);
        }
        // Used to make a move using start and end squares with flag
        public Move(ushort startSquare, ushort targetSquare, ushort flag)
        {
            moveValue = (ushort)(startSquare | targetSquare << 6 | flag << 12);
        }

        public int StartSquare
        {
            get
            {
                return moveValue & startSquareMask;
            }
        }

        public int TargetSquare
        {
            get
            {
                return (moveValue & targetSquareMask) >> 6;
            }
        }

        public int MoveFlag
        {
            get
            {
                return (moveValue & flagMask) >> 12;
            }
        }

        public bool IsPromotion
        {
            get
            {
                int flag = MoveFlag;
                return flag == Flag.PromoteToQueen || flag == Flag.PromoteToRook ||
                       flag == Flag.PromoteToKnight || flag == Flag.PromoteToBishop;
            }
        }

        public int PromotionPieceType
        {
            get
            {
                switch (MoveFlag)
                {
                    case Flag.PromoteToRook:
                        return Piece.Rook;
                    case Flag.PromoteToKnight:
                        return Piece.Knight;
                    case Flag.PromoteToBishop:
                        return Piece.Bishop;
                    case Flag.PromoteToQueen:
                        return Piece.Queen;
                    default:
                        return Piece.None;
                }
            }
        }

        public int Value
        {
            get
            {
                return moveValue;
            }
        }

        public static bool IsSameMove(Move a, Move b)
        {
            return a.moveValue == b.moveValue;
        }

        public string MoveName
        {
            get
            {
                return BoardRepresentation.SquareNameFromIndex(StartSquare) + "-"
                       + BoardRepresentation.SquareNameFromIndex(TargetSquare);
            }
        }
    }
}