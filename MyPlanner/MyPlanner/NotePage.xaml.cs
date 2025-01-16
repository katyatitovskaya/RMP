namespace MyPlanner;

public partial class NotePage : ContentPage
{
    public Note CurrentNote { get; set; }
    public string Date {  get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public event EventHandler<Note> NoteSaved;
    public NotePage(Note note)
    {
        CurrentNote = note;
        Date = note.NoteDate;
        Title = note.NoteName;
        Description = note.NoteText;
        InitializeComponent();
		BindingContext = this;
	}

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }

    private async void SaveNoteButton_Clicked(object sender, EventArgs e)
    {
        CurrentNote.NoteDate = Date;
        CurrentNote.NoteName = Title;
        CurrentNote.NoteText = Description;
        NoteSaved?.Invoke(sender, CurrentNote);
        await Navigation.PopModalAsync();
    }
}