using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerTest.Models
{
    public class Book
    {
        public static int globalBookId;

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public override string ToString()
        {
            return $"Book Id {Id}, Title: {Title}";
        }

        public Book(string title, string description, string author)
        {
            Id = Interlocked.Increment(ref globalBookId);
            Title = title;
            Description = description;
            Author = author;
        }

        public Book(int id, string title, string description, string author)
        {
            Id = Id;
            Title = title;
            Description = description;
            Author = author;
        }
    }
}
