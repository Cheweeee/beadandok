namespace PotyogosAmoba
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
            this.newGameButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.saveGameButton = new System.Windows.Forms.Button();
            this.loadGameButton = new System.Windows.Forms.Button();
            this.pauseGameButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.xTimeText = new System.Windows.Forms.TextBox();
            this.oTimeText = new System.Windows.Forms.TextBox();
            this.currentTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // newGameButton
            // 
            this.newGameButton.Location = new System.Drawing.Point(13, 13);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(75, 23);
            this.newGameButton.TabIndex = 0;
            this.newGameButton.Text = "New";
            this.newGameButton.UseVisualStyleBackColor = true;
            this.newGameButton.Click += new System.EventHandler(this.NewGameButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(337, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "X player thinking time:";
            // 
            // saveGameButton
            // 
            this.saveGameButton.Location = new System.Drawing.Point(94, 13);
            this.saveGameButton.Name = "saveGameButton";
            this.saveGameButton.Size = new System.Drawing.Size(75, 23);
            this.saveGameButton.TabIndex = 2;
            this.saveGameButton.Text = "Save";
            this.saveGameButton.UseVisualStyleBackColor = true;
            this.saveGameButton.Click += new System.EventHandler(this.SaveGameButton_Click);
            // 
            // loadGameButton
            // 
            this.loadGameButton.Location = new System.Drawing.Point(175, 13);
            this.loadGameButton.Name = "loadGameButton";
            this.loadGameButton.Size = new System.Drawing.Size(75, 23);
            this.loadGameButton.TabIndex = 3;
            this.loadGameButton.Text = "Load";
            this.loadGameButton.UseVisualStyleBackColor = true;
            this.loadGameButton.Click += new System.EventHandler(this.LoadGameButton_Click);
            // 
            // pauseGameButton
            // 
            this.pauseGameButton.Location = new System.Drawing.Point(256, 13);
            this.pauseGameButton.Name = "pauseGameButton";
            this.pauseGameButton.Size = new System.Drawing.Size(75, 23);
            this.pauseGameButton.TabIndex = 4;
            this.pauseGameButton.Text = "Pause Game";
            this.pauseGameButton.UseVisualStyleBackColor = true;
            this.pauseGameButton.Click += new System.EventHandler(this.PauseGameButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(337, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "O player thinking time:";
            // 
            // xTimeText
            // 
            this.xTimeText.Location = new System.Drawing.Point(493, 8);
            this.xTimeText.Name = "xTimeText";
            this.xTimeText.ReadOnly = true;
            this.xTimeText.Size = new System.Drawing.Size(53, 20);
            this.xTimeText.TabIndex = 6;
            this.xTimeText.Text = "0";
            this.xTimeText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // oTimeText
            // 
            this.oTimeText.Location = new System.Drawing.Point(493, 33);
            this.oTimeText.Name = "oTimeText";
            this.oTimeText.ReadOnly = true;
            this.oTimeText.Size = new System.Drawing.Size(53, 20);
            this.oTimeText.TabIndex = 7;
            this.oTimeText.Text = "0";
            this.oTimeText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // currentTextBox
            // 
            this.currentTextBox.Location = new System.Drawing.Point(674, 15);
            this.currentTextBox.Name = "currentTextBox";
            this.currentTextBox.ReadOnly = true;
            this.currentTextBox.Size = new System.Drawing.Size(28, 20);
            this.currentTextBox.TabIndex = 8;
            this.currentTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(578, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Current player:";
            // 
            // _panel
            // 
            this._panel.Location = new System.Drawing.Point(118, 75);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(550, 550);
            this._panel.TabIndex = 10;
            this._panel.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Paint);
            this._panel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this._panel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.currentTextBox);
            this.Controls.Add(this.oTimeText);
            this.Controls.Add(this.xTimeText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pauseGameButton);
            this.Controls.Add(this.loadGameButton);
            this.Controls.Add(this.saveGameButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newGameButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button newGameButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveGameButton;
        private System.Windows.Forms.Button loadGameButton;
        private System.Windows.Forms.Button pauseGameButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox xTimeText;
        private System.Windows.Forms.TextBox oTimeText;
        private System.Windows.Forms.TextBox currentTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel _panel;
    }
}

