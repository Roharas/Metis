#language: nl-NL
Functionaliteit: GetUsersApiTest

Achtergrond: AllUsers api get
    Als wij de Users api vragen om een lijst met alle gebruikers

Scenario: ApiTest Fail gebruiker bestaat niet
    Dan zien wij dat er een actieve gebruiker genaamd Admin bestaat

Scenario: ApiTest succes gebruiker bestaat
    Dan zien wij dat er een actieve gebruiker genaamd TestAdmin bestaat