namespace PeletonSoft.Tools.Model.File
{
    public interface IFileBox
    {
        byte[] Data { get; }
        string Extention { get; }
        void WriteToFile(string fileName);
    }
}