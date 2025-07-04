namespace Chess
{
    public partial class Form1 : Form
    {
        private int windowHeight;
        private int windowWidth;

        private const int BoardRows = 8;
        private int tileSize;
        private int startX;
        private int startY;
        private int boardSize;

        private int? selectedRow;
        private int? selectedCol;
        private int[]? selectedPieceCoords = new int[2];
        private bool isPieceSelected = false;

        private Dictionary<string, Image> pieceImages = new();

        public Label testingLabel = new Label();

        public Board boardInstance = new Board();
        public Piece pieceInstance = new Piece();

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Makes the flickering when redrawing the board go away
            LoadPieceImages();
            boardInstance.LoadStartPosition();

            this.BackColor = Color.FromArgb(50, 50, 50);
            this.WindowState = FormWindowState.Maximized; // Make sure the window is maximized
            this.StartPosition = FormStartPosition.CenterScreen; // Make the window start at the center of the screen

            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Make sure that window is not resized
            this.MaximizeBox = false; // Disable maximize button

            this.MouseDown += new MouseEventHandler(Form1_MouseDown);// Dectect mouse click

            windowWidth = this.ClientSize.Width;
            windowHeight = this.ClientSize.Height;

            // Debug
            // Delete
            testingLabel.Text = "Text";
            testingLabel.Location = new Point(20, 20);
            testingLabel.AutoSize = true;
            testingLabel.ForeColor = Color.Black;
            testingLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            this.Controls.Add(testingLabel);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Update the window size every it paints (every frame)
            windowWidth = this.ClientSize.Width;
            windowHeight = this.ClientSize.Height;

            tileSize = 80;
            boardSize = BoardRows * tileSize;
            // variables for the coords where the board starts (top and left margin)
            startX = (windowWidth - boardSize) / 2;
            startY = (windowHeight - boardSize) / 2;



            Graphics g = e.Graphics; // kind of like a python class, var e with the type of graphics has all the properties of the built in Graphics which are stored in e in the parameter

            // recursively draw the squares
            for (int row = 0; row < BoardRows; row++)
            {
                for (int col = 0; col < BoardRows; col++)
                {
                    Color tileColor = (selectedRow == row && selectedCol == col) ? Color.LightBlue :
                                      ((row + col) % 2 == 0) ? Color.White : Color.FromArgb(169, 122, 101);

                    using (SolidBrush brush = new SolidBrush(tileColor))
                    {
                        g.FillRectangle(brush, startX + (col * tileSize), startY + (row * tileSize), tileSize, tileSize);
                    }

                    // Add semi transparent display over selected squares
                    if (selectedRow == row && selectedCol == col)
                    {
                        using (SolidBrush overlay = new SolidBrush(Color.FromArgb(25, Color.Cyan)))
                        {
                            g.FillRectangle(overlay, startX + (col * tileSize), startY + (row * tileSize), tileSize, tileSize);
                        }
                    }
                    // --DRAW PIECE--
                    int squareIndex = row * 8 + col;
                    int piece = boardInstance.Squares[squareIndex];

                    if (piece != Piece.None)
                    {
                        string pieceSymbol = pieceInstance.GetPieceSymbol(piece);

                        if (pieceImages.TryGetValue(pieceSymbol, out Image? img) && img != null)
                        {
                            g.DrawImage(img, new RectangleF(startX + (col * tileSize), startY + (row * tileSize), tileSize, tileSize));
                        }
                    }
                }
            }
        }
        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            OnSquareClicked(e.X, e.Y);
        }

        private void OnSquareClicked(int X, int Y)
        {
            var pos = GetBoardPosition(X, Y);

            // If the click is inside the board
            if (pos != null)
            {
                // Where the current click is
                var (clickedRow, clickedCol) = pos.Value;

                // If no piece is selected right now 
                if (!isPieceSelected)
                {
                    // Select a piece if the square has a piece on it and highlight it
                    if (boardInstance.Squares[clickedRow * 8 + clickedCol] != Piece.None)
                    {
                        selectedRow = clickedRow;
                        selectedCol = clickedCol;
                        isPieceSelected = true;

                        this.Invalidate(); // Repaint the form to make sure the square is blue
                    }
                }
                // If a piece is already selected; add movement logic
                else if (isPieceSelected)
                {
                    // If the already selected square is clicked on again, remove selection
                    if (selectedRow == clickedRow && selectedCol == clickedCol)
                    {
                        selectedRow = selectedCol = null;
                        isPieceSelected = false;
                        this.Invalidate();
                    }
                    // If another square is clicked and another piece is present, unselect
                    else if (boardInstance.Squares[clickedRow * 8 + clickedCol] != Piece.None && selectedRow != null && selectedCol != null)
                    {
                        int fromIndex = selectedRow.Value * 8 + selectedCol.Value;
                        int toIndex = clickedRow * 8 + clickedCol;

                        // A basic move that has no flag for now
                        Move move = new Move((ushort)fromIndex, (ushort)toIndex);

                        // Actually make the move
                        boardInstance.MakeMove(move);

                        // Clear the selection
                        selectedRow = selectedCol = null;
                        isPieceSelected = false;

                        // Refresh the gui
                        this.Invalidate();
                    }
                    // If the clicked on square is empty, unselect the selected square and move
                    else if (boardInstance.Squares[clickedRow * 8 + clickedCol] == Piece.None && selectedRow != null && selectedCol != null)
                    {
                        int fromIndex = selectedRow.Value * 8 + selectedCol.Value;
                        int toIndex = clickedRow * 8 + clickedCol;

                        // A basic move that has no flag for now
                        Move move = new Move((ushort)fromIndex, (ushort)toIndex);

                        // Actually make the move
                        boardInstance.MakeMove(move);

                        // Clear the selection
                        selectedRow = selectedCol = null;
                        isPieceSelected = false;

                        // Refresh the gui
                        this.Invalidate();
                    }
                }
            }
            // If the click is outside the board
            else if (pos == null && isPieceSelected)
            {
                selectedRow = selectedCol = null;
                isPieceSelected = false;
                this.Invalidate();
            }
        }

        private (int row, int col)? GetBoardPosition(int x, int y)
        {
            if (this.startX <= x && x < this.startX + this.tileSize * BoardRows &&
                this.startY <= y && y < this.startY + this.tileSize * BoardRows)
            {
                int row = (y - this.startY) / this.tileSize;
                int col = (x - this.startX) / this.tileSize;
                return (row, col);
            }

            return null;
        }

        private void LoadPieceImages()
        {
            pieceImages["P"] = Image.FromFile($"Resources/Pieces/white_pawn.png");
            pieceImages["R"] = Image.FromFile("Resources/Pieces/white_rook.png");
            pieceImages["N"] = Image.FromFile("Resources/Pieces/white_knight.png");
            pieceImages["B"] = Image.FromFile("Resources/Pieces/white_bishop.png");
            pieceImages["Q"] = Image.FromFile("Resources/Pieces/white_queen.png");
            pieceImages["K"] = Image.FromFile("Resources/Pieces/white_king.png");

            pieceImages["p"] = Image.FromFile("Resources/Pieces/black_pawn.png");
            pieceImages["r"] = Image.FromFile("Resources/Pieces/black_rook.png");
            pieceImages["n"] = Image.FromFile("Resources/Pieces/black_knight.png");
            pieceImages["b"] = Image.FromFile("Resources/Pieces/black_bishop.png");
            pieceImages["q"] = Image.FromFile("Resources/Pieces/black_queen.png");
            pieceImages["k"] = Image.FromFile("Resources/Pieces/black_king.png");
        }
    }
}