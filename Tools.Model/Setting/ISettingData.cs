namespace PeletonSoft.Tools.Model.Setting
{
    public interface ISettingData
    {
        string ConnectionString { get;  }
        string SavePath { get; }
        int OrderId { get; }
        bool ReadOnly { get;  }
    }
}