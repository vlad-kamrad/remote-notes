namespace RN.WebApi.DTO
{
    public class CreateNoteDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class UpdateNoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class NoteId
    {
        public int Id { get; set; }
    }
}
