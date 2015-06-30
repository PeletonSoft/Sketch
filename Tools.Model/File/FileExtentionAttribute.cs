using System;

namespace PeletonSoft.Tools.Model.File
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FileExtentionAttribute : Attribute
    {
        public string Extention { get; private set; }
        public FileExtentionAttribute(string extention)
        {
            Extention = extention;
        }
    }
}
