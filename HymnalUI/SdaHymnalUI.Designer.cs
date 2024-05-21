namespace HymnalUI
{
	partial class SdaHymnalUI
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
			extractedText = new TextBox();
			uploadFile = new Button();
			statusStrip1 = new StatusStrip();
			statusTool = new ToolStripStatusLabel();
			disposeButton = new Button();
			transposeButton = new Button();
			resetButton = new Button();
			transposeValue = new TextBox();
			inputOne = new TextBox();
			inputTwo = new TextBox();
			statusStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// extractedText
			// 
			extractedText.Location = new Point(89, 133);
			extractedText.Multiline = true;
			extractedText.Name = "extractedText";
			extractedText.ScrollBars = ScrollBars.Both;
			extractedText.Size = new Size(1040, 314);
			extractedText.TabIndex = 0;
			// 
			// uploadFile
			// 
			uploadFile.Location = new Point(89, 54);
			uploadFile.Name = "uploadFile";
			uploadFile.Size = new Size(163, 53);
			uploadFile.TabIndex = 1;
			uploadFile.Text = "Upload";
			uploadFile.UseVisualStyleBackColor = true;
			uploadFile.Click += uploadFile_Click;
			// 
			// statusStrip1
			// 
			statusStrip1.ImageScalingSize = new Size(24, 24);
			statusStrip1.Items.AddRange(new ToolStripItem[] { statusTool });
			statusStrip1.Location = new Point(0, 676);
			statusStrip1.Name = "statusStrip1";
			statusStrip1.Size = new Size(1200, 44);
			statusStrip1.TabIndex = 2;
			statusStrip1.Text = "statusStrip1";
			// 
			// statusTool
			// 
			statusTool.Font = new Font("Montserrat", 10.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
			statusTool.Name = "statusTool";
			statusTool.Size = new Size(85, 37);
			statusTool.Text = "Ready";
			// 
			// disposeButton
			// 
			disposeButton.Location = new Point(89, 499);
			disposeButton.Name = "disposeButton";
			disposeButton.Size = new Size(53, 53);
			disposeButton.TabIndex = 1;
			disposeButton.Text = "-";
			disposeButton.UseVisualStyleBackColor = true;
			disposeButton.Click += disposeButton_Click;
			// 
			// transposeButton
			// 
			transposeButton.Location = new Point(199, 499);
			transposeButton.Name = "transposeButton";
			transposeButton.Size = new Size(53, 53);
			transposeButton.TabIndex = 1;
			transposeButton.Text = "+";
			transposeButton.UseVisualStyleBackColor = true;
			transposeButton.Click += transposeButton_Click;
			// 
			// resetButton
			// 
			resetButton.Location = new Point(271, 499);
			resetButton.Name = "resetButton";
			resetButton.Size = new Size(53, 53);
			resetButton.TabIndex = 1;
			resetButton.Text = "@";
			resetButton.UseVisualStyleBackColor = true;
			resetButton.Click += resetButton_Click;
			// 
			// transposeValue
			// 
			transposeValue.Font = new Font("Montserrat", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
			transposeValue.Location = new Point(148, 499);
			transposeValue.Multiline = true;
			transposeValue.Name = "transposeValue";
			transposeValue.ReadOnly = true;
			transposeValue.Size = new Size(45, 53);
			transposeValue.TabIndex = 3;
			transposeValue.Text = "0";
			transposeValue.TextAlign = HorizontalAlignment.Center;
			// 
			// inputOne
			// 
			inputOne.Font = new Font("Montserrat", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
			inputOne.Location = new Point(355, 499);
			inputOne.Name = "inputOne";
			inputOne.ReadOnly = true;
			inputOne.Size = new Size(180, 42);
			inputOne.TabIndex = 3;
			inputOne.Text = "Bdim7";
			inputOne.TextAlign = HorizontalAlignment.Center;
			// 
			// inputTwo
			// 
			inputTwo.Font = new Font("Montserrat", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
			inputTwo.Location = new Point(582, 499);
			inputTwo.Name = "inputTwo";
			inputTwo.ReadOnly = true;
			inputTwo.Size = new Size(193, 42);
			inputTwo.TabIndex = 3;
			inputTwo.Text = "Gm7/E";
			inputTwo.TextAlign = HorizontalAlignment.Center;
			// 
			// SdaHymnalUI
			// 
			AutoScaleDimensions = new SizeF(15F, 40F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1200, 720);
			Controls.Add(inputTwo);
			Controls.Add(inputOne);
			Controls.Add(transposeValue);
			Controls.Add(statusStrip1);
			Controls.Add(resetButton);
			Controls.Add(transposeButton);
			Controls.Add(disposeButton);
			Controls.Add(uploadFile);
			Controls.Add(extractedText);
			Font = new Font("Montserrat", 11.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Margin = new Padding(4, 5, 4, 5);
			Name = "SdaHymnalUI";
			Text = "SDA Hymnal";
			Load += SdaHymnalUI_Load;
			statusStrip1.ResumeLayout(false);
			statusStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox extractedText;
		private Button uploadFile;
		private StatusStrip statusStrip1;
		private ToolStripStatusLabel statusTool;
		private Button disposeButton;
		private Button transposeButton;
		private Button resetButton;
		private TextBox transposeValue;
		private TextBox inputOne;
		private TextBox inputTwo;
	}
}
