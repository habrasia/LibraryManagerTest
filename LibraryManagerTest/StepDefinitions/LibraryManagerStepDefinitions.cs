using LibraryManagerTest.Helpers;
using LibraryManagerTest.Models;
using LibraryManagerTest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerTest.StepDefinitions
{
    [Binding]
    internal sealed class LibraryManagerStepDefinitions
    {
        private readonly IBookCRUD _bookCRUD;
        private Book _book;
        private HttpResponseMessage _response;
        private Book _bookResult;

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
        public void WhenIExecuteAPostRequestToAddABookToTheCorrespondingLibraryManagerEndpoint()
        {
            _response = _bookCRUD.AddBookAsync(_book).Result;
            _bookResult = _response.Content.ReadAsAsync<Book>().Result;
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int status)
        {
            switch(status)
            {
                case 200:
                    Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode, $"Status response failed for add {_book.ToString()}");
                    break;
            }
        }

        [Then(@"response message should contain the added book information")]
        public void ThenResponseMessageShouldContainTheAddedBookInformation()
        {
            Assert.That(_bookResult.Equals(_book), Is.True);
        }

        [Given(@"I build a request with '([^']*)' value that extends the maximum length of '([^']*)' characters")]
        public void GivenIBuildARequestWithValueThatExtendsTheMaximumLengthOfCharacters(string author, string p1)
        {
            throw new PendingStepException();
        }

        [Then(@"an error message should be returned")]
        public void ThenAnErrorMessageShouldBeReturned()
        {
            throw new PendingStepException();
        }
    }
}
