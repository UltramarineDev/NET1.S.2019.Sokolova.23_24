namespace BLL.Interface.Services
{
    public interface IBonuseCalculator
    {
        decimal CalculateBonuses(string accountType, decimal withdrawAmount);
    }
}
