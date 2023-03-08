using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes
{
    static class DataBase
    {
        private static NoteContext notesContext;

        static DataBase()
        {
            notesContext = new NoteContext();
        }

        public static IEnumerable<Note> GetAll()
        {
            return notesContext.Notes.ToList();
        }

        public static Note GetNote(int noteID)
        {
            return notesContext.Notes.Find(noteID);
        }

        public static bool DeleteNote(int noteID)
        {
            var note = notesContext.Notes.Find(noteID);

            if (note != null)
            {
                notesContext.Notes.Remove(note);
                notesContext.SaveChanges();

                return true;
            }

            return false;
        }

        public static bool UpdateNote(int noteID, string newNoteDescription)
        {
            var note = notesContext.Notes.Find(noteID);

            if (note != null)
            {
                note.NoteDescription = newNoteDescription;
                notesContext.SaveChanges();

                return true;
            }

            return false;
        }

        public static bool AddNote(Note note)
        {
            try
            {
                notesContext.Notes.Add(note);
                notesContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
