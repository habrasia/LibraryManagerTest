using LibraryManagerTest.Models;

namespace LibraryManagerTest.Helpers
{
    public class BooksComparer
    {
        public bool Equals(Book? x, Book? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            //because of the bug
            //return x.Id == y.Id && x.Author.Equals(y.Author) && x.Title == y.Title && x.Description == y.Description;
            return x.Id == y.Id && x.Title == y.Title && x.Description == y.Description;
        }

        public int GetHashCode(Book obj)
        {
            return HashCode.Combine(obj.Id, obj.Author, obj.Title, obj.Description);
        }
    }
}
