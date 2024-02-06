using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupEntryModel
    {
        /// <summary>
        /// The unique identifier for the matchup
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The ID for the team
        /// </summary>
        public int TeamCompetingId { get; set; }
        /// <summary>
        /// Represents one team in the matchup
        /// </summary>
        public TeamModel TeamCompeting { get; set; }
        /// <summary>
        /// Represents the score of this particular matchup
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// The ID for the parent matchup
        /// </summary>
        public int ParentMatchupId { get; set; }
        /// <summary>
        /// Represents the matchup that this team came from as the winner
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
    }
}
