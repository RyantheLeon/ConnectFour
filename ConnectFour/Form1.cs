namespace ConnectFour;

public partial class Form1 : Form
{
    Button[,] gameButtons = new Button[7, 6]; //array of buttons for markers(red and blue)
    TextBox gameStatus = new TextBox(); // button to display the status for the game/instructions
    bool blue = true; //blue is set to true if the next marker is to be a blue
    string turnText = " goes next.";
    string winnerText = " is the winner!";
    int turnNum = 0;

    public Form1()
    {
        InitializeComponent();
    }

    private void LoadForm()
    {
        this.Text = "Connect 4";
        this.BackColor = Color.White;
        this.Width = 920;
        this.Height = 950;
        int x;
        int y;

        for (int row = 0; row < gameButtons.GetLength(0); row++)
        {
            x = 100 + row * 100;
            for (int col = 0; col < gameButtons.GetLength(1); col++)
            {
                y = 100 * col + 100;
                Button newButton = new Button();
                newButton.Location = new Point(x, y);
                newButton.Name = "btn" + (row + col + 1);
                newButton.Size = new Size(100, 100);
                newButton.TabIndex = row + col;
                newButton.UseVisualStyleBackColor = true;
                newButton.Visible = true;
                newButton.Click += (sender1, ex) => this.buttonHasBeenPressed(sender1);
                gameButtons[row, col] = newButton;
                Controls.Add(gameButtons[row, col]);
            }
        }

        gameStatus.BackColor = Color.Black;
        gameStatus.ForeColor = Color.White;
        gameStatus.Location = new Point(100, 770);
        gameStatus.Size = new Size(700, 100);
        gameStatus.Font = new Font(FontFamily.GenericSerif, 24);
        gameStatus.TabIndex = 100;
        gameStatus.Text = "Blue goes first.";
        Controls.Add(gameStatus);        
    }

    private void buttonHasBeenPressed(object sender)
    {
        Button buttonClicked = (Button)sender;
        int col = buttonClicked.Location.X / 100 - 1;
        int targetRow = GetButtonRow(col);
        if (targetRow >= 0)
        {
            if (blue == true)
            {
                gameButtons[col, targetRow].BackColor = Color.Blue;
                gameStatus.Text = "Red" + turnText;
            }
            else
            {
                gameButtons[col, targetRow].BackColor = Color.Red;
                gameStatus.Text = "Blue" + turnText;
            }
            CheckForWinner(col, targetRow);
            blue = !blue;
        }
    }

    public int GetButtonRow(int colIndex)
    {
        Button curButton;
        for (int row = gameButtons.GetLength(1) - 1; row >= 0; row--)
        {
            curButton = gameButtons[colIndex, row];
            if (curButton.BackColor != Color.Red && curButton.BackColor != Color.Blue)
            {
                return row;
            }
        }
        return -1;
    }

    private void CheckForWinner(int col, int row)
    {
        turnNum++;
        
        if (turnNum < 7)
            return;

        CheckXYForWinner(col, row);

        if (turnNum < 10)
            return;

        CheckDiagonalForWinner(col, row);
    }

    private void CheckXYForWinner(int col, int row)
    {
        CheckVerticalForWinner(col, row);
        CheckHorizontalForWinner(col, row);
    }

    private void CheckVerticalForWinner(int col, int row)
    {
        if (row > 2)
            return;

        int consecutive = 1;
        Color currentColor = Color.White;

        for (int i = 5; i >= row; i--)
        {
            if (gameButtons[col, i].BackColor == currentColor)
            {
                consecutive++;
                if (consecutive == 4 && currentColor != Color.White)
                {
                    DeclareWinner(currentColor.Name);
                }
            }
            else
            {
                consecutive = 1;
            }

            currentColor = gameButtons[col, i].BackColor;
        }
    }

    private void CheckHorizontalForWinner(int col, int row)
    {
        int consecutive = 1;
        Color currentColor = Color.White;

        for (int i = 0; i <= 6; i++)
        {
            if (gameButtons[1, row].BackColor == currentColor)
            {
                consecutive++;
                if (consecutive == 4 && currentColor != Color.White)
                {
                    DeclareWinner(currentColor.Name);
                }
            }
            else
            {
                consecutive = 1;
            }

            currentColor = gameButtons[1, row].BackColor;
        }
    }

    private void CheckDiagonalForWinner(int col, int row)
    {

    }

    private void DeclareWinner(string color)
    {
        gameStatus.Text = color + winnerText;
        foreach (var button in gameButtons)
        {
            button.Dispose();
        }
    }
}