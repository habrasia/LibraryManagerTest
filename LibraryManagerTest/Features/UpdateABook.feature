Feature: UpdateABook
To ensure a book information can be updated

Scenario Outline: Successfully updating book's information
	Given there is a book with id '5' available
	And I build an update request for a book with id '5' with Author set to '<Author>', Title set to '<Title>' and Description set to '<Description>'
	When I execute an update request to update the books of id '5' information to the corresponding library manager endpoint
	Then the response status code should be 200
	And response message should contain the added book information

Examples: 
	| Author        | Title	       | Description        |
	| ChangedAuthor | ChangedTitle | ChangedDescription |
	| ChangedAuthor | ChangedTitle |                    |

Scenario Outline: Updating a book's information when new values lenghts are bellow allowed maximum characters length
	Given there is a book with id '5' available
	And I build a request for a book with id '5' with '<Field>' value that extends the maximum length of <NumberOfCharacters> characters
	When I execute an update request to update the books of id '4' information to the corresponding library manager endpoint
	Then the response status code should be 200

Examples: 
	| Field  | NumberOfCharacters |
	| Author | 30                 |
	| Title  | 100                |

Scenario Outline: Updating a book's information when new values lenghts extend allowed maximum characters length
	Given there is a book with id '5' available
	And I build a request for a book with id '5' with '<Field>' value that extends the maximum length of <NumberOfCharacters> characters
	When I execute an update request to update the books of id '4' information to the corresponding library manager endpoint
	Then the response status code should be 400
	And an error message should be returned

Examples: 
	| Field  | NumberOfCharacters |
	| Author | 31                 |
	| Title  | 101                |

