namespace SudokuSolver
{
    partial class SudokuSolver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sizeField = new System.Windows.Forms.TextBox();
            this.generateField = new System.Windows.Forms.Button();
            this.Solve = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sizeField
            // 
            this.sizeField.Location = new System.Drawing.Point(13, 13);
            this.sizeField.Name = "sizeField";
            this.sizeField.Size = new System.Drawing.Size(68, 26);
            this.sizeField.TabIndex = 0;
            // 
            // generateField
            // 
            this.generateField.Location = new System.Drawing.Point(87, 13);
            this.generateField.Name = "generateField";
            this.generateField.Size = new System.Drawing.Size(131, 35);
            this.generateField.TabIndex = 1;
            this.generateField.Text = "Generate Field";
            this.generateField.UseVisualStyleBackColor = true;
            this.generateField.Click += new System.EventHandler(this.GenerateFieldButton);
            // 
            // Solve
            // 
            this.Solve.Location = new System.Drawing.Point(225, 13);
            this.Solve.Name = "Solve";
            this.Solve.Size = new System.Drawing.Size(88, 35);
            this.Solve.TabIndex = 2;
            this.Solve.Text = "Solve";
            this.Solve.UseVisualStyleBackColor = true;
            this.Solve.Click += new System.EventHandler(this.SolveButton);
            // 
            // SudokuSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 744);
            this.Controls.Add(this.Solve);
            this.Controls.Add(this.generateField);
            this.Controls.Add(this.sizeField);
            this.Name = "SudokuSolver";
            this.Text = "Sudoku Solver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sizeField;
        private System.Windows.Forms.Button generateField;
        private System.Windows.Forms.Button Solve;
    }
}

