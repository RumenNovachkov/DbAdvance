namespace P01_StudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Course
    {
        public Course()
        {
        }

        public Course(string name, string description, string startDate, string endDate, decimal price)
        {
            this.Name = name;
            this.Description = description;
            this.StartDate = DateTime.Parse(startDate);
            this.EndDate = DateTime.Parse(endDate);
            this.Price = price;
        }

        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public ICollection<StudentCourse> StudentsEnrolled { get; set; } = new List<StudentCourse>();

        public ICollection<Homework> HomeworkSubmissions { get; set; } = new List<Homework>();

        public ICollection<Resource> Resources { get; set; } = new List<Resource>();
    }
}
