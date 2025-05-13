Use  master
If EXISTS (SELECT *  FROM sys.databases WHERE name = 'Part1' )
DROP DATABASE Part1;
CREATE DATABASE Part1;

USE Part1;

DROP TABLE IF EXISTS Booking;

DROP TABLE IF EXISTS Event;

DROP TABLE IF EXISTS Venue;

CREATE TABLE Venue (
    VenueID INT PRIMARY KEY IDENTITY(1,1),
    VenueName NVARCHAR(100) NOT NULL,
    Location NVARCHAR(200) NOT NULL,
    Capacity INT NOT NULL,
    ImageUrl NVARCHAR(500)
);

CREATE TABLE Event (
    EventId INT PRIMARY KEY IDENTITY(1,1),
    EventName NVARCHAR(100) NOT NULL,
    EventDate DATETIME2 NOT NULL,
    Description NVARCHAR(500),
    VenueId INT,
    CONSTRAINT FK_Event_Venue FOREIGN KEY (VenueId) REFERENCES Venue(VenueID)
);

CREATE TABLE Booking (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    EventId INT NOT NULL,
    VenueId INT NOT NULL,
    BookingDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Booking_Event FOREIGN KEY (EventId) REFERENCES Event(EventId),
    CONSTRAINT FK_Booking_Venue FOREIGN KEY (VenueId) REFERENCES Venue(VenueID)
);

-- Insert sample venues
INSERT INTO Venue (VenueName, Location, Capacity, ImageUrl)
VALUES 
('Grand Ballroom', '123 Main Street, New York', 500, 'https://eventeasehk.blob.core.windows.net/venue-images/grand ballroom.jpeg'),
('Riverside Theater', '456 River Road, Chicago', 1200, 'https://eventeasehk.blob.core.windows.net/venue-images/theater.jpeg'),
('Skyline Convention Center', '789 High Avenue, Los Angeles', 3000, 'https://eventeasehk.blob.core.windows.net/venue-images/center.jpeg'),
('Intimate Jazz Club', '101 Jazz Lane, New Orleans', 80, 'https://eventeasehk.blob.core.windows.net/venue-images/jazz.jpeg'),
('Sports Arena', '202 Stadium Drive, Miami', 15000, 'https://eventeasehk.blob.core.windows.net/venue-images/sports.jpeg');

-- Insert sample events
INSERT INTO Event (EventName, EventDate, Description, VenueId)
VALUES
('Annual Tech Conference', '2023-11-15 09:00:00', 'The biggest tech conference of the year', 1),
('Rock Concert Extravaganza', '2023-12-20 19:30:00', 'Featuring top rock bands from around the world', 2),
('Food Festival', '2023-10-05 10:00:00', 'A celebration of international cuisine', 3),
('Jazz Night', '2023-11-10 20:00:00', 'An evening of smooth jazz', 4),
('Basketball Championship', '2023-12-15 18:00:00', 'National finals game', 5);


-- Insert sample bookings
INSERT INTO Booking (EventId, VenueId, BookingDate)
VALUES
(1, 1, '2023-01-15 10:30:00'),
(2, 2, '2025-06-20 14:15:00'),
(3, 3, '2025-05-30 12:00:00'),
(4, 4, '2025-06-22 13:00:00'),
(5, 5, '2025-05-29 16:30:00');




SELECT * FROM Venue;

SELECT * FROM Event;

SELECT * FROM Booking;
