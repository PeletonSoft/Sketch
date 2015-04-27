namespace PeletonSoft.Tools.Model.SketchMath.Wave
{
    public interface IWave<out T>
    {
        T Peak { get; }
        T InSeat { get; }
        T OutSeat { get; }
        IBottom<T> InSide { get; }
        IBottom<T> OutSide { get; }
    }
}