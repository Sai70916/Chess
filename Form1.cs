using System.CodeDom;

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

        public Label boardPositionInfoLabel;
        public static Label boardLabel = new Label();

        private Dictionary<string, Image> pieceImages = new();

        public Form1()
        {
            InitializeComponent();
            LoadPieceImages();

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
            // Label to show the row and col the user clicked on
            boardPositionInfoLabel = new Label();
            boardPositionInfoLabel.Text = $"Row: Col: ";
            boardPositionInfoLabel.ForeColor = Color.White;
            boardPositionInfoLabel.BackColor = Color.Transparent;
            boardPositionInfoLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            boardPositionInfoLabel.AutoSize = true;
            boardPositionInfoLabel.Location = new Point(20, 20);
            this.Controls.Add(boardPositionInfoLabel);

            // Debug
            // Delete
            // Label to show the array showing the board
            boardLabel = new Label();
            boardLabel.Text = $"test for display";
            boardLabel.ForeColor = Color.White;
            boardLabel.BackColor = Color.Transparent;
            boardLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            boardLabel.AutoSize = true;
            boardLabel.Location = new Point(20, 50);
            this.Controls.Add(boardLabel);
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
                                      ((row + col) % 2 == 0) ? Color.White : Color.Gray;

                    using (SolidBrush brush = new SolidBrush(tileColor))
                    {
                        g.FillRectangle(brush, startX + (col * tileSize), startY + (row * tileSize), tileSize, tileSize);
                    }

                    // --DRAW PIECE--
                    int squareIndex = row * 8 + col;
                    int piece = Board.Square[squareIndex];

                    if (piece != Piece.None)
                    {
                        string pieceSymbol = "9";
                    }
                }
            }
        }
        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X;
                int y = e.Y;


                var pos = GetBoardPosition(x, y);
                if (pos != null)
                {
                    var (row, col) = pos.Value;

                    selectedRow = row;
                    selectedCol = col;
                    boardPositionInfoLabel.Text = $"Selected Board Position, Row: {selectedRow} Col: {selectedCol}";
                    this.Invalidate(); // Repaint the form to make sure the square is blue
                }
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
            pieceImages["P"] = Image.FromFile("Resources/Piece/wp.png");
            pieceImages["R"] = Image.FromFile("Resources/Piece/wr.png");
            pieceImages["N"] = Image.FromFile("Resources/Piece/wn.png");
            pieceImages["B"] = Image.FromFile("Resources/Piece/wb.png");
            pieceImages["Q"] = Image.FromFile("Resources/Piece/wq.png");
            pieceImages["K"] = Image.FromFile("Resources/Piece/wk.png");

            pieceImages["p"] = Image.FromFile("Resources/Piece/bp.png");
            pieceImages["r"] = Image.FromFile("Resources/Piece/br.png");
            pieceImages["n"] = Image.FromFile("Resources/Piece/bn.png");
            pieceImages["b"] = Image.FromFile("Resources/Piece/bb.png");
            pieceImages["q"] = Image.FromFile("Resources/Piece/bq.png");
            pieceImages["k"] = Image.FromFile("Resources/Piece/bk.png");
        }
    }
}

