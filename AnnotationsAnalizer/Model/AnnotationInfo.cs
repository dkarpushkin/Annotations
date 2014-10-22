using AnnotationsAnalizer;

using System;
using System.Data.Entity;
using System.Linq;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;


namespace AnnotationsAnalizer.Model
{
    public class AnnotationInfo
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Reason { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateOfEntry { get; set; }

        public MemberAnnotationInfo CodeUnt { get; set; }

        public static AnnotationInfo FromAttribute(AnnotationsAnalizer.ChangesAttribute attr)
        {
            DateTime date = DateTime.Now;

            DateTime.TryParse(attr.Date, out date);

            AnnotationInfo result = new AnnotationInfo()
            {
                Author = attr.Author,
                Reason = attr.Reason,
                Date = date,
                DateOfEntry = DateTime.Now
            };

            return result;
        }
    }

    /// <summary>
    /// Класс или метод или еще что-то
    /// </summary>
    public class MemberAnnotationInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }

    public class AnnotationInfoContext : DbContext
    {
        public AnnotationInfoContext()
        {
            
        }

        public DbSet<AnnotationInfo> Annotations { get; set; }
        public DbSet<MemberAnnotationInfo> Members { get; set; }

    }
}
