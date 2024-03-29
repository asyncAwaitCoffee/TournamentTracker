CREATE OR ALTER PROCEDURE TOURNAMENT_TRACKER.USP_ADD_MATCHUP
	@TOURNAMENT_ID INT,
	@MATCHUP_ROUND INT,
	@ID INT = 0 OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	-- we dont know the winner yet so it's NULL
	INSERT INTO TOURNAMENT_TRACKER.MATCHUPS(TOURNAMENT_ID, MATCHUP_ROUND)
	VALUES (@TOURNAMENT_ID, @MATCHUP_ROUND)

	SELECT @ID = SCOPE_IDENTITY();
END
GO