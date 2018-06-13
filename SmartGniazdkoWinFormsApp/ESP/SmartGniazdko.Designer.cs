namespace ESP
{
	partial class MainWindow
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
			if(disposing && (components != null))
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
            this.ipTextArea = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.przek1 = new System.Windows.Forms.Button();
            this.przek2 = new System.Windows.Forms.Button();
            this.polacz = new System.Windows.Forms.Button();
            this.Label_Port = new System.Windows.Forms.Label();
            this.MessageReader = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ipTextArea
            // 
            this.ipTextArea.Location = new System.Drawing.Point(59, 91);
            this.ipTextArea.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.ipTextArea.Name = "ipTextArea";
            this.ipTextArea.Size = new System.Drawing.Size(247, 38);
            this.ipTextArea.TabIndex = 0;
            this.ipTextArea.Text = "192.168.4.1";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(324, 91);
            this.port.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.port.MaxLength = 5;
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(180, 38);
            this.port.TabIndex = 1;
            this.port.Text = "1234";
            this.port.TextChanged += new System.EventHandler(this.port_TextChanged);
            // 
            // przek1
            // 
            this.przek1.Location = new System.Drawing.Point(59, 300);
            this.przek1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.przek1.Name = "przek1";
            this.przek1.Size = new System.Drawing.Size(460, 62);
            this.przek1.TabIndex = 2;
            this.przek1.Text = "Przekaznik #1";
            this.przek1.UseVisualStyleBackColor = true;
            this.przek1.Click += new System.EventHandler(this.przek1_Click);
            // 
            // przek2
            // 
            this.przek2.Location = new System.Drawing.Point(59, 394);
            this.przek2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.przek2.Name = "przek2";
            this.przek2.Size = new System.Drawing.Size(460, 62);
            this.przek2.TabIndex = 3;
            this.przek2.Text = "Przekaznik #2";
            this.przek2.UseVisualStyleBackColor = true;
            this.przek2.Click += new System.EventHandler(this.przek2_Click);
            // 
            // polacz
            // 
            this.polacz.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.polacz.Location = new System.Drawing.Point(59, 160);
            this.polacz.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.polacz.Name = "polacz";
            this.polacz.Size = new System.Drawing.Size(460, 78);
            this.polacz.TabIndex = 6;
            this.polacz.Text = "Polącz";
            this.polacz.UseVisualStyleBackColor = true;
            this.polacz.Click += new System.EventHandler(this.polacz_Click);
            // 
            // Label_Port
            // 
            this.Label_Port.AutoSize = true;
            this.Label_Port.Location = new System.Drawing.Point(318, 33);
            this.Label_Port.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.Label_Port.Name = "Label_Port";
            this.Label_Port.Size = new System.Drawing.Size(75, 32);
            this.Label_Port.TabIndex = 8;
            this.Label_Port.Text = "Port:";
            // 
            // MessageReader
            // 
            this.MessageReader.WorkerSupportsCancellation = true;
            this.MessageReader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MessageReader_DoWork);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 32);
            this.label1.TabIndex = 9;
            this.label1.Text = "Adres:";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 593);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Label_Port);
            this.Controls.Add(this.przek1);
            this.Controls.Add(this.przek2);
            this.Controls.Add(this.port);
            this.Controls.Add(this.ipTextArea);
            this.Controls.Add(this.polacz);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "MainWindow";
            this.Text = "SmartGniazdko";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox ipTextArea;
		private System.Windows.Forms.TextBox port;
		private System.Windows.Forms.Button przek1;
		private System.Windows.Forms.Button przek2;
		private System.Windows.Forms.Button polacz;
		private System.Windows.Forms.Label Label_Port;
		private System.ComponentModel.BackgroundWorker MessageReader;
        private System.Windows.Forms.Label label1;
    }
}

