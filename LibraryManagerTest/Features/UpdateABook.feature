Feature: UpdateABook
To ensure a book information can be updated

Scenario Outline: Successfully updating book's information
	Given I build a request with 'Author' set to '<Author>'
	And I build a request with 'Title' set to '<Title>' 
	And I build a request with 'Description' set to '<Description>'
	When I execute a post request to add a book to the corresponding library manager endpoint
	Then the response status code should be 200
	And response message should contain the added book information
Examples: 
	| Author | Title | Description |
	| Author | Title | Description |
	| Author | Title |             |

Scenario: Adding a book to the library is not possible when Author value extends allowed maximum characters length
	Given I build a request with 'Author' value that extends the maximum length of '30' characters
	And I build a request with 'Title' value that extends the maximum length of '100' characters
	When I execute a post request to add a book to the corresponding library manager endpoint
	Then the response status code should be 400
	And an error message should be returned

Scenario: Adding a book to the library is not possible when Title value extends allowed maximum characters length
	Given I build a request with 'Author' value that extends the maximum length of '30' characters
	And I build a request with 'Title' value that extends the maximum length of '100' characters
	When I execute a post request to add a book to the corresponding library manager endpoint
	Then the response status code should be 400
	And an error message should be returned
