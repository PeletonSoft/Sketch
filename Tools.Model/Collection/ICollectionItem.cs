namespace PeletonSoft.Tools.Model.Collection
{
    public interface ICollectionItem
    {
        void AfterInsert();
        void BeforeDelete();

    }
}
