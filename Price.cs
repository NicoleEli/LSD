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
        public int Shillings  { get; set; }
        /// <summary>
        /// Number of pennies
        /// </summary>
        public double Pennies  { get; set; }

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
            throw new NotImplementedException();
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