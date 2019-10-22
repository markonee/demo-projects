namespace NMK2
{
    partial class UserControl1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ukloniSlikuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uvećajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.umanjiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ukloniSlikuToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.povećajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smanjiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ukloniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prilagodiVeličinuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 379);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 43);
            this.button1.TabIndex = 17;
            this.button1.Text = "Učitaj sliku";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Datum slikanja:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(184, 340);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(358, 26);
            this.dateTimePicker1.TabIndex = 19;
            this.dateTimePicker1.Validating += new System.ComponentModel.CancelEventHandler(this.dateTimePicker1_Validating);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.statusStrip1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 480);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Slika pacijenta";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(3, 455);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(574, 22);
            this.statusStrip1.TabIndex = 24;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(317, 379);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(225, 43);
            this.button2.TabIndex = 23;
            this.button2.Text = "Potvrdi";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox1.InitialImage = global::NMK2.Properties.Resources.maxresdefault;
            this.pictureBox1.Location = new System.Drawing.Point(27, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(515, 295);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip4
            // 
            this.contextMenuStrip4.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip4.Name = "contextMenuStrip4";
            this.contextMenuStrip4.Size = new System.Drawing.Size(61, 4);
            // 
            // ukloniSlikuToolStripMenuItem
            // 
            this.ukloniSlikuToolStripMenuItem.Name = "ukloniSlikuToolStripMenuItem";
            this.ukloniSlikuToolStripMenuItem.Size = new System.Drawing.Size(174, 30);
            this.ukloniSlikuToolStripMenuItem.Text = "Ukloni sliku";
            // 
            // uvećajToolStripMenuItem
            // 
            this.uvećajToolStripMenuItem.Name = "uvećajToolStripMenuItem";
            this.uvećajToolStripMenuItem.Size = new System.Drawing.Size(174, 30);
            this.uvećajToolStripMenuItem.Text = "Uvećaj";
            // 
            // umanjiToolStripMenuItem
            // 
            this.umanjiToolStripMenuItem.Name = "umanjiToolStripMenuItem";
            this.umanjiToolStripMenuItem.Size = new System.Drawing.Size(174, 30);
            this.umanjiToolStripMenuItem.Text = "Umanji";
            // 
            // ukloniSlikuToolStripMenuItem1
            // 
            this.ukloniSlikuToolStripMenuItem1.Name = "ukloniSlikuToolStripMenuItem1";
            this.ukloniSlikuToolStripMenuItem1.Size = new System.Drawing.Size(198, 30);
            this.ukloniSlikuToolStripMenuItem1.Text = "Ukloni sliku";
            // 
            // povećajToolStripMenuItem
            // 
            this.povećajToolStripMenuItem.Name = "povećajToolStripMenuItem";
            this.povećajToolStripMenuItem.Size = new System.Drawing.Size(198, 30);
            this.povećajToolStripMenuItem.Text = "Povećaj";
            // 
            // smanjiToolStripMenuItem
            // 
            this.smanjiToolStripMenuItem.Name = "smanjiToolStripMenuItem";
            this.smanjiToolStripMenuItem.Size = new System.Drawing.Size(198, 30);
            this.smanjiToolStripMenuItem.Text = "Smanji";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ukloniToolStripMenuItem,
            this.prilagodiVeličinuToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(218, 64);
            // 
            // ukloniToolStripMenuItem
            // 
            this.ukloniToolStripMenuItem.Name = "ukloniToolStripMenuItem";
            this.ukloniToolStripMenuItem.Size = new System.Drawing.Size(217, 30);
            this.ukloniToolStripMenuItem.Text = "Ukloni";
            this.ukloniToolStripMenuItem.Click += new System.EventHandler(this.ukloniToolStripMenuItem_Click);
            // 
            // prilagodiVeličinuToolStripMenuItem
            // 
            this.prilagodiVeličinuToolStripMenuItem.Name = "prilagodiVeličinuToolStripMenuItem";
            this.prilagodiVeličinuToolStripMenuItem.Size = new System.Drawing.Size(217, 30);
            this.prilagodiVeličinuToolStripMenuItem.Text = "Prilagodi veličinu";
            this.prilagodiVeličinuToolStripMenuItem.Click += new System.EventHandler(this.prilagodiVeličinuToolStripMenuItem_Click);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBox1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(580, 486);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;

        private System.Windows.Forms.ToolStripMenuItem ukloniSlikuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uvećajToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem umanjiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ukloniSlikuToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem povećajToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smanjiToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ukloniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prilagodiVeličinuToolStripMenuItem;
    }
}
