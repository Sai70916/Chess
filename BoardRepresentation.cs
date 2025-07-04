using System.Drawing.Drawing2D;

namespace Chess
{
    public class BoardRepresentation
    {
        public string fileNames = "abcdefgh";
        public string rankNames = "12345678";

        // Make sure the main squares have indexes, but not all squares, since we can just 
        // do this, int squareIndex = rankIndex * 8 + fileIndex
        public int a1 = 0;
        public int b1 = 1;
        public int c1 = 2;
        public int d1 = 3;
        public int e1 = 4;
        public int f1 = 5;
        public int g1 = 6;
        public int h1 = 7;

        public int a8 = 56;
        public int b8 = 57;
        public int c8 = 58;
        public int d8 = 59;
        public int e8 = 60;
        public int f8 = 61;
        public int g8 = 62;
        public int h8 = 63;

        // File index between 0-7
        public int FileIndex(int squareIndex)
        {
            // the binary is 7, and so since b2 is 9 or 0b00001001, the reutrn will 
            // be 0b00000001, which is the index of the file
            return squareIndex & 0b00000111;
        }

        // Rank index between 0-7
        public int RankIndex(int squareIndex)
        {
            // Same as squareIndex / 8, for example, b2 is 9 or 0b00001001 and right 
            // shifted 3 times is 0b00000001, which is the index 1
            return squareIndex >> 3;
        }

        public int IndexFromCoord(int fileIndex, int rankIndex)
        {
            return rankIndex * 8 + fileIndex;
        }

        public int IndexFromCoord(Coord coord)
        {
            return IndexFromCoord(coord.fileIndex, coord.rankIndex);
        }

        public Coord CoordFromIndex(int fileIndex, int rankIndex)
        {
            return new Coord(fileIndex, rankIndex);
        }

        public Coord CoordFromIndex(int squareIndex)
        {
            return new Coord(FileIndex(squareIndex), RankIndex(squareIndex));
        }

        // Check if the square in the chess board is white
        public bool IsWhite(int fileIndex, int rankIndex)
        {
            return (fileIndex + rankIndex) % 2 == 0;
        }

        public string SquareNameFromIndex(int squareIndex)
        {
            return SquareNameFromCoordinate(CoordFromIndex(squareIndex));
        }

        public string SquareNameFromCoordinate(Coord coord)
        {
            return SquareNameFromCoordinate(coord.fileIndex, coord.rankIndex);
        }

        public string SquareNameFromCoordinate(int fileIndex, int rankIndex)
        {
            return fileNames[fileIndex] + "" + rankNames[rankIndex];
        }
    }
}