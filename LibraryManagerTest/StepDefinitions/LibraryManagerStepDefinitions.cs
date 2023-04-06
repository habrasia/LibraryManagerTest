using LibraryManagerTest.Helpers;
using LibraryManagerTest.Models;
using LibraryManagerTest.Repositories;
using System.Net;
using System.Text.Json;
using System.Web.Http;
using TechTalk.SpecFlow.CommonModels;

namespace LibraryManagerTest.StepDefinitions
{
    [Binding]
    internal sealed class LibraryManagerStepDefinitions
    {
        private readonly IBookCRUD _bookCRUD;
        private HttpResponseMessage _response;
        private Book _book;
        private Book _bookResult;
        private List<Book> _booksListResult;

        public LibraryManagerStepDefinitions()
        {
            _bookCRUD = new BookCRUD();
        }

        [Given(@"I build a request with Author set to '([^']*)', Title set to '([^']*)' and Description set to '([^']*)'")]
        public void GivenIBuildARequestWithAuthorSetToTitleSetToAndDescriptionSetTo(string author, string title, string description)
        {
            _book = new Book(author, title, description);
        }

        [When(@"I execute a post request to add a book to the corresponding library manager endpoint")]
        [When(@"I execute a post request to add a book to the corresponding library manager endpoint again")]
        public void WhenIExecuteAPostRequestToAddABookToTheCorrespondingLibraryManagerEndpoint()
        {
            _response = _bookCRUD.AddBookAsync(_book).Result;
            _bookResult = _response.Content.ReadAsAsync<Book>().Result;
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int status)
        {
            switch (status)
            {
                case 200:
                    Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode, $"Status response failed for status check 200");
                    break;
                case 204:
                    Assert.AreEqual(HttpStatusCode.NoContent, _response.StatusCode, $"Status response failed status check 204");
                    break;
                case 404:
                    Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode, $"Status response failed status check 404");
                    break;
            }
        }

        [Then(@"response message should contain the added book information")]
        [Then(@"response message should contain the updated book information")]
        public void ThenResponseMessageShouldContainTheAddedBookInformation()
        {
            Assert.AreEqual(_book.Id, _bookResult.Id, $"Id check failed for add {_book.ToString()}");
            Assert.AreEqual(_book.Author, _bookResult.Author, $"Author check failed for add {_book.ToString()}");
            Assert.AreEqual(_book.Title, _bookResult.Title, $"Title check failed for add {_book.ToString()}");
            Assert.AreEqual(_book.Description, _bookResult.Description, $"Description check failed for add {_book.ToString()}");
        }

        [Given(@"I build a request with '([^']*)' value that exceeds the maximum length of (.*) characters")]
        public void GivenIBuildARequestWithValueThatExtendsTheMaximumLengthOfCharacters(string value, int lenght)
        {
            var author = "Author";
            var title = "Title";
            var description = "Description";

            switch (value)
            {
                case "Author":
                    author = StringGenerator.GenerateStringOfGivenLength(lenght);
                    break;
                case "Title":
                    title = StringGenerator.GenerateStringOfGivenLength(lenght);
                    break;
            }

            _book = new Book(author, title, description);
        }

        [Then(@"an error message should be returned")]
        public void ThenErrorMessageShouldBeReturned()
        {
            string errorMessageString = _response.Content.ReadAsStringAsync().Result;
            Error? error = JsonSerializer.Deserialize<Error>(errorMessageString);

            Assert.IsNotNull(error.Message, $"Error message check failed");
        }

        [Given(@"there is a book with id '(.*)' available")]
        public void GivenThereIsABookWithIdAvailable(int id)
        {
            if (!_bookCRUD.GetBookByIdAsync(id).Result.IsSuccessStatusCode)
            {
                var author = "AuthorIdTest";
                var title = "TitleIdTest";
                var description = "DescriptionIdTest";

                _book = new Book(id, author, title, description);

                Assert.IsTrue(_bookCRUD.AddBookAsync(_book).Result.IsSuccessStatusCode);
            }
        }

        [When(@"I send a request to retrive a book by id '(.*)'")]
        public void WhenISendARequestToRetriveABookById(int id)
        {
            _response = _bookCRUD.GetBookByIdAsync(id).Result;
            _bookResult = _response.Content.ReadAsAsync<Book>().Result;
        }

        [Then(@"response message should contain a single book with id '(.*)'")]
        public void ThenResponseMessageShouldContainASingleBookWithId(int id)
        {
            Assert.AreEqual(id, _bookResult.Id, $"Id check failed");
        }

        [Given(@"there is no book with id '(.*)' available")]
        public void GivenThereIsNoBookWithIdAvailable(int id)
        {
            if (_bookCRUD.GetBookByIdAsync(id).Result.IsSuccessStatusCode)
            {
                _bookCRUD.DeleteBookAsync(id);
            }
        }

        [Given(@"there are books with titles that contain '([^']*)' phrase in them available")]
        public void GivenThereAreBooksWithTitlesThatContainPhraseInThemAvailable(string title)
        {
            _response = _bookCRUD.GetBooksByTitleAsync(title).Result;
            _booksListResult = _response.Content.ReadAsAsync<List<Book>>().Result;

            if (_booksListResult.Count == 0)
            {
                var book1 = new Book()
                {
                    Id = 20,
                    Title = $"{title}20",
                    Author = "Author"
                };
                var book2 = new Book()
                {
                    Id = 21,
                    Title = $"{title}21",
                    Author = "Author"
                };
                Assert.IsTrue(_bookCRUD.AddBookAsync(book1).Result.IsSuccessStatusCode);
                Assert.IsTrue(_bookCRUD.AddBookAsync(book2).Result.IsSuccessStatusCode);
            }
        }

        [When(@"I send a request to retrive books with '([^']*)' phrase in the title")]
        public void WhenISendARequestToRetriveBooksWithPhraseInTheTitle(string title)
        {
            _response = _bookCRUD.GetBooksByTitleAsync(title).Result;
            _booksListResult = _response.Content.ReadAsAsync<List<Book>>().Result;
        }

        [Then(@"response message should contain a list of books where title contains '([^']*)' word")]
        public void ThenResponseMessageShouldContainAListOfBooksWhereTitleContainsWord(string title)
        {
            Assert.IsNotEmpty(_booksListResult);
            Assert.AreEqual(_booksListResult.Count, _booksListResult.Where(o => o.Title.Contains("test")).Count());
        }

        [Given(@"there are no books with titles that contain '([^']*)' phrase in them available")]
        public void GivenThereAreNoBooksWithTitlesThatContainPhraseInThemAvailable(string title)
        {
            _response = _bookCRUD.GetBooksByTitleAsync(title).Result;
            _booksListResult = _response.Content.ReadAsAsync<List<Book>>().Result;

            if (_booksListResult.Count != 0)
            {
                foreach (var book in _booksListResult)
                {
                    _bookCRUD.DeleteBookAsync(book.Id);
                }
            }
        }

        [Then(@"response message should contain an empty list")]
        public void ThenResponseMessageShouldContainAnEmptyList()
        {
            Assert.IsEmpty(_booksListResult);
        }

        [When(@"I send a request to delete a book by id '(.*)'")]
        [When(@"I send a request to delete a book by id '(.*)' again")]
        public void WhenISendARequestToDeleteABookById(int id)
        {
            _response = _bookCRUD.DeleteBookAsync(id).Result;
        }

        [Given(@"I build an update request for a book with id '(.*)' with Author set to '([^']*)', Title set to '([^']*)' and Description set to '([^']*)'")]
        public void GivenIBuildAnUpdateRequestForABookWithIdWithAuthorSetToTitleSetToAndDescriptionSetTo(int id, string changedAuthor, string changedTitle, string changedDescription)
        {
            _book = new Book(id, changedAuthor, changedTitle, changedDescription);
        }

        [Given(@"I build a request for a book with id '(.*)' with '([^']*)' value that extends the maximum length of (.*) characters")]
        public void GivenIBuildARequestForABookWithIdWithValueThatExtendsTheMaximumLengthOfCharacters(int id, string value, int lenght)
        {

            var author = "Author";
            var title = "Title";
            var description = "Description";

            switch (value)
            {
                case "Author":
                    author = StringGenerator.GenerateStringOfGivenLength(lenght);
                    break;
                case "Title":
                    title = StringGenerator.GenerateStringOfGivenLength(lenght);
                    break;
            }

            _book = new Book(id, author, title, description);
        }
       

        [When(@"I execute an update request to update the books of id '([^'].*)' information to the corresponding library manager endpoint")]
        public void WhenIExecuteAnUpdateRequestToUpdateTheBooksOfIdInformationToTheCorrespondingLibraryManagerEndpoint(int id)
        {
            _response = _bookCRUD.UpdateBookAsync(id, _book).Result;
            _bookResult = _response.Content.ReadAsAsync<Book>().Result;
        }
    }
}