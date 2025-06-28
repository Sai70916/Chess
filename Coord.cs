namespace Chess
{
    public struct Coord
    {
        public readonly int fileIndex;
        public readonly int rankIndex;

        public Coord(int fileIndex, int rankIndex)
        {
            this.fileIndex = fileIndex;
            this.rankIndex = rankIndex;
        }

        public Coord(int squareIndex)
        {
            fileIndex = BoardRepresentation.FileIndex(squareIndex);
            rankIndex = BoardRepresentation.RankIndex(squareIndex);
        }
    }
}