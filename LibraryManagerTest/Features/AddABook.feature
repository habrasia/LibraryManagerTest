Feature: AddABook
To ensure a book can be added to the library

Scenario Outline: Successfully adding a book to the library
	Given I build a request with Author set to '<Author>', Title set to '<Title>' and Description set to '<Description>'
	When I execute a post request to add a book to the corresponding library manager endpoint
	Then the response status code should be 200
	And response message should contain the added book information
Examples: 
	| Author | Title | Description |
	| Author | Title | Description |
	| Author | Title |             |

Scenario Outline: Adding a book to the library when values lenghts are bellow allowed maximum characters length
	Given I build a request with '<Field>' value that extends the maximum length of <NumberOfCharacters> characters
	When I execute a post request to add a book to the corresponding library manager endpoint
	Then the response status code should be 200

Examples: 
	| Field  | NumberOfCharacters |
	| Author | 30                 |
	| Title  | 100                |

Scenario Outline: Adding a book to the library is not possible when values lenghts extend allowed maximum characters length
	Given I build a request with '<Field>' value that extends the maximum length of <NumberOfCharacters> characters
	When I execute a post request to add a book to the corresponding library manager endpoint
	Then the response status code should be 400
	And an error message should be returned

Examples: 
	| Field  | NumberOfCharacters |
	| Author | 31                 |
	| Title  | 101                |