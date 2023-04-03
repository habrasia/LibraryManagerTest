using LibraryManagerTest.Helpers;
using LibraryManagerTest.Models;
using LibraryManagerTest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerTest.StepDefinitions
{
    [Binding]
    internal sealed class LibraryManagerStepDefinitions
    {
        private readonly IBookCRUD _bookCRUD;

        public LibraryManagerStepDefinitions()
        {
            _bookCRUD = new BookCRUD();
        }

        [Given(@"I build a request with Author set to '([^']*)', Title set to '([^']*)' and Description set to '([^']*)'")]
        public void GivenIBuildARequestWithAuthorSetToTitleSetToAndDescriptionSetTo(string author, string title, string description)
        {
            var book = new Book(author, title, description)
            {
                Title = "Test",
                Description = "Description",
                Author = "Author"
            };

        }

        [When(@"I execute a post request to add a book to the corresponding library manager endpoint")]
        public void WhenIExecuteAPostRequestToAddABookToTheCorrespondingLibraryManagerEndpoint()
        {
            
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int p0)
        {
            throw new PendingStepException();
        }

        [Then(@"response message should contain the added book information")]
        public void ThenResponseMessageShouldContainTheAddedBookInformation()
        {
            throw new PendingStepException();
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

        [Given(@"I build a request with '([^']*)' value that extends the maximum length of '([^']*)' characters")]
        public void GivenIBuildARequestWithValueThatExtendsTheMaximumLengthOfCharacters(string title, string p1)
        {
            throw new PendingStepException();
        }

    }
}
