public interface IUpdateFeeMap
{
    void UpdateFeeMap(int ordinal, DecreaseIncreaseMode decreaseIncreaseMode = DecreaseIncreaseMode.Increase);
    void UpdateFeeMap(int ordinal, int newFee);
}