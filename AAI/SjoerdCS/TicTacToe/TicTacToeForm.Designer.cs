namespace SjoerdCS.TicTacToe
{
	partial class TicTacToeForm
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
			this.BoardBox = new System.Windows.Forms.PictureBox();
			this.TurnBox = new System.Windows.Forms.PictureBox();
			this.TurnLabel = new System.Windows.Forms.Label();
			this.GameStateLabel = new System.Windows.Forms.Label();
			this.StartGameButton = new System.Windows.Forms.Button();
			this.ComputerMoveButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.BoardBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TurnBox)).BeginInit();
			this.SuspendLayout();
			// 
			// BoardBox
			// 
			this.BoardBox.Location = new System.Drawing.Point(180, 91);
			this.BoardBox.Name = "BoardBox";
			this.BoardBox.Size = new System.Drawing.Size(301, 301);
			this.BoardBox.TabIndex = 0;
			this.BoardBox.TabStop = false;
			this.BoardBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.BoardBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
			// 
			// TurnBox
			// 
			this.TurnBox.Location = new System.Drawing.Point(12, 91);
			this.TurnBox.Name = "TurnBox";
			this.TurnBox.Size = new System.Drawing.Size(101, 101);
			this.TurnBox.TabIndex = 1;
			this.TurnBox.TabStop = false;
			this.TurnBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
			// 
			// TurnLabel
			// 
			this.TurnLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TurnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TurnLabel.Location = new System.Drawing.Point(12, 57);
			this.TurnLabel.Name = "TurnLabel";
			this.TurnLabel.Size = new System.Drawing.Size(101, 23);
			this.TurnLabel.TabIndex = 3;
			this.TurnLabel.Text = "Turn:";
			this.TurnLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// GameStateLabel
			// 
			this.GameStateLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.GameStateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GameStateLabel.ForeColor = System.Drawing.Color.Red;
			this.GameStateLabel.Location = new System.Drawing.Point(180, 57);
			this.GameStateLabel.Name = "GameStateLabel";
			this.GameStateLabel.Size = new System.Drawing.Size(301, 23);
			this.GameStateLabel.TabIndex = 4;
			// 
			// StartGameButton
			// 
			this.StartGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.StartGameButton.ForeColor = System.Drawing.Color.Blue;
			this.StartGameButton.Location = new System.Drawing.Point(535, 57);
			this.StartGameButton.Name = "StartGameButton";
			this.StartGameButton.Size = new System.Drawing.Size(212, 47);
			this.StartGameButton.TabIndex = 5;
			this.StartGameButton.Text = "Start New Game";
			this.StartGameButton.UseVisualStyleBackColor = true;
			this.StartGameButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// ComputerMoveButton
			// 
			this.ComputerMoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ComputerMoveButton.Location = new System.Drawing.Point(535, 335);
			this.ComputerMoveButton.Name = "ComputerMoveButton";
			this.ComputerMoveButton.Size = new System.Drawing.Size(212, 57);
			this.ComputerMoveButton.TabIndex = 6;
			this.ComputerMoveButton.Text = "Generate Computer Move";
			this.ComputerMoveButton.UseVisualStyleBackColor = true;
			this.ComputerMoveButton.Click += new System.EventHandler(this.ComputerMoveButton_Click);
			// 
			// TicTacToeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(806, 438);
			this.Controls.Add(this.ComputerMoveButton);
			this.Controls.Add(this.StartGameButton);
			this.Controls.Add(this.GameStateLabel);
			this.Controls.Add(this.TurnLabel);
			this.Controls.Add(this.TurnBox);
			this.Controls.Add(this.BoardBox);
			this.DoubleBuffered = true;
			this.Name = "TicTacToeForm";
			this.Text = "Tic Tac Toe with a Twist - ISGPAD";
			((System.ComponentModel.ISupportInitialize)(this.BoardBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TurnBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox BoardBox;
		private System.Windows.Forms.PictureBox TurnBox;
		private System.Windows.Forms.Label TurnLabel;
		private System.Windows.Forms.Label GameStateLabel;
		private System.Windows.Forms.Button StartGameButton;
		private System.Windows.Forms.Button ComputerMoveButton;
	}
}