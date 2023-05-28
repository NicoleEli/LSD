using System.Text;

namespace Lsd
{
    /// <summary>
    /// A price in £sd currency.
    /// £1 (pound)     = 20/– (shillings)
    /// 1/– (shilling) = 12d. (pence)
    /// 
    /// Other coins
    /// -----------
    /// 1 guinea       = £1/1/–
    /// 1 sovreign     = £1       (not in common circulation at time of decimalisation)
    /// 1/2 sovreign   = 10/–     (not in common circulation at time of decimalisation)
    /// 1 crown        = 5/–      (not in common circulation at time of decimalisation)
    /// 1/2 crown      = 2/6
    /// 1 florin       = 2/–
    /// 1 sixpence     = 6d.
    /// 1 threepence   = 3d.
    /// 1 halfpenny    = 1/2 d.
    /// 1 farthing     = 1/4 d.
    /// </summary>
    public struct Price
    {
        /// <summary>
        /// Number of whole pounds
        /// </summary>
        public int Pounds { get; set; }
        /// <summary>
        /// Number of whole shillings
        /// </summary>
        public int Shillings { get; set; }
        /// <summary>
        /// Number of pence
        /// </summary>
        public double Pence { get; set; }

        /// <summary>
        /// Total number of whole shillings represented by this price
        /// </summary>
        public int TotalShillings
        {
            get 
            {
                return Pounds * 20
                        + Shillings;
            }
        }
        /// <summary>
        /// Total number of pennies represented by this price
        /// </summary>
        public double TotalPennies
        {
            get 
            {
                return Pounds * 20 * 12
                        + Shillings * 12
                        + Pence;
            }
        }

        /// <summary>
        /// Create a new <see cref="Price"/> in pounds, shillings and pence
        /// </summary>
        /// <param name="pounds">Number of whole pounds</param>
        /// <param name="shillings">Number of whole shillings</param>
        /// <param name="pence">Number of pennies</param>
        public Price(int pounds, int shillings, double pence)
        {
            Pounds = pounds;
            Shillings = shillings;
            Pence = pence;
        }

        public static Price FromCoins(int? guineas = null, int? pounds = null, int? sovreigns = null, int? halfSovreigns = null,
                                int? crowns = null, int? halfCrowns = null, int? florins = null, int? shillings = null, 
                                int? sixpences = null, int? threepences = null, int? pennies = null, 
                                int? halfpennies = null, int? farthings = null)
        {
            pounds = pounds ?? 0;
            shillings = shillings ?? 0;
            double pence = pennies ?? 0;

            pounds += guineas ?? 0;
            shillings += guineas ?? 0;

            pounds += sovreigns ?? 0;

            shillings += (halfSovreigns ?? 0) * 10;

            shillings += (crowns ?? 0) * 5;

            shillings += (halfCrowns ?? 0) * 2;
            pence += (halfCrowns ?? 0) * 6;

            shillings += (florins ?? 0) * 2;

            pence += (sixpences ?? 0) * 6;
            pence += (threepences ?? 0) * 3;

            double fractionalCoinsTotal = ((halfpennies ?? 0) * 0.5) + ((farthings ?? 0) * 0.25);
            int wholePennies = (int)fractionalCoinsTotal;
            pence += wholePennies;

            if (pence >= 12)
            {
                shillings += (int)pence / 12;
                pence = (int)pence % 12;
            }
            if (shillings >= 20)
            {
                pounds += shillings / 20;
                shillings = shillings % 20;
            }

            // add on remaining fractional pennies at the end so they don't get lost in remainder maths
            pence += fractionalCoinsTotal - wholePennies;

            return new Price(pounds.Value, shillings.Value, pence);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Pounds > 0)
            {
                sb.Append($"£{Pounds}");
                if (Shillings > 0 || Pence > 0)
                {
                    var shillings = Shillings > 0 ? Shillings.ToString() : "–";
                    var pennies = Pence > 0 ? Pence.ToString() : "–";
                    sb.Append($"/{shillings}/{pennies}");
                }
            }
            else if (Shillings > 0)
            {
                var shillings = Shillings > 0 ? Shillings.ToString() : "–";
                var pennies = Pence > 0 ? Pence.ToString() : "–";
                sb.Append($"/{shillings}/{pennies}");
            }
            else
            {
                sb.Append($"{Pence}d.");
            }
            return sb.ToString();
        }
    }
}