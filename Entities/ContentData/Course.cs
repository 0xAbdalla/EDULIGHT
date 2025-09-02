using EDULIGHT.Entities.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EDULIGHT.Entities.ContentData
{
    //public  List<string> Level = new List<string>{ "Beginner", "Intermediate", "Advanced" }; 
    public enum Level
    {
        [EnumMember(Value = "Beginner")]
        Beginner,
        [EnumMember(Value = "Intermediate")]
        Intermediate,
        [EnumMember(Value = "Advanced")]
        Advanced
    }
    public enum Language
    {
        [EnumMember(Value = "Arabic")]
        Arabic,
        [EnumMember(Value = "English")]
        English

    }

    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public IFormFile Poster { get; set; }
        public string PosterURL { get; set; }
        public int Duration { get; set; }
        public Level Level { get; set; }
        public Language Language { get; set; }
        public string WelcomeMessage { get; set; }
        public string CongratulationMessage { get; set; }
        public DateTime Created_at => DateTime.Now;
        public ICollection<Sections> sections { get; set; } = new List<Sections>();
        public ICollection<Review>? Review { get; set; } = new List<Review>();

        [ForeignKey("Category")]
        public int category_id { get; set; }
        public Category Category { get; set; }
        [ForeignKey("Instructor")]
        public string InstructorId { get; set; }
        [ForeignKey("Roadmap")]
        public int roadmap_id { get; set; }
        public Roadmap Roadmap { get; set; }


    }
}
