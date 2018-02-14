namespace CG.G2D
{
	partial class Form2D
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
			this.drawPanel = new System.Windows.Forms.Panel();
			this.rotateButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// drawPanel
			// 
			this.drawPanel.Location = new System.Drawing.Point(12, 12);
			this.drawPanel.Name = "drawPanel";
			this.drawPanel.Size = new System.Drawing.Size(260, 237);
			this.drawPanel.TabIndex = 0;
			this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
			// 
			// rotateButton
			// 
			this.rotateButton.Location = new System.Drawing.Point(13, 256);
			this.rotateButton.Name = "rotateButton";
			this.rotateButton.Size = new System.Drawing.Size(75, 23);
			this.rotateButton.TabIndex = 1;
			this.rotateButton.Text = "Rotate";
			this.rotateButton.UseVisualStyleBackColor = true;
			this.rotateButton.Click += new System.EventHandler(this.rotateButton_Click);
			// 
			// Form2D
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 301);
			this.Controls.Add(this.rotateButton);
			this.Controls.Add(this.drawPanel);
			this.Name = "Form2D";
			this.Text = "Form2D";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel drawPanel;
		private System.Windows.Forms.Button rotateButton;
	}
}