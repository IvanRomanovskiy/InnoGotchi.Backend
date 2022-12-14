using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.Application.Common.Exeptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, string key)
           : base($"Entity \"{name}\" ({key}) not found.") { }
    }
}
