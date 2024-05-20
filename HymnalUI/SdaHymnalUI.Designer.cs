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
			statusStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// extractedText
			// 
			extractedText.Location = new Point(89, 133);
			extractedText.Multiline = true;
			extractedText.Name = "extractedText";
			extractedText.ScrollBars = ScrollBars.Both;
			extractedText.Size = new Size(1040, 508);
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
			uploadFile.Click += this.uploadFile_Click;
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
			// SdaHymnalUI
			// 
			AutoScaleDimensions = new SizeF(15F, 40F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1200, 720);
			Controls.Add(statusStrip1);
			Controls.Add(uploadFile);
			Controls.Add(extractedText);
			Font = new Font("Montserrat", 11.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Margin = new Padding(4, 5, 4, 5);
			Name = "SdaHymnalUI";
			Text = "SDA Hymnal";
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
	}
}
