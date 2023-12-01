# DataBase Design :

  This is a sample database designed to track the fitness of each members. The database has 4 tables, where each table is in 3NF. Each table has at least 4 fields (including primary keys).

The Database has 4 tables:

  1. Members
  2. Trainers
  3. Classes
  4. Attendance

## Members Table:

- Attributes: MemberID (Primary Key), FirstName, LastName, MembershipType
- No partial dependencies, all attributes are fully dependent on the primary key.
- Meets 3NF requirements.


## Trainers Table:

- Attributes: TrainerID (Primary Key), TrainerName, Specialization, Certification, ExperienceYears
- No partial dependencies, all attributes are fully dependent on the primary key.
- Meets 3NF requirements.



## Classes Table:

- Attributes: ClassID (Primary Key), ClassName, Schedule, TrainerID (Foreign Key), MaxCapacity, RoomNumber
- No partial dependencies, all attributes are fully dependent on the primary key.
- Meets 3NF requirements.



## Attendance Table:

- Attributes: AttendanceID (Primary Key), MemberID (Foreign Key), ClassID (Foreign Key), Date, Status
- No partial dependencies, all attributes are fully dependent on the primary key.
- Meets 3NF requirements.


# Functional Dependencies:

## Members Table:
  MemberID -> FirstName, LastName, MembershipType, ExpirationDate
## Trainers Table:
  TrainerID -> TrainerName, Specialization, Certification, ExperienceYears
## Classes Table:
  ClassID -> ClassName, Schedule, TrainerID, MaxCapacity, RoomNumber
## Attendance Table:
  AttendanceID -> MemberID, ClassID, Date, Status


# Cross-Table Dependencies in Database Tables
### Members Table:
  No cross-table dependencies.
### Trainers Table:
  No cross-table dependencies.
### Classes Table:
  TrainerID (in Classes Table) -> TrainerID (in Trainers Table)
### Attendance Table:
  MemberID (in Attendance Table) -> MemberID (in Members Table)
  ClassID (in Attendance Table) -> ClassID (in Classes Table)



# Sample Data
## Members Table:
  1. 1, FirstName: John, LastName: Doe, MembershipType: Premium, ExpirationDate: 2023-12-31
  2. 2, FirstName: Jane, LastName: Smith, MembershipType: Standard, ExpirationDate: 2023-11-30
## Trainers Table:
  1. 1, TrainerName: Yoga Master, Specialization: Yoga, Certification: Yoga Certification, ExperienceYears: 5
  2. 2, TrainerName: Fitness Pro, Specialization: Fitness, Certification: Fitness Certification, ExperienceYears: 8
## Classes Table:
  1. 1, ClassName: Yoga Class, Schedule: 2023-11-17 10:00:00, TrainerID: 1, MaxCapacity: 20, RoomNumber: 101
  2. 2, ClassName: Zumba Class, Schedule: 2023-11-18 15:30:00, TrainerID: 2, MaxCapacity: 15, RoomNumber: 102
## Attendance Table:
  1. 1, MemberID: 1, ClassID: 1, Date: 2023-11-17 10:00:00, Status: Present
  2. 2, MemberID: 2, ClassID: 2, Date: 2023-11-18 15:30:00, Status: Absent


