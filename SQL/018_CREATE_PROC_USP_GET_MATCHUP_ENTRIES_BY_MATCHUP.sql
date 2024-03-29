CREATE OR ALTER PROCEDURE [TOURNAMENT_TRACKER].[USP_GET_MATCHUP_ENTRIES_BY_MATCHUP]
	@MATCHUP_ID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		ME.ID AS Id,
		ME.PARENT_MATCHUP_ID AS ParentMatchupId,
		ME.TEAM_COMPETING_ID AS TeamCompetingId,
		ME.SCORE AS Score
	FROM TOURNAMENT_TRACKER.MATCHUP_ENTRIES AS ME
	WHERE MATCHUP_ID = @MATCHUP_ID;
END