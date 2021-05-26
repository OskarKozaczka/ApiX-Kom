# ApiX-Kom

# PL

Aby korzystać z API należy:

1. Utworzyć lokalną baze danych korzystając z pliku DBQuery
2. W pliku konfiguracyjnym wkleić connection string do swojej bazy danych
3. Dodać parametr `MultipleActiveResultSets=true` do connection string

GET /api/Meetings/All - Zwraca Wszystkie spotkania wraz z zapisanymi uczestnikami

GET /api/Meetings/{NazwaSpotkania} - Zwraca Pojedyńcze Spotkanie o podanej nazwie wraz z zapisanymi uczestnikami

POST /api/Meetings/create/{NazwaSpotkania} - Tworzy nowe spotkanie o podanej nazwie | Jeśli spotkanie już istnieje w bazie danych zwrócony zostanie komunikat "Meeting already exists"

DELETE /api/Meetings/{NazwaSpotkania} lub POST /api/Meetings/delete/{NazwaSpotkania} - Usuwa spotkanie o podanej nazwie (i wszystkich uczestników zapisanych na to spotkanie)

POST /api/Meetings/register?meeting={NazwaSpotkania}&name={ImięUczestnika}&email={EmailUczestnika} - Rejestruje uczestnika o podanym Imieniu i adresie e-mail na spotkanie o podanej nazwie | Jeśli spotkanie o danej nazwie nie znajduje się w bazie danych, zwrócony zostanie komunikat"Meeting does not exist". Dodatkowo obowiązuje limit 25 uczestników, jeśli spróbujemy zarejestrować uczestnika na spotkanie które osiągneło już limit 25 uczestników, otrzymamy komunikat "Limit of participants has been reached".

# ANG

To use API you need to:

1. Create a local database using the DBQuery file
2. Paste the connection string to your database in the configuration file
3. Add `MultipleActiveResultSets=true` parameter to connection string

GET /api/Meetings/All - Returns all meetings along with the registered participants

GET /api/Meetings/{MeetingName} - Returns a single meeting with the given name along with the registered participants

POST /api/Meetings/create/{MeetingName} - Creates a new meeting with the given name | If the meeting already exists in the database, the error message "Meeting already exists" will be returned

DELETE /api/Meetings/{MeetingName} or POST /api/Meetings/delete/{MeetingName} - Deletes a meeting with the given name (and all participants registered for this meeting)

POST /api/Meetings/register?meeting={MeetingName}&name={ParticipantName}&email={ParticipantEmail} - Registers the participant with the given Name and e-mail address to the meeting with the given name | If the meeting with the given name is not in the database, the error message "Meeting does not exist" will be returned. Additionally, there is a limit of 25 participants, if we try to register a participant for a meeting that has already reached the limit of 25 participants the error message "Limit of participants has been reached" will be returned.
