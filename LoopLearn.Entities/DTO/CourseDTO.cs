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
        public string Avatar { get; set; }
        [Required]
        [CourseTitle]
        public string Title { get; set; }

        [Required]
        [Category]
        public string Category { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 50, ErrorMessage = "Description must be between 50 and 5000 characters.")]
        public string Description { get; set; }

        [Required]
        public Level Level { get; set; }

        [Required]
        public int Duration { get; set; }
    }

}
