CREATE TABLE Meetings (
    Id          INT          NOT NULL IDENTITY ,
    MeetingName VARCHAR (50) NOT NULL);

CREATE TABLE Participants (
    Id      INT          NOT NULL IDENTITY,
    Meeting VARCHAR (50) NOT NULL,
    Name    VARCHAR (50) NOT NULL,
    email   VARCHAR (50) NOT NULL,
);