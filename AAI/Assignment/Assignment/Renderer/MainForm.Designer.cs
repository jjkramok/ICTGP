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
			this.worldPanel = new System.Windows.Forms.Panel();
			this.startButton = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.infoLabel = new System.Windows.Forms.Label();
			this.GridButton = new System.Windows.Forms.Button();
			this.aStarButton = new System.Windows.Forms.Button();
			this.entityButton = new System.Windows.Forms.Button();
			this.navigationButton = new System.Windows.Forms.Button();
			this.toggleLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// worldPanel
			// 
			this.worldPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.worldPanel.Location = new System.Drawing.Point(0, 0);
			this.worldPanel.Name = "worldPanel";
			this.worldPanel.Size = new System.Drawing.Size(700, 474);
			this.worldPanel.TabIndex = 0;
			this.worldPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.worldPanel_Paint);
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
			// GridButton
			// 
			this.GridButton.Location = new System.Drawing.Point(716, 439);
			this.GridButton.Name = "GridButton";
			this.GridButton.Size = new System.Drawing.Size(75, 23);
			this.GridButton.TabIndex = 3;
			this.GridButton.Text = "Grid";
			this.GridButton.UseVisualStyleBackColor = true;
			this.GridButton.Click += new System.EventHandler(this.GridButton_Click);
			// 
			// aStarButton
			// 
			this.aStarButton.Location = new System.Drawing.Point(716, 410);
			this.aStarButton.Name = "aStarButton";
			this.aStarButton.Size = new System.Drawing.Size(75, 23);
			this.aStarButton.TabIndex = 4;
			this.aStarButton.Text = "A* Path";
			this.aStarButton.UseVisualStyleBackColor = true;
			this.aStarButton.Click += new System.EventHandler(this.aStarButton_Click);
			// 
			// entityButton
			// 
			this.entityButton.Location = new System.Drawing.Point(717, 381);
			this.entityButton.Name = "entityButton";
			this.entityButton.Size = new System.Drawing.Size(75, 23);
			this.entityButton.TabIndex = 5;
			this.entityButton.Text = "Entity";
			this.entityButton.UseVisualStyleBackColor = true;
			this.entityButton.Click += new System.EventHandler(this.entityButton_Click);
			// 
			// navigationButton
			// 
			this.navigationButton.Location = new System.Drawing.Point(716, 352);
			this.navigationButton.Name = "navigationButton";
			this.navigationButton.Size = new System.Drawing.Size(75, 23);
			this.navigationButton.TabIndex = 6;
			this.navigationButton.Text = "Navigation";
			this.navigationButton.UseVisualStyleBackColor = true;
			this.navigationButton.Click += new System.EventHandler(this.navigationButton_Click);
			// 
			// toggleLabel
			// 
			this.toggleLabel.AutoSize = true;
			this.toggleLabel.Location = new System.Drawing.Point(716, 333);
			this.toggleLabel.Name = "toggleLabel";
			this.toggleLabel.Size = new System.Drawing.Size(73, 13);
			this.toggleLabel.TabIndex = 7;
			this.toggleLabel.Text = "Toggle debug";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 474);
			this.Controls.Add(this.toggleLabel);
			this.Controls.Add(this.navigationButton);
			this.Controls.Add(this.entityButton);
			this.Controls.Add(this.aStarButton);
			this.Controls.Add(this.GridButton);
			this.Controls.Add(this.infoLabel);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.worldPanel);
			this.Name = "MainForm";
			this.Text = "AAIForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel worldPanel;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label infoLabel;
		private System.Windows.Forms.Button GridButton;
		private System.Windows.Forms.Button aStarButton;
		private System.Windows.Forms.Button entityButton;
		private System.Windows.Forms.Button navigationButton;
		private System.Windows.Forms.Label toggleLabel;
	}
}