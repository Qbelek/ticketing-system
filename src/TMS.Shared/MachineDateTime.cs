using System;

namespace TMS.Shared
{
    public class MachineDateTime
    {
        public virtual DateTimeOffset Now()
        {
            return DateTimeOffset.Now.ToUniversalTime();
        }
    }
}