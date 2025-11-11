namespace Notes.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NotePage : ContentPage
    {
        public string ItemId
        {
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    LoadNote(value);
                }
            }
        }

        public NotePage()
        {
            InitializeComponent();
            BindingContext = new Models.Notes();
        }

        void LoadNote(string fileName)
        {
            var note = new Models.Notes
            {
                Filename = fileName,
                Text = File.Exists(fileName) ? File.ReadAllText(fileName) : string.Empty,
                Date = File.Exists(fileName) ? File.GetLastWriteTime(fileName) : DateTime.Now
            };

            BindingContext = note;
        }

        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var note = (Models.Notes)BindingContext;

            if (string.IsNullOrWhiteSpace(note.Filename))
            {
                string appDataPath = FileSystem.AppDataDirectory;
                string fileName = Path.Combine(appDataPath, $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(fileName, note.Text ?? string.Empty);
            }
            else
            {
                File.WriteAllText(note.Filename, note.Text ?? string.Empty);
            }

            await Shell.Current.GoToAsync("..");
        }

        async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var note = (Models.Notes)BindingContext;

            if (!string.IsNullOrWhiteSpace(note.Filename) && File.Exists(note.Filename))
            {
                File.Delete(note.Filename);
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
