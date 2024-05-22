using HymnsWithChords.Controllers;
using HymnsWithChords.Data;
using HymnsWithChords.Interfaces;

namespace HymnalUI
{
	public partial class SdaHymnalUI : Form
	{
		private readonly LyricExtractionController _lyricExtract;
		public SdaHymnalUI(LyricExtractionController lyricExtract)
		{
			InitializeComponent();
			_lyricExtract = lyricExtract;
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
				try
				{	//Extract the text
					List<string> lyrics = await _lyricExtract.GetLyricsAsync(filePath);					

					//Showing the results

					extractedText.Text = String.Join(Environment.NewLine, lyrics);
					statusTool.Text = "Ready";
				}
				catch (NotSupportedException ex) 
				{
					MessageBox.Show($"Error: {ex.Message}");
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
