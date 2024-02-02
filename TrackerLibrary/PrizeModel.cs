using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class PrizeModel
    {
        /// <summary>
        /// The unique identifier for the prize
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The numeric identifier for the place
        /// </summary>
        public int PlaceNumber { get; set; }
        /// <summary>
        /// The friendly string name for the place
        /// </summary>
        public string PlaceName { get; set; }
        /// <summary>
        /// The fixed amount this place earns or 0 if it is not used
        /// </summary>
        public decimal PrizeAmout { get; set; }
        /// <summary>
        /// The percentage of the overall take or 0 if it is not used
        /// Fractions of 1, so 0.5 is 50%
        /// </summary>
        public double PrizePercentage { get; set; }

        public PrizeModel()
        {
            
        }

        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage)
        {
            PlaceName = placeName;

            int.TryParse(placeNumber, out int placeNumberValue);
            PlaceNumber = placeNumberValue;

            decimal.TryParse(prizeAmount, out decimal prizeAmountValue);
            PrizeAmout = prizeAmountValue;

            double.TryParse(prizePercentage, out double prizePercentageValue);
            PrizePercentage = prizePercentageValue;
        }
    }
}
