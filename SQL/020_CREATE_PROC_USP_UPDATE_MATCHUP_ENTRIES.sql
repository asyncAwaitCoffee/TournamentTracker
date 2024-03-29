CREATE OR ALTER PROCEDURE TOURNAMENT_TRACKER.USP_UPDATE_MATCHUP_ENTRIES
	@ID INT,
	@TEAM_COMPETING_ID INT = NULL,
	@SCORE float = NULL
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE TOURNAMENT_TRACKER.MATCHUP_ENTRIES
		SET TEAM_COMPETING_ID = @TEAM_COMPETING_ID,
			SCORE = @SCORE
	WHERE ID = @ID;
END