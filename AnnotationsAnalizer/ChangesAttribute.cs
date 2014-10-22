using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnotationsAnalizer
{
    public sealed class ChangesAttribute : Attribute
    {
        public string Date { get; set; }
        public string Reason { get; set; }
        public string Author { get; set; }

        public ChangesAttribute(string author, string reason, string date)
        {
            Author = author;
            Reason = reason;
            Date = date;
        }
    }
}
