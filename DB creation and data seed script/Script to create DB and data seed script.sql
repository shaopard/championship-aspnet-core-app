--IF OBJECT_ID (N'dbo.UDF_GetWinner', N'FN') IS NOT NULL
--BEGIN  
--    DROP FUNCTION UDF_GetWinner;  
--END
--GO
--CREATE FUNCTION UDF_GetWinner(@FirstPlayerID INT, @SecondPlayerID INT, @FirstPlayerScore INT, @SecondPlayerScore INT)
--RETURNS INT
--AS
--BEGIN
--	IF(@SecondPlayerScore > @FirstPlayerScore)
--		RETURN @SecondPlayerID

--	RETURN @FirstPlayerID
--END
--GO

IF (OBJECT_ID('dbo.RoundType', 'U') IS NULL)
BEGIN
	CREATE TABLE dbo.[RoundType](
		RoundTypeID INT NOT NULL IDENTITY(1, 1),
		Name NVARCHAR(30) UNIQUE NOT NULL,
		CONSTRAINT PK_RoundType_RoundTypeID PRIMARY KEY(RoundTypeID)
	)

	INSERT INTO dbo.[RoundType](Name)
	VALUES ('First round'), ('Winners roster'), ('Losers roster'), ('Final round')
END

IF (OBJECT_ID('dbo.Player', 'U') IS NULL)
BEGIN
	CREATE TABLE dbo.[Player](
		PlayerID INT NOT NULL IDENTITY(1, 1),
		Name NVARCHAR(30),
		CONSTRAINT PK_Player_PlayerID PRIMARY KEY(PlayerID)
	)

	INSERT INTO dbo.Player(Name)
	VALUES ('Andrei'), ('Bogdan'), ('Calin'), ('Dorel'), ('Elena'), ('Florin'), ('Gheorghe'), ('Hagi')
END

IF (OBJECT_ID('dbo.Match', 'U') IS NULL)
BEGIN
	CREATE TABLE dbo.[Match](
		MatchID INT NOT NULL IDENTITY(1, 1),
		Number INT NOT NULL,
		RoundTypeID INT NOT NULL CONSTRAINT FK_Round_RoundType REFERENCES dbo.RoundType(RoundTypeID),
		CONSTRAINT PK_Round_RoundID PRIMARY KEY(MatchID)
	)

	DECLARE @firstRoundID INT = (SELECT RoundTypeID FROM dbo.RoundType WHERE LTRIM(RTRIM(LOWER(Name))) = 'first round')
	DECLARE @winnerRosterID INT = (SELECT RoundTypeID FROM dbo.RoundType WHERE LTRIM(RTRIM(LOWER(Name))) = 'winners roster')
	DECLARE @loserRosterID INT = (SELECT RoundTypeID FROM dbo.RoundType WHERE LTRIM(RTRIM(LOWER(Name))) = 'losers roster')
	DECLARE @finalRoundID INT = (SELECT RoundTypeID FROM dbo.RoundType WHERE LTRIM(RTRIM(LOWER(Name))) = 'final round')

	SET IDENTITY_INSERT dbo.[Match] ON
	INSERT 
		INTO dbo.[Match](MatchID, Number, RoundTypeID)
	VALUES 
		(1, 1, @firstRoundID),
		(2, 1, @firstRoundID),
		(3, 1, @firstRoundID),
		(4, 1, @firstRoundID), 
		(5, 1, @winnerRosterID), 
		(6, 2, @winnerRosterID), 
		(7, 3, @winnerRosterID), 
		(8, 1, @loserRosterID), 
		(9, 2, @loserRosterID), 
		(10, 3, @loserRosterID), 
		(11, 1, @finalRoundID)
	SET IDENTITY_INSERT dbo.[Match] OFF
END

IF (OBJECT_ID('dbo.MatchDetail', 'U') IS NULL)
BEGIN
	CREATE TABLE dbo.[MatchDetail](
		MatchDetailID INT NOT NULL IDENTITY(1, 1),
		MatchID INT NOT NULL CONSTRAINT FK_MatchDetail_Match REFERENCES dbo.[Match](MatchID),
		Player1ID INT NOT NULL CONSTRAINT FK_Match_Player1 REFERENCES dbo.[Player](PlayerID),
		Player2ID INT NOT NULL CONSTRAINT FK_Match_Player2 REFERENCES dbo.[Player](PlayerID),
		Player1Score INT NOT NULL,
		Player2Score INT NOT NULL,
		WinningPlayer AS dbo.UDF_GetWinner(Player1ID, Player2ID, Player1Score, Player2Score),

		CONSTRAINT PK_MatchDetail_MatchDetailID PRIMARY KEY(MatchDetailID)
	)

	DECLARE @PlayerAndreiID INT = (SELECT PlayerID FROM dbo.Player WHERE LTRIM(RTRIM(LOWER(Name))) = 'andrei')
	DECLARE @PlayerBogdanID INT = (SELECT PlayerID FROM dbo.Player WHERE LTRIM(RTRIM(LOWER(Name))) = 'bogdan')
	DECLARE @PlayerCalinID INT = (SELECT PlayerID FROM dbo.Player WHERE LTRIM(RTRIM(LOWER(Name))) = 'calin')
	DECLARE @PlayerDorelID INT = (SELECT PlayerID FROM dbo.Player WHERE LTRIM(RTRIM(LOWER(Name))) = 'dorel')
	DECLARE @PlayerElenaID INT = (SELECT PlayerID FROM dbo.Player WHERE LTRIM(RTRIM(LOWER(Name))) = 'elena')
	DECLARE @PlayerFlorinID INT = (SELECT PlayerID FROM dbo.Player WHERE LTRIM(RTRIM(LOWER(Name))) = 'florin')
	DECLARE @PlayerGheorgheID INT = (SELECT PlayerID FROM dbo.Player WHERE LTRIM(RTRIM(LOWER(Name))) = 'gheorghe')
	DECLARE @PlayerHagiID INT = (SELECT PlayerID FROM dbo.Player WHERE LTRIM(RTRIM(LOWER(Name))) = 'hagi')


	INSERT 
		INTO dbo.MatchDetail(MatchID, Player1ID, Player2ID, Player1Score, Player2Score)
	VALUES 
		(1, @PlayerAndreiID, @PlayerBogdanID, 4, 3),
		(2, @PlayerCalinID, @PlayerDorelID, 2, 1),
		(3, @PlayerElenaID, @PlayerFlorinID, 5, 3),
		(4, @PlayerGheorgheID, @PlayerHagiID, 9, 3),

		(5, @PlayerAndreiID, @PlayerCalinID, 8, 3),
		(6, @PlayerElenaID, @PlayerGheorgheID, 7, 3),
		(7, @PlayerAndreiID, @PlayerElenaID, 6, 3),

		(8, @PlayerBogdanID, @PlayerDorelID, 9, 8),
		(9, @PlayerFlorinID, @PlayerHagiID, 7, 2),
		(10, @PlayerBogdanID, @PlayerFlorinID, 8, 3),

		(11, @PlayerAndreiID, @PlayerBogdanID, 4, 3)
END

SELECT * FROM dbo.MatchDetail
SELECT * FROM dbo.Match
SELECT * FROM dbo.RoundType
SELECT * FROM dbo.Player

--DROP TABLE dbo.MatchDetail
--DROP TABLE dbo.Match
--DROP TABLE dbo.RoundType
--DROP TABLE dbo.Player
--DROP FUNCTION UDF_GetWinner

