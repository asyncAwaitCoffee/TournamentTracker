/*
CREATE SCHEMA TOURNAMENT_TRACKER
*/
DROP TABLE IF EXISTS TOURNAMENT_TRACKER.TOURNAMENTS
CREATE TABLE TOURNAMENT_TRACKER.TOURNAMENTS
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	TOURNAMENT_NAME VARCHAR(50),
	ENTRY_FEE MONEY
)

DROP TABLE IF EXISTS TOURNAMENT_TRACKER.TOURNAMENT_ENTRIES
CREATE TABLE TOURNAMENT_TRACKER.TOURNAMENT_ENTRIES
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	TOURNAMENT_ID VARCHAR(50),
	TEAM_ID INT
)

DROP TABLE IF EXISTS TOURNAMENT_TRACKER.TOURNAMENT_PRIZES
CREATE TABLE TOURNAMENT_TRACKER.TOURNAMENT_PRIZES
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	TOURNAMENT_ID VARCHAR(50),
	PRIZE_ID INT
)

DROP TABLE IF EXISTS TOURNAMENT_TRACKER.PRIZES

CREATE TABLE TOURNAMENT_TRACKER.PRIZES
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	PLACE_NUMBER INT,
	PLACE_NAME NVARCHAR(50),
	PRIZE_AMOUNT MONEY,
	PRIZE_PERCENTAGE FLOAT
)

DROP TABLE IF EXISTS TOURNAMENT_TRACKER.TEAMS

CREATE TABLE TOURNAMENT_TRACKER.TEAMS
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	TEAM_NAME NVARCHAR(50)
)

DROP TABLE IF EXISTS TOURNAMENT_TRACKER.TEAM_MEMBERS

CREATE TABLE TOURNAMENT_TRACKER.TEAM_MEMBERS
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	TEAM_ID INT,
	PERSON_ID INT
)

DROP TABLE IF EXISTS TOURNAMENT_TRACKER.PERSONS

CREATE TABLE TOURNAMENT_TRACKER.PERSONS
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	FIRST_NAME NVARCHAR(50),
	LAST_NAME NVARCHAR(50),
	EMAIL NVARCHAR(50),
	CELLPHONE NVARCHAR(50)
)

DROP TABLE IF EXISTS TOURNAMENT_TRACKER.MATCHUPS

CREATE TABLE TOURNAMENT_TRACKER.MATCHUPS
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	WINNER_ID INT,
	MATCHUP_ROUND INT
)

DROP TABLE IF EXISTS TOURNAMENT_TRACKER.MATCHUP_ENTRIES

CREATE TABLE TOURNAMENT_TRACKER.MATCHUP_ENTRIES
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	MATCHUP_ID INT,
	PARENT_MATCHUP_ID INT,
	TEAM_COMPETING_ID INT,
	SCORE INT
)