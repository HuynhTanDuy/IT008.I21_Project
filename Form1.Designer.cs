namespace PhanMemXemPhimSongNgu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnchoosefile = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSub1 = new System.Windows.Forms.Button();
            this.btnSub2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(3, 38);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(444, 327);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            this.axWindowsMediaPlayer1.Enter += new System.EventHandler(this.axWindowsMediaPlayer1_Enter);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(3, 12);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(238, 20);
            this.txtPath.TabIndex = 1;
            // 
            // btnchoosefile
            // 
            this.btnchoosefile.Location = new System.Drawing.Point(485, 38);
            this.btnchoosefile.Name = "btnchoosefile";
            this.btnchoosefile.Size = new System.Drawing.Size(75, 23);
            this.btnchoosefile.TabIndex = 2;
            this.btnchoosefile.Text = "Choose File";
            this.btnchoosefile.UseVisualStyleBackColor = true;
            this.btnchoosefile.Click += new System.EventHandler(this.btnchoosefile_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(270, 9);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(351, 10);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // txt1
            // 
            this.txt1.Location = new System.Drawing.Point(3, 370);
            this.txt1.Margin = new System.Windows.Forms.Padding(2);
            this.txt1.Multiline = true;
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(444, 37);
            this.txt1.TabIndex = 5;
            this.txt1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 412);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(444, 40);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // btnSub1
            // 
            this.btnSub1.Location = new System.Drawing.Point(485, 83);
            this.btnSub1.Name = "btnSub1";
            this.btnSub1.Size = new System.Drawing.Size(93, 23);
            this.btnSub1.TabIndex = 7;
            this.btnSub1.Text = "Choose Sub1";
            this.btnSub1.UseVisualStyleBackColor = true;
            this.btnSub1.Click += new System.EventHandler(this.btnSub1_Click);
            // 
            // btnSub2
            // 
            this.btnSub2.Location = new System.Drawing.Point(485, 129);
            this.btnSub2.Name = "btnSub2";
            this.btnSub2.Size = new System.Drawing.Size(93, 23);
            this.btnSub2.TabIndex = 8;
            this.btnSub2.Text = "Choose Sub2";
            this.btnSub2.UseVisualStyleBackColor = true;
            this.btnSub2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 475);
            this.Controls.Add(this.btnSub2);
            this.Controls.Add(this.btnSub1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txt1);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnchoosefile);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnchoosefile;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSub1;
        private System.Windows.Forms.Button btnSub2;
    }
}

