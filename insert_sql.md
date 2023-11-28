
## Insert data into Members table
INSERT INTO Members (FirstName, LastName, MembershipType, ExpirationDate)
VALUES
    ('John', 'Doe', 'Active', '2023-12-31'),
    ('Alice', 'Smith', 'Active', '2024-01-15'),
    ('Bob', 'Johnson', 'Expired', '2023-11-30'),
    ('Eva', 'Williams', 'Active', '2024-02-28'),
    ('Michael', 'Davis', 'Expired', '2023-10-20'),
    ('Sophia', 'Brown', 'Active', '2024-03-15'),
    ('Daniel', 'Miller', 'Active', '2023-12-31'),
    ('Olivia', 'Taylor', 'Expired', '2023-09-10'),
    ('Matthew', 'Anderson', 'Active', '2024-04-30'),
    ('Emma', 'Moore', 'Active', '2024-05-31');


## Insert data into Trainers table
INSERT INTO Trainers (TrainerName, Specialization, Certification, ExperienceYears)
VALUES
    ('Sarah Johnson', 'Yoga', 'Yoga Certification', 5),
    ('Alex Rodriguez', 'Weightlifting', 'Fitness Trainer Certification', 7),
    ('Emily White', 'Cardio', 'Cardio Instructor Certification', 4),
    ('Michael Brown', 'CrossFit', 'CrossFit Trainer Certification', 6),
    ('Jessica Miller', 'Pilates', 'Pilates Instructor Certification', 3);



## Insert data into Classes table
INSERT INTO Classes (ClassName, Schedule, TrainerID, MaxCapacity, RoomNumber)
VALUES
    ('Yoga Class', '2023-11-20 10:00:00', 1, 15, 101),
    ('Weightlifting Session', '2023-11-21 14:30:00', 2, 10, 102),
    ('Cardio Workout', '2023-11-22 18:00:00', 3, 20, 103),
    ('CrossFit Training', '2023-11-23 11:15:00', 4, 12, 104),
    ('Pilates Session', '2023-11-24 16:45:00', 5, 18, 105);
	


## Insert data into Attendance table
INSERT INTO Attendance (MemberID, ClassID, Date, Status)
VALUES
    (1, 1, '2023-11-20 10:00:00', 'Present'),  -- John attended Yoga Class
    (2, 2, '2023-11-21 14:30:00', 'Present'),  -- Alice attended Weightlifting Session
    (3, 3, '2023-11-22 18:00:00', 'Present'),  -- Bob attended Cardio Workout
    (4, 4, '2023-11-23 11:15:00', 'Present'),  -- Eva attended CrossFit Training
    (5, 5, '2023-11-24 16:45:00', 'Present'),  -- Michael attended Pilates Session
    (6, 1, '2023-11-20 10:00:00', 'Present'),  -- Sophia attended Yoga Class
    (7, 2, '2023-11-21 14:30:00', 'Present'),  -- Daniel attended Weightlifting Session
    (8, 3, '2023-11-22 18:00:00', 'Present'),  -- Olivia attended Cardio Workout
    (9, 4, '2023-11-23 11:15:00', 'Present'),  -- Matthew attended CrossFit Training
    (10, 5, '2023-11-24 16:45:00', 'Present');  -- Emma attended Pilates Session

