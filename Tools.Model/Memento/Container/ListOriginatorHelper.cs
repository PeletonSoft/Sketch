using System.Linq;


namespace PeletonSoft.Tools.Model.Memento.Container
{
    public static class ListOriginatorHelper
    {
        public static void PlainSave<T>(this IListOriginator<T> originator, IListDataTransfer<T> state)
            where T : class, IDataTransfer
        {
            originator.Items
                .Select(item => item.Value)
                .ToList()
                .ForEach(item => state.List.Add(item.GetDataTransfer()));
        }

        public static void ZipRestore<T>(this IListOriginator<T> originator, IListDataTransfer<T> state)
            where T : class, IDataTransfer
        {
            originator.Items
                .Zip(state.List,
                    (item, tc) => new
                    {
                        Originator = item.Value,
                        DataTransfer = tc.Content
                    })
                .ToList()
                .ForEach(rec => rec.Originator.Restore(rec.DataTransfer));
        }
    }
}
