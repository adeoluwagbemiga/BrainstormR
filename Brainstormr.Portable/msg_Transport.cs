using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable
{
    public class msg_Transport<T>
    {
        public T Msg { get; private set; }
        public msg_Transport(T msg)
        {
            Msg = msg;
        }
    }
}
