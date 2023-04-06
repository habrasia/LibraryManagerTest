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

Scenario Outline: Attempt to add a book without required fields isn't supported
	Given I build a request with Author set to '<Author>', Title set to '<Title>' and Description set to '<Description>'
	When I execute a post request to add a book to the corresponding library manager endpoint
	Then the response status code should be 400
	And an error message should be returned
Examples: 
	| Author | Title | Description |
	|        | Title | Description |
	| Author |       | Description |


Scenario Outline: Adding a book to the library when values lenghts are bellow maximum allowed characters length
	Given I build a request with '<Field>' value that exceeds the maximum length of <NumberOfCharacters> characters
	When I execute a post request to add a book to the corresponding library manager endpoint
	Then the response status code should be 200
Examples: 
	| Field  | NumberOfCharacters |
	| Author | 30                 |
	| Title  | 100                |


Scenario Outline: Adding a book to the library is not possible when values lenghts extend maximum allowed characters length
	Given I build a request with '<Field>' value that exceeds the maximum length of <NumberOfCharacters> characters
	When I execute a post request to add a book to the corresponding library manager endpoint
	Then the response status code should be 400
	And an error message should be returned
Examples: 
	| Field  | NumberOfCharacters |
	| Author | 31                 |
	| Title  | 101                |

Scenario: Attempt to add a book for the second time
	Given I build a request with Author set to '<Author>', Title set to '<Title>' and Description set to '<Description>'
	When I execute a post request to add a book to the corresponding library manager endpoint
	And I execute a post request to add a book to the corresponding library manager endpoint again
	Then the response status code should be 400
	And an error message should be returned
