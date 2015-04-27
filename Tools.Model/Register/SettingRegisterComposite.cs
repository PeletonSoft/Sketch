using System.Collections.Generic;
using System.Windows.Markup;

namespace PeletonSoft.Tools.Model.Register
{
    [ContentProperty("List")]
    public class SettingRegisterComposite : ISettingRegister
    {
        public IList<ISettingRegister> List { get; set; }

        public void Register(IDictionary<string, object> settings)
        {
            if (List != null)
            {
                foreach (var register in List)
                {
                    register.Register(settings);
                }
            }
        }
    }
}
