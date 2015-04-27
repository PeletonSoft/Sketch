namespace PeletonSoft.Tools.Model.SketchMath.Wave
{
    public interface IBottom<out T>
    {
        T Start { get; }
        T Finish { get; }
    }
}