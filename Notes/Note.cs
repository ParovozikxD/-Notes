using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    public class Note
    {
        public int NoteID {  get; set; }

        public string NoteName { get; set; }

        public string NoteDescription { get; set; }

        public Note() { }

        public Note (string noteName, string noteDescription)
        {
            NoteName = noteName;
            NoteDescription = noteDescription;
        }
    }
}
