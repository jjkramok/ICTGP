namespace Assignment.Renderer
{
	partial class MainForm
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
			this.worldPanel1 = new System.Windows.Forms.Panel();
			this.startButton = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.infoLabel = new System.Windows.Forms.Label();
			this.worldPanel2 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// worldPanel
			// 
			this.worldPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.worldPanel1.Location = new System.Drawing.Point(0, 0);
			this.worldPanel1.Name = "worldPanel";
			this.worldPanel1.Size = new System.Drawing.Size(700, 474);
			this.worldPanel1.TabIndex = 0;
			this.worldPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.worldPanel2_Paint);
			// 
			// startButton
			// 
			this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.startButton.Location = new System.Drawing.Point(717, 13);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(75, 23);
			this.startButton.TabIndex = 1;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(716, 43);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "TestButton";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// infoLabel
			// 
			this.infoLabel.AutoSize = true;
			this.infoLabel.Location = new System.Drawing.Point(714, 69);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new System.Drawing.Size(35, 13);
			this.infoLabel.TabIndex = 0;
			this.infoLabel.Text = "label1";
			// 
			// worldPanel2
			// 
			this.worldPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.worldPanel2.Location = new System.Drawing.Point(0, 0);
			this.worldPanel2.Name = "worldPanel2";
			this.worldPanel2.Size = new System.Drawing.Size(700, 474);
			this.worldPanel2.TabIndex = 3;
			this.worldPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.worldPanel2_Paint);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 474);
			this.Controls.Add(this.worldPanel2);
			this.Controls.Add(this.infoLabel);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.worldPanel1);
			this.Name = "MainForm";
			this.Text = "AAIForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel worldPanel1;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label infoLabel;
		private System.Windows.Forms.Panel worldPanel2;
	}
}