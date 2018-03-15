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
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.navigationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.entityInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.entityForceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.spatialPartitioningGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enableStatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.arriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fleeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.flockingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.obstacleAvoidanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pathFollowingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.seekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.wanderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.steeringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ditheringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.truncatedSumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.prioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pathSmoothingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// worldPanel
			// 
			this.worldPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.worldPanel.Location = new System.Drawing.Point(0, 24);
			this.worldPanel.Margin = new System.Windows.Forms.Padding(0);
			this.worldPanel.Name = "worldPanel";
			this.worldPanel.Size = new System.Drawing.Size(804, 450);
			this.worldPanel.TabIndex = 0;
			this.worldPanel.Click += new System.EventHandler(this.worldPanel_Click);
			this.worldPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.worldPanel_Paint);
			// 
			// menuStrip
			// 
			this.menuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.menuStrip.AutoSize = false;
			this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.steeringToolStripMenuItem,
            this.miscToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Padding = new System.Windows.Forms.Padding(0);
			this.menuStrip.Size = new System.Drawing.Size(804, 24);
			this.menuStrip.TabIndex = 8;
			this.menuStrip.Text = "menuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// restartToolStripMenuItem
			// 
			this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
			this.restartToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.restartToolStripMenuItem.Text = "Restart";
			this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navigationToolStripMenuItem,
            this.entityInfoToolStripMenuItem,
            this.entityForceToolStripMenuItem,
            this.spatialPartitioningGridToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// navigationToolStripMenuItem
			// 
			this.navigationToolStripMenuItem.CheckOnClick = true;
			this.navigationToolStripMenuItem.Name = "navigationToolStripMenuItem";
			this.navigationToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.navigationToolStripMenuItem.Text = "Navigation";
			this.navigationToolStripMenuItem.Click += new System.EventHandler(this.navigationToolStripMenuItem_Click);
			// 
			// entityInfoToolStripMenuItem
			// 
			this.entityInfoToolStripMenuItem.CheckOnClick = true;
			this.entityInfoToolStripMenuItem.Name = "entityInfoToolStripMenuItem";
			this.entityInfoToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.entityInfoToolStripMenuItem.Text = "Entity Info";
			this.entityInfoToolStripMenuItem.Click += new System.EventHandler(this.entityInfoToolStripMenuItem_Click);
			// 
			// entityForceToolStripMenuItem
			// 
			this.entityForceToolStripMenuItem.CheckOnClick = true;
			this.entityForceToolStripMenuItem.Name = "entityForceToolStripMenuItem";
			this.entityForceToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.entityForceToolStripMenuItem.Text = "Entity Force";
			this.entityForceToolStripMenuItem.Click += new System.EventHandler(this.entityForceToolStripMenuItem_Click);
			// 
			// spatialPartitioningGridToolStripMenuItem
			// 
			this.spatialPartitioningGridToolStripMenuItem.CheckOnClick = true;
			this.spatialPartitioningGridToolStripMenuItem.Name = "spatialPartitioningGridToolStripMenuItem";
			this.spatialPartitioningGridToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.spatialPartitioningGridToolStripMenuItem.Text = "SpatialPartitioning Grid";
			this.spatialPartitioningGridToolStripMenuItem.Click += new System.EventHandler(this.spatialPartitioningGridToolStripMenuItem_Click);
			// 
			// debugToolStripMenuItem
			// 
			this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableStatesToolStripMenuItem,
            this.arriveToolStripMenuItem,
            this.fleeToolStripMenuItem,
            this.flockingToolStripMenuItem,
            this.obstacleAvoidanceToolStripMenuItem,
            this.pathFollowingToolStripMenuItem,
            this.seekToolStripMenuItem,
            this.wanderToolStripMenuItem});
			this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
			this.debugToolStripMenuItem.Size = new System.Drawing.Size(99, 24);
			this.debugToolStripMenuItem.Text = "Steering Forces";
			// 
			// enableStatesToolStripMenuItem
			// 
			this.enableStatesToolStripMenuItem.Checked = true;
			this.enableStatesToolStripMenuItem.CheckOnClick = true;
			this.enableStatesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.enableStatesToolStripMenuItem.Name = "enableStatesToolStripMenuItem";
			this.enableStatesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.enableStatesToolStripMenuItem.Text = "Enable States";
			this.enableStatesToolStripMenuItem.Click += new System.EventHandler(this.enableStatesToolStripMenuItem_Click);
			// 
			// arriveToolStripMenuItem
			// 
			this.arriveToolStripMenuItem.CheckOnClick = true;
			this.arriveToolStripMenuItem.Enabled = false;
			this.arriveToolStripMenuItem.Name = "arriveToolStripMenuItem";
			this.arriveToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.arriveToolStripMenuItem.Text = "Arrive";
			this.arriveToolStripMenuItem.Click += new System.EventHandler(this.arriveToolStripMenuItem_Click);
			// 
			// fleeToolStripMenuItem
			// 
			this.fleeToolStripMenuItem.CheckOnClick = true;
			this.fleeToolStripMenuItem.Enabled = false;
			this.fleeToolStripMenuItem.Name = "fleeToolStripMenuItem";
			this.fleeToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.fleeToolStripMenuItem.Text = "Flee";
			this.fleeToolStripMenuItem.Click += new System.EventHandler(this.fleeToolStripMenuItem_Click);
			// 
			// flockingToolStripMenuItem
			// 
			this.flockingToolStripMenuItem.CheckOnClick = true;
			this.flockingToolStripMenuItem.Enabled = false;
			this.flockingToolStripMenuItem.Name = "flockingToolStripMenuItem";
			this.flockingToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.flockingToolStripMenuItem.Text = "Flocking";
			this.flockingToolStripMenuItem.Click += new System.EventHandler(this.flockingToolStripMenuItem_Click);
			// 
			// obstacleAvoidanceToolStripMenuItem
			// 
			this.obstacleAvoidanceToolStripMenuItem.CheckOnClick = true;
			this.obstacleAvoidanceToolStripMenuItem.Enabled = false;
			this.obstacleAvoidanceToolStripMenuItem.Name = "obstacleAvoidanceToolStripMenuItem";
			this.obstacleAvoidanceToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.obstacleAvoidanceToolStripMenuItem.Text = "Obstacle Avoidance";
			this.obstacleAvoidanceToolStripMenuItem.Click += new System.EventHandler(this.obstacleAvoidanceToolStripMenuItem_Click);
			// 
			// pathFollowingToolStripMenuItem
			// 
			this.pathFollowingToolStripMenuItem.CheckOnClick = true;
			this.pathFollowingToolStripMenuItem.Enabled = false;
			this.pathFollowingToolStripMenuItem.Name = "pathFollowingToolStripMenuItem";
			this.pathFollowingToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.pathFollowingToolStripMenuItem.Text = "Path Following";
			this.pathFollowingToolStripMenuItem.Click += new System.EventHandler(this.pathFollowingToolStripMenuItem_Click);
			// 
			// seekToolStripMenuItem
			// 
			this.seekToolStripMenuItem.CheckOnClick = true;
			this.seekToolStripMenuItem.Enabled = false;
			this.seekToolStripMenuItem.Name = "seekToolStripMenuItem";
			this.seekToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.seekToolStripMenuItem.Text = "Seek";
			this.seekToolStripMenuItem.Click += new System.EventHandler(this.seekToolStripMenuItem_Click);
			// 
			// wanderToolStripMenuItem
			// 
			this.wanderToolStripMenuItem.CheckOnClick = true;
			this.wanderToolStripMenuItem.Enabled = false;
			this.wanderToolStripMenuItem.Name = "wanderToolStripMenuItem";
			this.wanderToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.wanderToolStripMenuItem.Text = "Wander";
			this.wanderToolStripMenuItem.Click += new System.EventHandler(this.wanderToolStripMenuItem_Click);
			// 
			// steeringToolStripMenuItem
			// 
			this.steeringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ditheringToolStripMenuItem,
            this.truncatedSumToolStripMenuItem,
            this.prioToolStripMenuItem});
			this.steeringToolStripMenuItem.Name = "steeringToolStripMenuItem";
			this.steeringToolStripMenuItem.Size = new System.Drawing.Size(125, 24);
			this.steeringToolStripMenuItem.Text = "Steering Calculation";
			// 
			// ditheringToolStripMenuItem
			// 
			this.ditheringToolStripMenuItem.Name = "ditheringToolStripMenuItem";
			this.ditheringToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.ditheringToolStripMenuItem.Text = "Dithering";
			this.ditheringToolStripMenuItem.Click += new System.EventHandler(this.ditheringToolStripMenuItem_Click);
			// 
			// truncatedSumToolStripMenuItem
			// 
			this.truncatedSumToolStripMenuItem.Name = "truncatedSumToolStripMenuItem";
			this.truncatedSumToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.truncatedSumToolStripMenuItem.Text = "Truncated sum";
			this.truncatedSumToolStripMenuItem.Click += new System.EventHandler(this.truncatedSumToolStripMenuItem_Click);
			// 
			// prioToolStripMenuItem
			// 
			this.prioToolStripMenuItem.Name = "prioToolStripMenuItem";
			this.prioToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.prioToolStripMenuItem.Text = "Priorization";
			this.prioToolStripMenuItem.Click += new System.EventHandler(this.prioToolStripMenuItem_Click);
			// 
			// miscToolStripMenuItem
			// 
			this.miscToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pathSmoothingToolStripMenuItem});
			this.miscToolStripMenuItem.Name = "miscToolStripMenuItem";
			this.miscToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.miscToolStripMenuItem.Text = "Misc";
			// 
			// pathSmoothingToolStripMenuItem
			// 
			this.pathSmoothingToolStripMenuItem.Checked = true;
			this.pathSmoothingToolStripMenuItem.CheckOnClick = true;
			this.pathSmoothingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.pathSmoothingToolStripMenuItem.Name = "pathSmoothingToolStripMenuItem";
			this.pathSmoothingToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.pathSmoothingToolStripMenuItem.Text = "Path Smoothing";
			this.pathSmoothingToolStripMenuItem.Click += new System.EventHandler(this.pathSmoothingToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 474);
			this.Controls.Add(this.worldPanel);
			this.Controls.Add(this.menuStrip);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.Text = "AAIForm";
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel worldPanel;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem navigationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem entityInfoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem entityForceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem spatialPartitioningGridToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem enableStatesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem arriveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fleeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem flockingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem obstacleAvoidanceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pathFollowingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem seekToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem wanderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem steeringToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ditheringToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem truncatedSumToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem prioToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miscToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pathSmoothingToolStripMenuItem;
	}
}