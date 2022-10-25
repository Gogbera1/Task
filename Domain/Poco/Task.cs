using Common.BaseModel;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Poco
{
    public class Task : ISoftDeletable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }

        public IdentityUser User { get; set; }

        public ICollection<File> Files { get; set; }
    }
}
