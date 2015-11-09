namespace PeletonSoft.Tools.Model.Memento
{
    public static class OriginatorHelper
    {
        public static T Save<T>(this IOriginator<T> originator) where T : IDataTransfer
        {
            var state = originator.CreateState();
            originator.Save(state);
            return state;
        }
    }
}