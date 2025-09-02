using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EDULIGHT.Entities.ContentData
{
    public enum ContentType
    {
        [EnumMember(Value = "vedio")]
        vedio,
        [EnumMember(Value = "Attach File")]
        AttachFile
    }
    public class ContentCourse
    {
        [Key]
        public int Content_Id { get; set; }
        public string ContentTitle { get; set; }
        public ContentType ContentType { get; set; }
        [NotMapped]
        public IFormFile Content { get; set; }
        public string ContentUrl { get; set; }
        public DateTime Created_at => DateTime.Now;
        [ForeignKey(nameof(Sections))]
        public int SectionId { get; set; }
        public Sections Section { get; set; }
    }
}
