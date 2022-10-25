using Common.BaseModel;

namespace Domain.Poco
{
    public class File : ISoftDeletable
    {
        public int Id { get; set; }
        public string FileContent { get; set; }
        public int TaskId { get; set; }
        public bool IsDeleted { get; set; }
        public Task Task { get; set; }
    }
}
