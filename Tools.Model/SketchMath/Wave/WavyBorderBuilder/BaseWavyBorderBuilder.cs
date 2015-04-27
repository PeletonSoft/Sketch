namespace PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder
{
    public abstract class BaseWavyBorderBuilder : IWavyBorderBuilder
    {
        protected BaseWavyBorderBuilder(WavyBorderParameters parameters,
            IExtraStrategy extraStrategy)
        {
            _parameters = parameters;
            _extraStrategy = extraStrategy;
            WavyBorderOffset = new WavyBorderOffset(0, 0);
        }

        private readonly IExtraStrategy _extraStrategy;
        private readonly WavyBorderParameters _parameters;

        public double Width
        {
            get { return _parameters.Width; }
        }

        public int WaveCount
        {
            get { return _parameters.WaveCount; }
        }

        public double WaveHeight
        {
            get { return _parameters.WaveHeight; }
        }

        public double ExtraStart
        {
            get { return _extraStrategy.GetExtraStart(_parameters, WavyBorderOffset); }
        }

        public double ExtraFinish
        {
            get { return _extraStrategy.GetExtraFinish(_parameters, WavyBorderOffset); }
        }

        private WavyBorder<Position> _wavyBorder;

        public WavyBorder<Position> WavyBorder
        {
            get
            {
                if (_wavyBorder == null)
                {
                    var waveShare = Calculate();
                    _wavyBorder = new WavyBorder<Position>()
                    {
                        Waves = waveShare.Waves,
                        Bottoms = waveShare.Bottoms
                    };
                }
                return _wavyBorder;
            }
        }

        public double FullWidth
        {
            get { return Width + 2*WaveHeight*WaveCount; }
        }

        public double Step
        {
            get
            {
                return (Width - ExtraStart - ExtraFinish - WavyBorderOffset.A)/
                       (WaveCount + WavyBorderOffset.K);
            }
        }

        public WavyBorderOffset WavyBorderOffset { get; protected set; }

        protected abstract WavyBorder<Position> Calculate();

    }
}