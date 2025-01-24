using System.Collections.ObjectModel;
using SQLite;

namespace MyPlanner
{
    public partial class MainPage : ContentPage
    {
        public Note SelectedNote { get; set; }
        public ObservableCollection<Note> Notes { get; set; }= new ObservableCollection<Note>();
        public bool isEdited = false;
        private SQLiteAsyncConnection _connection;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            _connection = new SQLiteAsyncConnection(Path.Combine
                (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Planner.db"));
            InitializeDatabase();
        }

        private async Task InitializeDatabase()
        {
            await _connection.CreateTableAsync<Note>();
            await LoadNote();
        }

        private async Task LoadNote()
        {
            var notes = await _connection.Table<Note>().ToListAsync();
            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }

        private async void AddNewNote(object? sender, EventArgs e)
        {
            NotePage notePage = new NotePage(new Note());
            notePage.NoteSaved += NotePage_NoteSaved;
            await Navigation.PushModalAsync(notePage);
        }

        private async void NotePage_NoteSaved(object? sender, Note e)
        {
            SelectedNote = e;
            if (isEdited)
            {
                int id = Notes.IndexOf(e);
                Notes.RemoveAt(id);
                Notes.Insert(id, e);
                isEdited = false;
                await _connection.UpdateAsync(e);
            }
            else
            {
                Notes.Add(e);
                await _connection.InsertAsync(e);
            }
            
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            if (SelectedNote != null)
            {
                isEdited = true;
                NotePage notePage = new NotePage(SelectedNote);
                notePage.NoteSaved += NotePage_NoteSaved;
                await Navigation.PushModalAsync(notePage);
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if(SelectedNote != null)
            {
                await _connection.DeleteAsync(SelectedNote);
                Notes.Remove(SelectedNote);
            }
            
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Note> searchedNotes = new ObservableCollection<Note>();
            foreach(Note note in Notes)
            {
                if (note.NoteName.Contains(Searching.Text))
                {
                    searchedNotes.Add(note);
                }
            }
            NotesCollectionView.ItemsSource = searchedNotes;
            if(Searching.Text=="")
            {
                NotesCollectionView.ItemsSource = Notes;
            }
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SettingsPage());
        }
    }

}
