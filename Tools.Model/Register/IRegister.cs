using System.Collections.ObjectModel;

namespace PeletonSoft.Tools.Model.Register
{
    public interface IRegister
    {
        void Register();
    }

    public class RegisterList : Collection<IRegister>
    {
    }
}
