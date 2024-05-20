using HymnsWithChords.Data;

namespace HymnalUI
{
	public partial class SdaHymnalUI : Form
	{
		public SdaHymnalUI()
		{
			InitializeComponent();
		}

		public void uploadFile_Click(object sender, EventArgs e)
		{
			statusTool.Text = "Loading...";
			OpenFileDialog fileDialog = new OpenFileDialog();

			//Help User by hiding irrelevant file types
			fileDialog.Filter = 
				"TextFiles (*.txt) |*.txt|Word Documents (*.doc, *.docx)|*.doc,*.docx|PDF Files (*.pdf)|*.pdf";
			

			if(fileDialog.ShowDialog() == DialogResult.OK)
			{
				string filePath = fileDialog.FileName;
				string fileExtension = Path.GetExtension(filePath).ToLower();
				List<string> lyrics = new List<string>();


				TextFileValidationAttribute validator = new 
					TextFileValidationAttribute( ".txt", ".doc", ".docx", ".pdf" );

				if(validator.IsValidFileType(filePath) )
				{
					//Extract the text
					switch (fileExtension)
					{
						case ".txt":
							lyrics =LyricExtractor.ExtractTxtFile(filePath); 
								break;
						case ".doc":
						case ".docx":
							lyrics = LyricExtractor.ExtractWordDocFile(filePath); 
								break;
						case ".pdf":
							lyrics= LyricExtractor.ExtractPdfLyricFile(filePath);
								break;
						default:
							MessageBox.Show("Unsupported file Type");
								break;
					}
					
					//Showing the results

					extractedText.Text = String.Join(Environment.NewLine, lyrics);
					statusTool.Text = "Ready";
				}
				else
				{
					MessageBox.Show("Invalid FileType. Please select a valid text, word, or pdf file");
					statusTool.Text = "ERROR";
				}

			}
		}
	}
}
