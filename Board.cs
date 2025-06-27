namespace Chess
{
    using System.Collections.Generic;
    public static class Board
    {
        public static int[] Square;

        static Board()
        {
            Square = new int[64];
        }
    }
}