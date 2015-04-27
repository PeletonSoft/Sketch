using System.Collections.Generic;
using System.Windows.Markup;

namespace PeletonSoft.Tools.Model.Register
{
    [ContentProperty("InnerRegister")]
    public class RegisterAdapter : ISettingRegister
    {
        public IRegister InnerRegister { get; set; }
        public void Register(IDictionary<string, object> settings)
        {
            InnerRegister.Register();
        }
    }
}
