namespace PeletonSoft.Tools.Model.Dragable
{
    public class DataTransition<TS, TD>
    {
        public TS Source { get; private set; }
        public TD Destination { get; private set; }

        public DataTransition(TS source, TD destination)
        {
            Source = source;
            Destination = destination;
        }
    }

    public class DataTransition : DataTransition<object, object>
    {
        public DataTransition(object source, object destination)
            : base(source, destination)
        {
        }

        public DataTransition<TS, TD> Cast<TS, TD>()
        {
            return new DataTransition<TS, TD>((TS) Source, (TD) Destination);
        }
    }


}
