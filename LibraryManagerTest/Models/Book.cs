﻿using System;
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
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


        public override string ToString()
        {
            return $"Book Id {Id}, Title: {Title}";
        }

        public Book()
        {
        }

        public Book(string author, string title, string description)
        {
            Id = Interlocked.Increment(ref globalBookId);
            Author = author;
            Title = title;
            Description = description;
        }

        public Book(int id, string author, string title, string description)
        {
            Id = id;
            Author = author;
            Title = title;
            Description = description;
        }
    }
}
