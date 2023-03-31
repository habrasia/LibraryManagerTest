Feature: GetABook
To ensure a book/books can be retrived from the library by given search criteria

Scenario: Get a book by its id
	Given there is a book with id "1" available
	When I send a request to retrive a book by id "1"
	Then the response status code should be 200
	And response message should contain a single book of the id "1"
	 
Scenario: Attempt to get a book by non-existing id return an error
	Given there is no book with id "99999" available
	When I send a request to retrive a book by id "99999"
	Then the response status code should be 400
	And an error message should be returned

Scenario: Get all books that contain the given word in the title
	Given there are books with titles that contain "Test" phrase in them available
	When I send a request to retrive books with "Test" phrase in the title
	Then the response status code should be 200
	And response message should contain a list of books where title contains "Test" word

Scenario: Attempt to get a book by non-existing title return an empty list
	Given there are no books with titles that contain "NonExistingBook" phrase in them available
	When I send a request to retrive books with "NonExistingBook" phrase in the title
	Then the response status code should be 400
	And an error message should be returned