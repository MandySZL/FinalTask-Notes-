using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

namespace Notes.Models;

internal class AllNotes
{
    public ObservableCollection<Notes> Notes { get; set; } = new ObservableCollection<Notes>();

    public AllNotes() =>
        LoadNotes();

    public void LoadNotes()
    {
        Notes.Clear();

        string appDataPath = FileSystem.AppDataDirectory;

        IEnumerable<Notes> notes = Directory

            .EnumerateFiles(appDataPath, "*.notes.txt")

            .Select(filename => new Notes()
            {
                Filename = filename,
                Text = File.ReadAllText(filename),
                Date = File.GetLastWriteTime(filename)
            })

            .OrderBy(note => note.Date);

        foreach (Notes note in notes)
            Notes.Add(note);

    }
}
