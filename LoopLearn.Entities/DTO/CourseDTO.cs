using LoopLearn.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LoopLearn.Entities.utils.Enums;

namespace LoopLearn.Entities.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Avatar {  get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public string InstructorName { get; set; }

    }

    public class CourseDetailsDTO : CourseDTO
    {
        public string InstructorAvatar { get; set; }
        public string InstructorBio { get; set; }
        public string Description { get; set; }
        public Level Level { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public List<LessonDTO> Lessons { get; set; } = new List<LessonDTO>();
        public List<CommentsDTO> Comments { get; set; } = new List<CommentsDTO>();
    }
    public class CreateCourseDTO
    {
        [Required]
        public int InstructorId { get; set; }
        public string Avatar { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Category { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [MaxLength(4000, ErrorMessage = "Description cannot exceed 4000 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Description { get; set; }

        [Required]
        public Level Level { get; set; }

        [Required]
        public int Duration { get; set; }
    }

}
