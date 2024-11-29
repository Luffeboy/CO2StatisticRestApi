﻿use jeppejeppsson_dk_db_test

DROP TABLE IF EXISTS SensorUser
DROP TABLE IF EXISTS Users
DROP TABLE IF EXISTS Sensors
DROP TABLE IF EXISTS SensorUser
DROP TABLE IF EXISTS Measurements

CREATE TABLE Users (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	Email VARCHAR(32),
	UserPassword VARCHAR(32)
);
CREATE TABLE Sensors (
	Id INT PRIMARY KEY IDENTITY(1,1),
	SensorName VARCHAR(16)
);
CREATE TABLE SensorUser (
	Id INT PRIMARY KEY IDENTITY(1,1),
	UserId INT,
	SensorId INT,
	CONSTRAINT FK_UserId FOREIGN KEY (UserId) REFERENCES Users(Id),
	CONSTRAINT FK_SensorId FOREIGN KEY (SensorId ) REFERENCES Sensors(Id)
);

CREATE TABLE Measurements (
	Id INT PRIMARY KEY IDENTITY(1, 1),
	SensorId INT,
	MeasurementTime DateTime,
	MeasurementValue INT,
	CONSTRAINT FK_MeasurementSensorId FOREIGN KEY (SensorId) REFERENCES Sensors(Id)
);
