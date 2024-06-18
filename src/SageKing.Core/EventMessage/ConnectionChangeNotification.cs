using SageKing.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.EventMessage;

public record ConnectionChangeNotification<T>(ClientConnectionInfo<T> ClientConnection, bool isAdd) : INotification;
