-- Smart Attendance Classroom Monitoring
-- MySQL schema for fresh installs or migrations.
-- Run with: mysql -u root -p < database.sql

CREATE DATABASE IF NOT EXISTS `smart-attendance-monitoring`
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE `smart-attendance-monitoring`;

-- Students master list
CREATE TABLE IF NOT EXISTS students_tbl (
  idstudents_tbl INT NOT NULL AUTO_INCREMENT,
  FirstName VARCHAR(100) NOT NULL,
  MiddleName VARCHAR(100) NULL,
  LastName VARCHAR(100) NOT NULL,
  Gender VARCHAR(20) NULL,
  YearLevel INT NOT NULL DEFAULT 1,
  Section VARCHAR(50) NULL,
  LRN VARCHAR(50) NULL,
  BirthDate DATE NULL,
  Email VARCHAR(150) NULL,
  Address VARCHAR(255) NULL,
  Image TEXT NULL,
  QRCode LONGBLOB NULL,
  PRIMARY KEY (idstudents_tbl),
  KEY idx_students_name (LastName, FirstName),
  KEY idx_students_section (Section)
) ENGINE=InnoDB;

-- Teacher profile list
CREATE TABLE IF NOT EXISTS teachers_tbl (
  idteachers_tbl INT NOT NULL AUTO_INCREMENT,
  FirstName VARCHAR(100) NOT NULL,
  LastName VARCHAR(100) NOT NULL,
  Gender VARCHAR(20) NULL,
  DateOfBirth DATE NULL,
  Email VARCHAR(150) NULL,
  PhoneNumber VARCHAR(50) NULL,
  Address VARCHAR(255) NULL,
  Image TEXT NULL,
  PRIMARY KEY (idteachers_tbl),
  KEY idx_teachers_name (LastName, FirstName)
) ENGINE=InnoDB;

-- Teacher credentials (stored as plain text to match the app)
CREATE TABLE IF NOT EXISTS teacher_account_tbl (
  idteacher_account INT NOT NULL AUTO_INCREMENT,
  name VARCHAR(255) NULL,        -- expected format: "LastName, FirstName"
  username VARCHAR(100) NULL,
  password VARCHAR(100) NULL,
  PRIMARY KEY (idteacher_account),
  UNIQUE KEY uq_teacher_username (username)
) ENGINE=InnoDB;

-- Subject -> teacher mapping (comma-separated teacher IDs per column, one row with id = 1)
CREATE TABLE IF NOT EXISTS subjects_tbl (
  idsubjects_tbl INT NOT NULL,
  english VARCHAR(255) NULL,
  filipino VARCHAR(255) NULL,
  math VARCHAR(255) NULL,
  science VARCHAR(255) NULL,
  araling_panlipunan VARCHAR(255) NULL,
  mapeh VARCHAR(255) NULL,
  ict VARCHAR(255) NULL,
  PRIMARY KEY (idsubjects_tbl)
) ENGINE=InnoDB;

-- Ensure the single configuration row exists
INSERT INTO subjects_tbl (idsubjects_tbl) VALUES (1)
  ON DUPLICATE KEY UPDATE idsubjects_tbl = VALUES(idsubjects_tbl);

-- Optional: relational storage for schedules (UI currently persists to %APPDATA%\\SmartAttendance\\schedules.json)
CREATE TABLE IF NOT EXISTS schedules_tbl (
  idschedules_tbl INT NOT NULL AUTO_INCREMENT,
  room INT NOT NULL,
  day VARCHAR(16) NOT NULL,
  slot INT NOT NULL,
  subjectId INT NULL,
  teacherId INT NULL,
  PRIMARY KEY (idschedules_tbl),
  KEY idx_sched_room_day_slot (room, day, slot),
  CONSTRAINT fk_sched_teacher FOREIGN KEY (teacherId) REFERENCES teachers_tbl(idteachers_tbl) ON DELETE SET NULL
) ENGINE=InnoDB;
