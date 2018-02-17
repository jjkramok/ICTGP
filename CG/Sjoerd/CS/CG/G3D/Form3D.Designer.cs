namespace CG.G3D
{
	partial class Form3D
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
			this.SuspendLayout();
			// 
			// drawPanel
			// 
			this.drawPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.drawPanel.Location = new System.Drawing.Point(13, 13);
			this.drawPanel.Name = "drawPanel";
			this.drawPanel.Size = new System.Drawing.Size(554, 422);
			this.drawPanel.TabIndex = 0;
			this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
			// 
			// Form3D
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(579, 447);
			this.Controls.Add(this.drawPanel);
			this.KeyPreview = true;
			this.Name = "Form3D";
			this.Text = "Form3D";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form3D_KeyDown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel drawPanel;
	}
}