using System.Drawing.Drawing2D;

namespace Chess
{
    public static class BoardRepresentation
    {
        public static string fileNames = "abcdefgh";
        public static string rankNames = "12345678";

        // Make sure the main squares have indexes, but not all squares, since we can just 
        // do this, int squareIndex = rankIndex * 8 + fileIndex
        public static int a1 = 0;
        public static int b1 = 1;
        public static int c1 = 2;
        public static int d1 = 3;
        public static int e1 = 4;
        public static int f1 = 5;
        public static int g1 = 6;
        public static int h1 = 7;

        public static int a8 = 56;
        public static int b8 = 57;
        public static int c8 = 58;
        public static int d8 = 59;
        public static int e8 = 60;
        public static int f8 = 61;
        public static int g8 = 62;
        public static int h8 = 63;

        // File index between 0-7
        public static int FileIndex(int squareIndex)
        {
            // the binary is 7, and so since b2 is 9 or 0b00001001, the reutrn will 
            // be 0b00000001, which is the index of the file
            return squareIndex & 0b00000111;
        }

        // Rank index between 0-7
        public static int RankIndex(int squareIndex)
        {
            // Same as squareIndex / 8, for example, b2 is 9 or 0b00001001 and right 
            // shifted 3 times is 0b00000001, which is the index 1
            return squareIndex >> 3;
        }

        public static int IndexFromCoord(int fileIndex, int rankIndex)
        {
            return rankIndex * 8 + fileIndex;
        }

        public static int IndexFromCoord(Coord coord)
        {
            return IndexFromCoord(coord.fileIndex, coord.rankIndex);
        }

        public static Coord CoordFromIndex(int fileIndex, int rankIndex)
        {
            return new Coord(fileIndex, rankIndex);
        }

        public static Coord CoordFromIndex(int squareIndex)
        {
            return new Coord(FileIndex(squareIndex), RankIndex(squareIndex));
        }

        // Check if the square in the chess board is white
        public static bool IsWhite(int fileIndex, int rankIndex)
        {
            return (fileIndex + rankIndex) % 2 == 0;
        }

        public static string SquareNameFromIndex(int squareIndex)
        {
            return SquareNameFromCoordinate(CoordFromIndex(squareIndex));
        }

        public static string SquareNameFromCoordinate(Coord coord)
        {
            return SquareNameFromCoordinate(coord.fileIndex, coord.rankIndex);
        }

        public static string SquareNameFromCoordinate(int fileIndex, int rankIndex)
        {
            return fileNames[fileIndex] + "" + rankNames[rankIndex];
        }
    }
}