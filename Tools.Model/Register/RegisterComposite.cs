using System.Collections.Generic;
using System.Windows.Markup;

namespace PeletonSoft.Tools.Model.Register
{
    [ContentProperty("List")]
    public class RegisterComposite : IRegister
    {
        public IList<IRegister> List { get; set; }
        public void Register()
        {
            if (List != null)
            {
                foreach (var register in List)
                {
                    register.Register();
                }
            }
        }
    }
}
