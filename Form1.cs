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

        private Label boardPositionInfoLabel;

        public Form1()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(50, 50, 50);
            this.WindowState = FormWindowState.Maximized; // Make sure the window is maximized
            this.StartPosition = FormStartPosition.CenterScreen; // Make the window start at the center of the screen

            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Make sure that window is not resized
            this.MaximizeBox = false; // Disable maximize button

            this.MouseDown += new MouseEventHandler(Form1_MouseDown);// Dectect mouse click

            windowWidth = this.ClientSize.Width;
            windowHeight = this.ClientSize.Height;

            // Label to show the row and col the user clicked on
            boardPositionInfoLabel = new Label();
            boardPositionInfoLabel.Text = $"Row: Col: ";
            boardPositionInfoLabel.ForeColor = Color.White;
            boardPositionInfoLabel.BackColor = Color.Transparent;
            boardPositionInfoLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            boardPositionInfoLabel.AutoSize = true;
            boardPositionInfoLabel.Location = new Point(20, 20);
            this.Controls.Add(boardPositionInfoLabel);
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
                    Color tileColor;

                    if (selectedRow == row && selectedCol == col)
                    {
                        tileColor = Color.LightBlue;
                    }
                    else
                    {
                        tileColor = ((row + col) % 2 == 0) ? Color.White : Color.Gray; // Choose the color of the squares - add row and col, divide by 2, if remainder is 0, white, else grey
                    }

                    using (SolidBrush brush = new SolidBrush(tileColor))
                    {
                        g.FillRectangle(brush, startX + (col * tileSize), startY + (row * tileSize), tileSize, tileSize);
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

    }
}

