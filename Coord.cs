namespace Chess
{
    public struct Coord
    {
        public readonly int fileIndex;
        public readonly int rankIndex;

        public BoardRepresentation boardRepresentationInstance = new BoardRepresentation();

        public Coord(int fileIndex, int rankIndex)
        {
            this.fileIndex = fileIndex;
            this.rankIndex = rankIndex;
        }

        public Coord(int squareIndex)
        {
            fileIndex = boardRepresentationInstance.FileIndex(squareIndex);
            rankIndex = boardRepresentationInstance.RankIndex(squareIndex);
        }
    }
}