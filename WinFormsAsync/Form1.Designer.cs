namespace WinFormsApp1
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
            buttonSequencial = new Button();
            textBox1 = new TextBox();
            buttonSameTime = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            buttonCancel = new Button();
            buttonExample = new Button();
            label5 = new Label();
            buttonTestUnlocked = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // buttonSequencial
            // 
            buttonSequencial.Location = new Point(15, 162);
            buttonSequencial.Name = "buttonSequencial";
            buttonSequencial.Size = new Size(139, 60);
            buttonSequencial.TabIndex = 0;
            buttonSequencial.Text = "Pure Async / Await - run P2 only after P1 finished";
            buttonSequencial.UseVisualStyleBackColor = true;
            buttonSequencial.Click += buttonSequencial_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 37);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(513, 23);
            textBox1.TabIndex = 1;
            // 
            // buttonSameTime
            // 
            buttonSameTime.Location = new Point(188, 166);
            buttonSameTime.Name = "buttonSameTime";
            buttonSameTime.Size = new Size(140, 56);
            buttonSameTime.TabIndex = 2;
            buttonSameTime.Text = "Multi Async/Await - Run Both tasks at same time";
            buttonSameTime.UseVisualStyleBackColor = true;
            buttonSameTime.Click += buttonSameTime_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 88);
            label1.Name = "label1";
            label1.Size = new Size(110, 15);
            label1.TabIndex = 6;
            label1.Text = "Async Process One:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 113);
            label2.Name = "label2";
            label2.Size = new Size(109, 15);
            label2.TabIndex = 7;
            label2.Text = "Async Process Two:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(129, 88);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 8;
            label3.Text = "label3";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(129, 113);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 9;
            label4.Text = "label4";
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(524, 242);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(118, 29);
            buttonCancel.TabIndex = 10;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonExample
            // 
            buttonExample.Location = new Point(352, 228);
            buttonExample.Name = "buttonExample";
            buttonExample.Size = new Size(155, 57);
            buttonExample.TabIndex = 11;
            buttonExample.Text = "Task With Cancellation";
            buttonExample.UseVisualStyleBackColor = true;
            buttonExample.Click += buttonExample_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 19);
            label5.Name = "label5";
            label5.Size = new Size(121, 15);
            label5.TabIndex = 12;
            label5.Text = "Test unlock keyboard:";
            // 
            // buttonTestUnlocked
            // 
            buttonTestUnlocked.Location = new Point(389, 74);
            buttonTestUnlocked.Name = "buttonTestUnlocked";
            buttonTestUnlocked.Size = new Size(136, 29);
            buttonTestUnlocked.TabIndex = 13;
            buttonTestUnlocked.Text = "Test Unlocked Mouse";
            buttonTestUnlocked.UseVisualStyleBackColor = true;
            buttonTestUnlocked.Click += buttonTestUnlocked_Click;
            // 
            // button1
            // 
            button1.Location = new Point(352, 166);
            button1.Name = "button1";
            button1.Size = new Size(155, 56);
            button1.TabIndex = 14;
            button1.Text = "Task.Run";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(711, 355);
            Controls.Add(button1);
            Controls.Add(buttonTestUnlocked);
            Controls.Add(label5);
            Controls.Add(buttonExample);
            Controls.Add(buttonCancel);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonSameTime);
            Controls.Add(textBox1);
            Controls.Add(buttonSequencial);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonSequencial;
        private TextBox textBox1;
        private Button buttonSameTime;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button buttonCancel;
        private Button buttonExample;
        private Label label5;
        private Button buttonTestUnlocked;
        private Button button1;
    }
}