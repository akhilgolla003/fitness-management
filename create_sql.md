# Creating Database and Tables

CREATE DATABASE FitnessCenterDB;


CREATE TABLE Members
(
    MemberID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    MembershipType NVARCHAR(20),
    ExpirationDate DATE
);

CREATE TABLE Trainers
(
    TrainerID INT IDENTITY(1,1) PRIMARY KEY,
    TrainerName NVARCHAR(50),
    Specialization NVARCHAR(50),
    Certification NVARCHAR(50),
    ExperienceYears INT
);



CREATE TABLE Classes
(
    ClassID INT IDENTITY(1,1) PRIMARY KEY,
    ClassName NVARCHAR(50),
    Schedule DATETIME,
    TrainerID INT FOREIGN KEY REFERENCES Trainers(TrainerID),
    MaxCapacity INT,
    RoomNumber INT
);


CREATE TABLE Attendance
(
    AttendanceID INT IDENTITY(1,1) PRIMARY KEY,
    MemberID INT FOREIGN KEY REFERENCES Members(MemberID),
    ClassID INT FOREIGN KEY REFERENCES Classes(ClassID),
    Date DATETIME,
    Status NVARCHAR(20)
);



# Triggers


----------This trigger enforces a maximum capacity constraint for a class in the `Attendance` table, 
preventing the addition of more attendees if the class is already at full capacity.----------------------

CREATE TRIGGER EnforceMaxCapacity
ON Attendance
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @current_capacity INT;
    DECLARE @class_id INT;

    SELECT @class_id = ClassID FROM INSERTED;

    SELECT @current_capacity = COUNT(*)
    FROM Attendance
    WHERE ClassID = @class_id;

    IF @current_capacity >= (SELECT MaxCapacity FROM Classes WHERE ClassID = @class_id) 
    BEGIN
        THROW 50000, 'Class at full capacity. Cannot add more attendees.', 1;
    END
END;



--------------------This trigger prevents schedule conflicts in the `Classes` table by checking 
if the new schedule overlaps with existing class schedules before allowing an update.-----------------------------------

CREATE TRIGGER PreventScheduleConflict
ON Classes
INSTEAD OF UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @new_schedule VARCHAR(50);
    DECLARE @class_id INT;

    SELECT @new_schedule = Schedule, @class_id = ClassID
    FROM INSERTED;

    IF EXISTS (
        SELECT 1
        FROM Classes
        WHERE ClassID <> @class_id
          AND Schedule = @new_schedule
    )
    BEGIN
        THROW 50000, 'Schedule conflicts with other classes. Choose a different schedule.', 1;
    END
    ELSE
    BEGIN
        UPDATE Classes
        SET 
            ClassName = INSERTED.ClassName,
            Schedule = INSERTED.Schedule,
            TrainerID = INSERTED.TrainerID,
            MaxCapacity = INSERTED.MaxCapacity,
            RoomNumber = INSERTED.RoomNumber
        FROM INSERTED
        WHERE Classes.ClassID = @class_id;
    END
END;
