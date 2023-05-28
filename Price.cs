using System;
using System.Text;

public namespace Lsd
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
        /// Number of pennies
        /// </summary>
        public double Pennies { get; set; }

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
                        + Pennies;
            }
        }

        /// <summary>
        /// Create a new <see cref="Price"/> in pounds, shillings and pence
        /// </summary>
        /// <param name="pounds">Number of whole pounds</param>
        /// <param name="shillings">Number of whole shillings</param>
        /// <param name="pennies">Number of pennies</param>
        public Price(int pounds, int shillings, double pennies)
        {
            Pounds = pounds;
            Shillings = shillings;
            Pennies = pennies;
        }

        public Price FromCoins(int? guineas = null, int? pounds = null, int? sovreigns = null, int? halfSovreigns = null,
                                int? crowns = null, int? halfCrowns = null, int? florins = null, int? shillings = null, 
                                int? sixpences = null, int? threepences = null, int? pennies = null, 
                                int? halfpennies = null, int? farthings = null)
        {
            Pounds = pounds ?? 0;
            Shillings = shillings ?? 0;
            Pennies = pennies ?? 0;

            Pounds += guineas ?? 0;
            Shillings += guineas ?? 0;

            Pounds += sovreigns ?? 0;

            Shillings += (halfSovreigns ?? 0) * 10;

            Shillings += (crowns ?? 0) * 5;

            Shillings += (halfCrowns ?? 0) * 2;
            Pennies += (halfCrowns ?? 0) * 6;

            Shillings += (florins ?? 0) * 2;

            Pennies += (sixpences ?? 0) * 6;
            Pennies += (threepences ?? 0) * 3;

            double fractionalCoinsTotal = (halfpennies * 0.5) + (farthings * 0.25);
            int wholePennies = fractionalCoinsTotal / 1;
            Pennies += wholePennies;

            if (Pennies > 12)
            {
                Shillings += Pennies / 12;
                Pennies = Pennies % 12;
            }
            if (Shillings > 20)
            {
                Pounds += Shillings / 20;
                Shillings = Shillings % 20;
            }

            // add on remaining fractional pennies at the end so they don't get lost in remainder maths
            Pennies += fractionalCoinsTotal - wholePennies;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Pounds > 0)
            {
                sb.Append($"£{Pounds}");
                if (Shillings > 0 || Pennies > 0)
                {
                    var shillings = Shillings > 0 ? Shillings.ToString() : "–";
                    var pennies = Pennies > 0 ? Pennies.ToString() : "–";
                    sb.Append($"/{shillings}/{pennies}");
                }
            }
            else if (Shillings > 0)
            {
                var shillings = Shillings > 0 ? Shillings.ToString() : "–";
                var pennies = Pennies > 0 ? Pennies.ToString() : "–";
                sb.Append($"/{shillings}/{pennies}");
            }
            else
            {
                sb.Append($"{Pennies}d.");
            }
            return sb.ToString();
        }
    }
}