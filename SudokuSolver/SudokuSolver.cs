using System;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class SudokuSolver : Form
    {
        public const int MIN_FIELD_SIZE = 300;
        public const int BORDER_WIDTH = 100;
        public const int MIN_SIZE = 4;
        public const int MIN_DISTANCE = 5;

        public TextBox[,] boxes;
        public int fieldSize = MIN_FIELD_SIZE;
        public int distance = MIN_DISTANCE;
        public Sudoku sudoku;
        public int size;

        public SudokuSolver()
        {
            InitializeComponent();
            ResizeEnd += delegate //resetup the field if the screen get resized
            {
                SetupField();
            };
            Solve.Enabled = false; //don't allow puzzle to be solved without a field
        }
        /// <summary>
        /// Updates the field when the screen is resized
        /// </summary>
        public void SetupField()
        {
            if (size >= MIN_SIZE)
            {
                RemoveField(); //Clear the field
                fieldSize = Math.Max(MIN_FIELD_SIZE, Math.Min(ClientSize.Width, ClientSize.Height) - BORDER_WIDTH * 2); //calculate the field size
                distance = Math.Max(MIN_DISTANCE, (Math.Min(ClientSize.Width, ClientSize.Height) - BORDER_WIDTH * 2) / (size * 3));//calculate the diustance between the blocks of numbers
                InitializeField(); //Initialize the field
            }
        }

        /// <summary>
        /// Generates the field, based on the size from the input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GenerateFieldButton(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sizeField.Text)) return; //we can't generate a field if there's no size
            size = Convert.ToInt32(sizeField.Text); //get the size from the textbox
            if (size >= MIN_SIZE && !IsPrime(size)) //only allow a field of certain sizes. The size must be bigger than 3 and the number can't be a prime. If the number is a prime, it means the only block size available is 1xsize, which isn't really a sudoku.
            {
                sudoku = new Sudoku(size); //Initialize a new Sudoku instance
                SetupField(); //Call the method to setup the field.
                Solve.Enabled = true; //Allow the solve button to be called
            }
        }

        /// <summary>
        /// This method gets called when the solve button gets clicked, and solves the sudoku
        /// </summary>
        public void SolveButton(object sender, EventArgs e)
        {
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    TextBox textBox = boxes[row, column]; //get the textbox at each position
                    if (!string.IsNullOrEmpty(textBox.Text)) //if there's no number at the box, don't do anything
                    {
                        int number = Convert.ToInt32(textBox.Text); //get the int value from the textbox
                        sudoku.AddNumber(row, column, number); //add the number to the sudoku
                    }
                }
            }
            if (sudoku.Solve()) //if the sudoku is solved
            {
                for (int row = 0; row < size; row++)
                {
                    for (int column = 0; column < size; column++)
                    {
                        boxes[row, column].Text = "" + sudoku.GetNumber(row, column); //place every number in each of the textboxes
                    }
                }
            }
            else
            {
                DrawString("The puzzle couldn't be solved. Is this sudoku valid?"); //else print on the screen that the sudoku is invalid
            }
        }

        /// <summary>
        /// Checks to see if the number is not a prime
        /// </summary>
        /// <param name="number">The number to check</param>
        /// <returns></returns>
        public static bool IsPrime(int number)
        {
            if (number == 1) return false;
            if (number == 2) return true;

            int boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 2; i <= boundary; ++i)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        /// <summary>
        /// Draws a string at the GUI
        /// </summary>
        /// <param name="drawString">The string to draw</param>
        public void DrawString(string drawString)
        {
            Graphics formGraphics = CreateGraphics();
            var drawFont = new Font("Arial", 11);
            var drawBrush = new SolidBrush(Color.Black);
            const float x = BORDER_WIDTH;
            const float y = BORDER_WIDTH / 4F * 3F;
            var drawFormat = new StringFormat();
            formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            formGraphics.Dispose();
        }

        /// <summary>
        /// Removes all the TextBoxes from the field
        /// </summary>
        public void RemoveField()
        {
            if (boxes != null)
            {
                foreach (TextBox textBox in boxes)
                {
                    Controls.Remove(textBox); //remove each of the text boxes from the controls
                }
            }
        }

        /// <summary>
        /// Initializes the field. It constructs the boxes array and fills the array with TextBoxes
        /// </summary>
        public void InitializeField()
        {
            boxes = new TextBox[size, size]; //initialize the TextBox array
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    int boxSize = fieldSize / size; //calculate the box size

                    int xOffset = row / sudoku.BlockWidth * distance; //the xOffset for a textbox. Changes based on the block position
                    int yOffset = column / sudoku.BlockHeight * distance; //the yOffset for a textbox. Changes based on the block position

                    TextBox textBox = new TextBox //initialize a new textbox
                    {
                        Location = new Point(boxSize * row + BORDER_WIDTH + xOffset, boxSize * column + BORDER_WIDTH + yOffset), //set the location of the textbox
                        Size = new Size(boxSize, boxSize), //set the size of the textbox
                        AutoSize = false //don't make the box bigger than it's initial size
                    };
                    float fontSize = 8.25F * boxSize / (size * 2); //calculate the font size to match the the size of a text box
                    textBox.Font = new Font(textBox.Font.FontFamily, fontSize); //set the font with the new font size
                    boxes[row, column] = textBox; //add the textbox to the array
                    Controls.Add(textBox); //add the textbox to the controls
                }
            }
        }
    }
}