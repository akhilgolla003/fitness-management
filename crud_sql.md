# Members Table


## Create (Insert) a new member
INSERT INTO Members (FirstName, LastName, MembershipType, ExpirationDate)
VALUES ('John', 'Doe', 'Premium', '2023-12-31');

## Read (Select) all members
SELECT * FROM Members;

## Update a member's information
UPDATE Members
SET MembershipType = 'Gold'
WHERE MemberID = 1;

## Delete a member
DELETE FROM Members
WHERE MemberID = 1;

# Trainers Table


## Create (Insert) a new trainer
INSERT INTO Trainers (TrainerName, Specialization, Certification, ExperienceYears)
VALUES ('Jane Smith', 'Yoga', 'Yoga Certification', 5);

## Read (Select) all trainers
SELECT * FROM Trainers;

## Update a trainer's information
UPDATE Trainers
SET ExperienceYears = 6
WHERE TrainerID = 1;

## Delete a trainer
DELETE FROM Trainers
WHERE TrainerID = 1;

# Classes Table


## Create (Insert) a new class
INSERT INTO Classes (ClassName, Schedule, TrainerID, MaxCapacity, RoomNumber)
VALUES ('Yoga Class', '2023-11-17 10:00:00', 1, 20, 101);

## Read (Select) all classes
SELECT * FROM Classes;

## Update a class's information
UPDATE Classes
SET MaxCapacity = 25
WHERE ClassID = 1;

## Delete a class
DELETE FROM Classes
WHERE ClassID = 1;

# Attendance Table


## Create (Insert) attendance record
INSERT INTO Attendance (MemberID, ClassID, Date, Status)
VALUES (1, 1, '2023-11-17 10:00:00', 'Present');

## Read (Select) all attendance records
SELECT * FROM Attendance;

## Update an attendance record
UPDATE Attendance
SET Status = 'Absent'
WHERE AttendanceID = 1;

## Delete an attendance record
DELETE FROM Attendance
WHERE AttendanceID = 1;
