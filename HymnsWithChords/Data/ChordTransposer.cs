using System;
using System.Collections.Generic;
using System.Linq;

namespace HymnsWithChords.Data
{
	public class ChordTransposer
	{
		private readonly Dictionary<string, int> _sharpChromaticScale;
		private readonly Dictionary<string, int> _flatChromaticScale;

		private List<string> _originalChords;
		public ChordTransposer()
		{
			_sharpChromaticScale = new Dictionary<string, int>
			{
				{ "C", 0},{"C#",1},{"D",2},{"D#",3}, {"E",4},{"F",5}, {"F#",6},
				{"G",7 },{"G#",8},{"A",9},{"A#",10},{"B",11}
			};
			_flatChromaticScale = new Dictionary<string, int>
			{
				{ "C", 0},{"Db",1},{"D",2},{"Eb",3}, {"E",4},{"F",5}, {"Gb",6},
				{"G",7 },{"Ab",8},{"A",9},{"Bb",10},{"B",11}
			};

			_originalChords = new List<string>();

		}

		public void StoreOriginalChords(string[] chords)
		{
			_originalChords = chords.ToList();
		}

		public string[] ResetChords()
		{
			return _originalChords.ToArray();
		}

		public string[] TransposeChords(int semitones)
		{
			return _originalChords.Select(chord => TransposeChord(chord, semitones)).ToArray();
		}

		public string TransposeChord(string chord, int semitones)
		{			
			chord = chord.Trim().Replace(" ", "");//Remove any spaces

			var match = System.Text.RegularExpressions
				.Regex.Match(chord, @"^([A-G])(#|b|##|bb)?(m|maj|min|sus|aug|dim|add)?(\d+)?(/([A-G])(#|b)?)?$"
);
			if(!match.Success) return chord;

			var rootNote = match.Groups[1].Value + match.Groups[2].Value;
			var chordQuality = match.Groups[3].Value + match.Groups[4];
			var bassNote = (string.IsNullOrEmpty(match.Groups[5].Value) == false) ? 
				match.Groups[5].Value.TrimStart('/'):string.Empty;

			//call the transposeNote method
			var transposedRoot = TransposeNote(rootNote, semitones);
			var transposedBass = !string.IsNullOrEmpty(bassNote)? 
				TransposeNote(bassNote, semitones): string.Empty;

			return $"{transposedRoot}{chordQuality}{(string.IsNullOrEmpty(transposedBass)?"":"/"+ transposedBass)}";
		}

		public string TransposeNote(string note, int semitones)
		{
			//Determine which scale was used
			var scale = _sharpChromaticScale.ContainsKey(note) ?
				_sharpChromaticScale : _flatChromaticScale;

			//Get and manipulate the note in scale
			var noteIndex = scale[note];
			if (noteIndex == -1) return note;
			var newIndex = (noteIndex + semitones + 12) % 12;

			var newNote = scale.FirstOrDefault(x => x.Value == newIndex).Key;
			return newNote;
		}
	}
}
