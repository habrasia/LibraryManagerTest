Feature: DeleteABook

Scenario: Delete a book with the given id
	Given there is a book with id '5' available
	When I send a request to delete a book by id '5'
	Then the response status code should be 204
	When I send a request to retrive a book by id '5'
	Then the response status code should be 404
	And an error message should be returned

	 
Scenario: Attempt to delete a book that doesn't exist return an error
	Given there is no book with id '99999' available
	When I send a request to delete a book by id '9999'
	Then the response status code should be 400
	And an error message should be returned