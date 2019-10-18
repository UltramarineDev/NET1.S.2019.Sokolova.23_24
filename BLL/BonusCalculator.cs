using System;
using BLL.Interface.Services;

namespace BLL
{
    public class BonusCalculator: IBonuseCalculator
    {
        public decimal CalculateBonuses(string accountType, decimal withdrawAmount)
        {
            decimal bonuses = default(decimal);

            switch(accountType)
            {
                case "BASE":
                    bonuses = withdrawAmount * 0.015m;
                    break;
                case "GOLD":
                    bonuses = withdrawAmount * 0.03m;
                    break;
                case "PLATINUM":
                    bonuses = withdrawAmount * 0.05m;
                    break;
                default:
                    throw new ArgumentException("Invalid account type", nameof(accountType));
            }

            return bonuses;
        }
    }
}
