namespace WindowsFormsApp2
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.detectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lABELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pREDICTIONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(449, 426);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detectToolStripMenuItem,
            this.loadNextToolStripMenuItem,
            this.lABELToolStripMenuItem,
            this.pREDICTIONToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(449, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // detectToolStripMenuItem
            // 
            this.detectToolStripMenuItem.Name = "detectToolStripMenuItem";
            this.detectToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.detectToolStripMenuItem.Text = "Detect";
            this.detectToolStripMenuItem.Click += new System.EventHandler(this.detectToolStripMenuItem_Click);
            // 
            // loadNextToolStripMenuItem
            // 
            this.loadNextToolStripMenuItem.Name = "loadNextToolStripMenuItem";
            this.loadNextToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.loadNextToolStripMenuItem.Text = "Detect Next";
            this.loadNextToolStripMenuItem.Click += new System.EventHandler(this.loadNextToolStripMenuItem_Click);
            // 
            // lABELToolStripMenuItem
            // 
            this.lABELToolStripMenuItem.Name = "lABELToolStripMenuItem";
            this.lABELToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.lABELToolStripMenuItem.Text = "LABEL";
            // 
            // pREDICTIONToolStripMenuItem
            // 
            this.pREDICTIONToolStripMenuItem.Name = "pREDICTIONToolStripMenuItem";
            this.pREDICTIONToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.pREDICTIONToolStripMenuItem.Text = "PREDICTION";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem detectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadNextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lABELToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pREDICTIONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

