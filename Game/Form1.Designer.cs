namespace Game
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.glControl1 = new OpenTK.GLControl();
            this.player1Info = new System.Windows.Forms.Label();
            this.player2Info = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(0, 0);
            this.glControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(1351, 750);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.GlControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.GlControl1_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GlControl1_KeyDown);
            this.glControl1.Resize += new System.EventHandler(this.GlControl1_Resize);
            // 
            // player1Info
            // 
            this.player1Info.AutoSize = true;
            this.player1Info.Location = new System.Drawing.Point(60, 45);
            this.player1Info.Name = "player1Info";
            this.player1Info.Size = new System.Drawing.Size(46, 17);
            this.player1Info.TabIndex = 1;
            this.player1Info.Text = "label1";
            // 
            // player2Info
            // 
            this.player2Info.AutoSize = true;
            this.player2Info.Location = new System.Drawing.Point(1088, 45);
            this.player2Info.Name = "player2Info";
            this.player2Info.Size = new System.Drawing.Size(46, 17);
            this.player2Info.TabIndex = 2;
            this.player2Info.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1351, 750);
            this.Controls.Add(this.player1Info);
            this.Controls.Add(this.player2Info);
            this.Controls.Add(this.glControl1);
            this.Name = "Form1";
            this.Text = "Танки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
      
        private System.Windows.Forms.Label player1Info;
        private System.Windows.Forms.Label player2Info;
    }
}

