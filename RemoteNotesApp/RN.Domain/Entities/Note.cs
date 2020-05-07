using RN.Domain.Common;

namespace RN.Domain.Entities
{
    public class Note : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        //public Note(int id, string title, string text, DateTime createTime)
        //{
        //    Id = id;
        //    Title = title;
        //    Text = text;
        //}
    }
}
