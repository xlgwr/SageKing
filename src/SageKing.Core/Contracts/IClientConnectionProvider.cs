using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

public interface IClientConnectionProvider  
{
    public IClientConnection GetClientConnection(string name);

}
