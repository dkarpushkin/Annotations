using AnnotationsAnalizer;

using System;
using System.Data.Entity;
using System.Linq;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Reflection;


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

        public static AnnotationInfo FromAttribute(object attr, Type type)
        {
            string author, reason, dateStr;
            try
            {
                author = (string)type.GetProperty("Author").GetValue(attr);
                reason = (string)type.GetProperty("Reason").GetValue(attr);
                dateStr = (string)type.GetProperty("Date").GetValue(attr);
            }
            catch { return null; }

            DateTime date = DateTime.Now;

            DateTime.TryParse(dateStr, out date);

            AnnotationInfo result = new AnnotationInfo()
            {
                Author = author,
                Reason = reason,
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
