using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlanner
{
    public class Note
    {
        public string NoteName { get; set; }
        public string NoteText { get; set; }

        [PrimaryKey]
        public string NoteDate { get; set; }
        public Note(string name, string noteText)
        {
            NoteName = name;
            NoteText = noteText;
            NoteDate = DateTime.Now.ToString();
        }
        public Note()
        {
            NoteDate = DateTime.Now.ToString();
        }
    }
}
