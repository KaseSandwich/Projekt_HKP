using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_HKP.Lib.DataAccess
{
    public class DefaultTypeProvider : ITypeProvider
    {
        public IEnumerable<string> GetAllTypes()
        {
            return new List<string>
            {
                "Switch",
                "Router",
                "AccessPoint",
                "DesktopPc",
                "Notebook",
                "Server",
                "Drucker"
            };
        }
    }
}
