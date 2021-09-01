namespace PriceComparator
{
    public interface IPriceSource
    {
        string Tag { get; }
        
        string Producer { get; }
        
        float GetPrice();

        void LoadPrice();

        int GetColor();
    }
}