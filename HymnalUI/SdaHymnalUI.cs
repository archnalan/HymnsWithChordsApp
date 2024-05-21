using HymnsWithChords.Data;

namespace HymnalUI
{
	public partial class SdaHymnalUI : Form
	{
		public SdaHymnalUI()
		{
			InitializeComponent();
		}

		public async void uploadFile_Click(object sender, EventArgs e)
		{
			statusTool.Text = "Loading...";
			OpenFileDialog fileDialog = new OpenFileDialog();

			//Help User by hiding irrelevant file types
			fileDialog.Filter =
				"TextFiles (*.txt) |*.txt|Word Documents (*.doc, *.docx)|*.doc,*.docx|PDF Files (*.pdf)|*.pdf";


			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				string filePath = fileDialog.FileName;
				string fileExtension = Path.GetExtension(filePath).ToLower();
				List<string> lyrics = new List<string>();


				TextFileValidationAttribute validator = new
					TextFileValidationAttribute(".txt", ".doc", ".docx", ".pdf");

				if (validator.IsValidFileType(filePath))
				{
					//Extract the text
					switch (fileExtension)
					{
						case ".txt":
							lyrics = await LyricExtractor.ExtractTxtFileAsync(filePath);
							break;
						case ".doc":
						case ".docx":
							lyrics = await LyricExtractor.ExtractWordDocAsync(filePath);
							break;
						case ".pdf":
							lyrics = await LyricExtractor.ExtractPdfAsyc(filePath);
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

		/////////////////////////////////////////////////////////////////////////////
		ChordTransposer transposer = new ChordTransposer();
		private void disposeButton_Click(object sender, EventArgs e)
		{
			statusTool.Text = "disposing...";

			int x = Convert.ToInt32(transposeValue.Text);
			int semitones = -1;
			transposeValue.Text = ((x + semitones + 12) % 12).ToString();			

			inputOne.Text = transposer.TransposeChord(inputOne.Text, semitones);

			inputTwo.Text = transposer.TransposeChord(inputTwo.Text, semitones);

			statusTool.Text = "Ready";
		}

		private void transposeButton_Click(object sender, EventArgs e)
		{
			statusTool.Text = "transposing...";
			int x = Convert.ToInt32(transposeValue.Text);
			int semitones = 1;
			transposeValue.Text = ((x + semitones + 12) % 12).ToString();

			inputOne.Text = transposer.TransposeChord(inputOne.Text, semitones);

			inputTwo.Text = transposer.TransposeChord(inputTwo.Text, semitones);

			statusTool.Text = "Ready";
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			statusTool.Text = "resetting...";
			string[] originalChords = transposer.ResetChords();

			inputOne.Text = originalChords[0];
			inputTwo.Text= originalChords[1];

			transposeValue.Text = "0";
			statusTool.Text = "Ready";
		}

		private void SdaHymnalUI_Load(object sender, EventArgs e)
		{
			string[] chords = { inputOne.Text, inputTwo.Text };
			
			transposer.StoreOriginalChords(chords);

		}
	}
}
