using System;

namespace SudokuSolver
{
    public class Sudoku
    {
        private int size { get; } //the size of the sudoku. Internal use only
        public int[,] field; //the field

        public int BlockWidth { get; private set; } //a property holding the width of a block
        public int BlockHeight { get; private set; } //a propery holding the height of a block

        /// <summary>
        /// Constructor of the sudoku, initialized the needed variables
        /// </summary>
        /// <param name="size">The size of the sudoku</param>
        public Sudoku(int size)
        {
            this.size = size; //set the size
            field = new int[size, size]; //initialize the field

            SetBlockSize(); //calculate the block size
        }

        /// <summary>
        /// Sets a number in the sudoku
        /// </summary>
        /// <param name="row">The row to place the number in</param>
        /// <param name="column">The column to place the number in</param>
        /// <param name="number">The number to set</param>
        public void AddNumber(int row, int column, int number)
        {
            field[row, column] = number; //set the nuber on the field
        }

        /// <summary>
        /// Gets a number from the field
        /// </summary>
        /// <param name="row">The row to get the number from</param>
        /// <param name="column">The column to get the number from</param>
        /// <returns>The number at that position</returns>
        public int GetNumber(int row, int column)
        {
            return field[row, column]; //get the number from the field
        }

        /// <summary>
        /// Solve the sudoku
        /// </summary>
        /// <returns>A boolean indicating if the sudoku could be solved or not</returns>
        public bool Solve()
        {
            return Solve(0, 0); //solve with a starting row and column
        }

        /// <summary>
        /// Solve the sudoku
        /// </summary>
        /// <param name="row">The starting row</param>
        /// <param name="column">The starting column</param>
        /// <returns>A boolean indicating if the sudoku could be solved or not</returns>
        private bool Solve(int row, int column)
        {
            if (row == size) return true; //return true when we're done with the sudoku
            if (field[row, column] != 0) //if the number is already filled in by the user
            {
                if (Solve(column == size - 1 ? row + 1 : row, (column + 1) % size)) //go on to the next box
                {
                    return true; //and return true if that box is filled in
                }
            }
            else //else try to solve that box
            {
                for (int number = 1; number < size + 1; number++) //try each possible number
                {
                    if (CanPlace(row, column, number)) //if the numer can be places
                    {
                        field[row, column] = number; //set the number

                        if (Solve(column == size - 1 ? row + 1 : row, (column + 1) % size)) return true; //if the reset of the sudoku can be solved, return true
                        field[row, column] = 0; //else set the number back to 0, indicating that that number hasn't been solved yet
                    }
                }
            }
            return false; //if the puzzle couldn't be solved, return false
        }

        /// <summary>
        /// Can the number be placed at that position in the field
        /// </summary>
        /// <param name="row">The row to check</param>
        /// <param name="column">The column to check</param>
        /// <param name="number">The number to check</param>
        /// <returns>A boolean indicating if the number can be placed there</returns>
        private bool CanPlace(int row, int column, int number)
        {
            for (int i = 0; i < size; i++)
            {
                if (i != column && field[row, i] == number) return false; //check if the row doesn't contain the number
                if (i != row && field[i, column] == number) return false; //check if the column doesn't contain the number
            }
            int startRow = row / BlockWidth * BlockWidth; //calculate the start row of the block
            int startColumn = column / BlockHeight * BlockHeight; //calculate the start column of the block

            for (int i = startRow; i < startRow + BlockWidth; i++) //go through each of the rows
            {
                for (int j = startColumn; j < startColumn + BlockHeight; j++) //go through each of the column
                {
                    if (!(i == row && j == column)) //if the row and column are not equal to the current box trying to solve
                    {
                        if (field[i, j] == number) //if the field contains a number (the number is already in the block), return false
                        {
                            return false;
                        }
                    }
                }
            }
            return true; //else you're good to place the number
        }

        /// <summary>
        /// Dynamically calculates the size of a block. It can handle every size from 4 up to theoratical infinity, except for primes
        /// </summary>
        private void SetBlockSize()
        {
            int sqrt = (int)Math.Sqrt(size); //calculates the square root of the size
            if (sqrt * sqrt == size) //if the square root of the size is the width and height of the puzzle, set the correct size
            {
                BlockHeight = sqrt;
                BlockWidth = sqrt;
                return;
            }
            int oldWidth = 0;
            int oldHeight = 0;
            int i = 2;
            while (true) //go on unless there's no more solution
            {
                if (size % i == 0) //if the size is divisible by i (the number trying to calculate the block for)
                {
                    int newWidth = size / i; //calculate the width of a block
                    int newHeight = size / newWidth; //calculate the height of a block
                    if (newWidth == oldHeight) break; //if the box is the same dimensions of the last time tried, break out of the loop.

                    //if not, set the temporary new dimensions for the block
                    oldWidth = newWidth;
                    oldHeight = newHeight;
                }
                i++; //increase i to check for another box size
            }
            //set the new dimensions of the block
            BlockWidth = oldWidth;
            BlockHeight = oldHeight;
        }
    }
}
