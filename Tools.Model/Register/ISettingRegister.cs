using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PeletonSoft.Tools.Model.Register
{
    public interface ISettingRegister
    {
        void Register(IDictionary<string,object> settings);
    }

    public class SettingRegisterList : Collection<ISettingRegister>
    {
    }
}
