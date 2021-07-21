using System;

namespace JournalToGo.BookSearch
{
    public class BookSearcher
    {
        
        public bool ValidateSearch(string searchBook)
        {
            return !String.IsNullOrEmpty(searchBook);
        }
        
        public string SearchBook(string searchBook)
        {
            var bookService = new GoogleBooksService();
            var book = bookService.GetFirstBook(searchBook);
            return book.Id + " " + book.Link;
        }
    }
}