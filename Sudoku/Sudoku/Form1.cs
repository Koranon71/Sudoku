using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        private TextBox[,] grid;
        private int[,] solution;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeGrid();
            GenerateSudoku();
        }

        private void InitializeGrid()
        {
            grid = new TextBox[9, 9];
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    grid[row, col] = new TextBox
                    {
                        Width = 40,
                        Height = 40,
                        Location = new Point(40 * col, 40 * row),
                        MaxLength = 1,
                        TextAlign = HorizontalAlignment.Center,
                        Font = new Font("Arial", 20),
                        BackColor = Color.White,
                        ForeColor = Color.Black
                    };
                    this.Controls.Add(grid[row, col]);
                }
            }
        }

        private void GenerateSudoku()
        {
            solution = new int[,]
            {
                {5, 3, 4, 6, 7, 8, 9, 1, 2},
                {6, 7, 2, 1, 9, 5, 3, 4, 8},
                {1, 9, 8, 3, 4, 2, 5, 6, 7},
                {8, 5, 9, 7, 6, 1, 4, 2, 3},
                {4, 2, 6, 8, 5, 3, 7, 9, 1},
                {7, 1, 3, 9, 2, 4, 8, 5, 6},
                {9, 6, 1, 5, 3, 7, 2, 8, 4},
                {2, 8, 7, 4, 1, 9, 6, 3, 5},
                {3, 4, 5, 2, 8, 6, 1, 7, 9}
            };

            Random rand = new Random();
            int numbersToAdd = 20;
            for (int i = 0; i < numbersToAdd; i++)
            {
                int row, col;
                do
                {
                    row = rand.Next(0, 9);
                    col = rand.Next(0, 9);
                } while (!string.IsNullOrEmpty(grid[row, col].Text));

                grid[row, col].Text = solution[row, col].ToString();
                grid[row, col].BackColor = Color.Blue;
                grid[row, col].ForeColor = Color.White;
                grid[row, col].ReadOnly = true;
            }
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            if (IsValidSudoku())
            {
                MessageBox.Show("Верно!");
            }
            else
            {
                MessageBox.Show("Неверно, попробуйте ещё");
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            foreach (var textBox in grid)
            {
                if (!textBox.ReadOnly)
                {
                    textBox.Text = "";
                    textBox.BackColor = Color.White;
                    textBox.ForeColor = Color.Black;
                }
            }
        }

        private bool IsValidSudoku()
        {
            // Проверка строк
            for (int row = 0; row < 9; row++)
            {
                bool[] seen = new bool[9];
                for (int col = 0; col < 9; col++)
                {
                    if (int.TryParse(grid[row, col].Text, out int value) && value >= 1 && value <= 9)
                    {
                        if (seen[value - 1]) return false;
                        seen[value - 1] = true;
                    }
                    else if (!string.IsNullOrEmpty(grid[row, col].Text))
                    {
                        return false;
                    }
                }
            }

            // Проверка столбцов
            for (int col = 0; col < 9; col++)
            {
                bool[] seen = new bool[9];
                for (int row = 0; row < 9; row++)
                {
                    if (int.TryParse(grid[row, col].Text, out int value) && value >= 1 && value <= 9)
                    {
                        if (seen[value - 1]) return false;
                        seen[value - 1] = true;
                    }
                    else if (!string.IsNullOrEmpty(grid[row, col].Text))
                    {
                        return false;
                    }
                }
            }

            // Проверка квадратов 3x3
            for (int block = 0; block < 9; block++)
            {
                bool[] seen = new bool[9];
                int startRow = (block / 3) * 3;
                int startCol = (block % 3) * 3;
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        int curRow = startRow + row;
                        int curCol = startCol + col;
                        if (int.TryParse(grid[curRow, curCol].Text, out int value) && value >= 1 && value <= 9)
                        {
                            if (seen[value - 1]) return false;
                            seen[value - 1] = true;
                        }
                        else if (!string.IsNullOrEmpty(grid[curRow, curCol].Text))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}