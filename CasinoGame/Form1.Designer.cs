namespace CasinoGame
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textArea = new System.Windows.Forms.Label();
            this.inputMoney = new System.Windows.Forms.TextBox();
            this.chooseBetMoney = new System.Windows.Forms.Button();
            this.chooseRange = new System.Windows.Forms.Button();
            this.chooseColour = new System.Windows.Forms.Button();
            this.chooseRed = new System.Windows.Forms.Button();
            this.chooseBlack = new System.Windows.Forms.Button();
            this.start = new System.Windows.Forms.Button();
            this.endGame = new System.Windows.Forms.Button();
            this.chooseNumber = new System.Windows.Forms.TextBox();
            this.betButton = new System.Windows.Forms.Button();
            this.timerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textArea
            // 
            this.textArea.AutoSize = true;
            this.textArea.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textArea.Location = new System.Drawing.Point(453, 56);
            this.textArea.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.textArea.MaximumSize = new System.Drawing.Size(300, 130);
            this.textArea.MinimumSize = new System.Drawing.Size(300, 130);
            this.textArea.Name = "textArea";
            this.textArea.Size = new System.Drawing.Size(300, 130);
            this.textArea.TabIndex = 0;
            this.textArea.Text = "Сумма ставки?";
            this.textArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.textArea.Click += new System.EventHandler(this.label1_Click);
            // 
            // inputMoney
            // 
            this.inputMoney.Location = new System.Drawing.Point(425, 198);
            this.inputMoney.Name = "inputMoney";
            this.inputMoney.Size = new System.Drawing.Size(361, 31);
            this.inputMoney.TabIndex = 1;
            this.inputMoney.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // chooseBetMoney
            // 
            this.chooseBetMoney.Location = new System.Drawing.Point(523, 254);
            this.chooseBetMoney.Name = "chooseBetMoney";
            this.chooseBetMoney.Size = new System.Drawing.Size(112, 34);
            this.chooseBetMoney.TabIndex = 2;
            this.chooseBetMoney.Text = "выбрать";
            this.chooseBetMoney.UseVisualStyleBackColor = true;
            this.chooseBetMoney.Click += new System.EventHandler(this.button1_Click);
            // 
            // chooseRange
            // 
            this.chooseRange.Location = new System.Drawing.Point(409, 254);
            this.chooseRange.Name = "chooseRange";
            this.chooseRange.Size = new System.Drawing.Size(183, 34);
            this.chooseRange.TabIndex = 3;
            this.chooseRange.Text = "Выбрать: 1- 152";
            this.chooseRange.UseVisualStyleBackColor = true;
            this.chooseRange.Visible = false;
            this.chooseRange.Click += new System.EventHandler(this.button2_Click);
            // 
            // chooseColour
            // 
            this.chooseColour.Location = new System.Drawing.Point(641, 254);
            this.chooseColour.Name = "chooseColour";
            this.chooseColour.Size = new System.Drawing.Size(158, 34);
            this.chooseColour.TabIndex = 4;
            this.chooseColour.Text = "Выбрать цвет";
            this.chooseColour.UseVisualStyleBackColor = true;
            this.chooseColour.Visible = false;
            this.chooseColour.Click += new System.EventHandler(this.button3_Click);
            // 
            // chooseRed
            // 
            this.chooseRed.Location = new System.Drawing.Point(425, 254);
            this.chooseRed.Name = "chooseRed";
            this.chooseRed.Size = new System.Drawing.Size(112, 34);
            this.chooseRed.TabIndex = 5;
            this.chooseRed.Text = "красный";
            this.chooseRed.UseVisualStyleBackColor = true;
            this.chooseRed.Visible = false;
            this.chooseRed.Click += new System.EventHandler(this.button4_Click);
            // 
            // chooseBlack
            // 
            this.chooseBlack.Location = new System.Drawing.Point(641, 254);
            this.chooseBlack.Name = "chooseBlack";
            this.chooseBlack.Size = new System.Drawing.Size(112, 34);
            this.chooseBlack.TabIndex = 6;
            this.chooseBlack.Text = "чёрный";
            this.chooseBlack.UseVisualStyleBackColor = true;
            this.chooseBlack.Visible = false;
            this.chooseBlack.Click += new System.EventHandler(this.button5_Click);
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(523, 308);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(112, 34);
            this.start.TabIndex = 7;
            this.start.Text = "СТАРТ";
            this.start.UseVisualStyleBackColor = true;
            this.start.Visible = false;
            this.start.Click += new System.EventHandler(this.button6_Click);
            // 
            // endGame
            // 
            this.endGame.Location = new System.Drawing.Point(523, 429);
            this.endGame.Name = "endGame";
            this.endGame.Size = new System.Drawing.Size(112, 34);
            this.endGame.TabIndex = 8;
            this.endGame.Text = "Закончить";
            this.endGame.UseVisualStyleBackColor = true;
            this.endGame.Visible = false;
            this.endGame.Click += new System.EventHandler(this.button7_Click);
            // 
            // chooseNumber
            // 
            this.chooseNumber.Location = new System.Drawing.Point(508, 198);
            this.chooseNumber.Name = "chooseNumber";
            this.chooseNumber.Size = new System.Drawing.Size(150, 31);
            this.chooseNumber.TabIndex = 10;
            this.chooseNumber.Visible = false;
            this.chooseNumber.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // betButton
            // 
            this.betButton.Location = new System.Drawing.Point(523, 254);
            this.betButton.Name = "betButton";
            this.betButton.Size = new System.Drawing.Size(112, 34);
            this.betButton.TabIndex = 11;
            this.betButton.Text = "ставка";
            this.betButton.UseVisualStyleBackColor = true;
            this.betButton.Visible = false;
            this.betButton.Click += new System.EventHandler(this.button9_Click);
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Location = new System.Drawing.Point(508, 367);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(0, 25);
            this.timerLabel.TabIndex = 12;
            this.timerLabel.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1286, 672);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.betButton);
            this.Controls.Add(this.chooseNumber);
            this.Controls.Add(this.endGame);
            this.Controls.Add(this.start);
            this.Controls.Add(this.chooseBlack);
            this.Controls.Add(this.chooseRed);
            this.Controls.Add(this.chooseColour);
            this.Controls.Add(this.chooseRange);
            this.Controls.Add(this.chooseBetMoney);
            this.Controls.Add(this.inputMoney);
            this.Controls.Add(this.textArea);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label textArea;
        private TextBox inputMoney;
        private Button chooseBetMoney;
        private Button chooseRange;
        private Button chooseColour;
        private Button chooseRed;
        private Button chooseBlack;
        private Button start;
        private Button endGame;
        private TextBox chooseNumber;
        private Button betButton;
        private Label timerLabel;
    }
}