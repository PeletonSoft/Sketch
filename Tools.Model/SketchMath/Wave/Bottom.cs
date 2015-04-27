namespace PeletonSoft.Tools.Model.SketchMath.Wave
{
    public class Bottom<T> : IBottom<T>
    {
        public T Start { get; private set; }
        public T Finish { get; private set; }

        public Bottom(T start, T finish)
        {
            Start = start;
            Finish = finish;
        }
    }
}