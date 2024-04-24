CREATE DATABASE CarCode;
GO

USE CarCode;
GO

CREATE TABLE Info
(
    Id INT PRIMARY KEY IDENTITY,
    Code NVARCHAR(10) NOT NULL,
    Region NVARCHAR(40) NOT NULL
)
	

INSERT INTO Regions 
VALUES
('CC9101CC', 'Odesa'),
('DD2345DD', 'Kharkiv'),
('EE6789EE', 'Dnipro'),
('BB5678BB', 'Lviv'),
('JJ9900JJ', 'Rivne'),
('GG3344GG', 'Khmelnytskyi'),
('FF1122FF', 'Zaporizhzhia'),
('HH5566HH', 'Chernivtsi'),
('II7788II', 'Lutsk'),
('AA1234AA', 'Kyiv')

