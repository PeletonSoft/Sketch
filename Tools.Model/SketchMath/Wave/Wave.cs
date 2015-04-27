namespace PeletonSoft.Tools.Model.SketchMath.Wave
{
    public class Wave<T> : IWave<T>
    {
        public T Peak { get; private set; }
        public T InSeat { get; private set; }
        public T OutSeat { get; private set; }

        public Wave(T peak, T inSeat, T outSeat)
        {
            Peak = peak;
            InSeat = inSeat;
            OutSeat = outSeat;
        }

        public IBottom<T> InSide 
        {
            get { return new Bottom<T>(InSeat, Peak);} 
        }
        public IBottom<T> OutSide
        {
            get { return new Bottom<T>(Peak, OutSeat); }
        }

    }
}