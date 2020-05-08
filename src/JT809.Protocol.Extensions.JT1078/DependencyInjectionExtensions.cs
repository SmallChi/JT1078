using JT809.Protocol.Extensions.JT1078.Enums;
using JT809.Protocol.Extensions.JT1078.MessageBody;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT809.Protocol.Extensions.JT1078
{
    public static class DependencyInjectionExtensions
    {
        public static IJT809Builder AddJT1078Configure(this IJT809Builder iJT809Builder)
        {
            iJT809Builder.Config.Register(Assembly.GetExecutingAssembly());
            iJT809Builder.Config.BusinessTypeFactory.Register(Assembly.GetExecutingAssembly());
            iJT809Builder.Config.SubBusinessTypeFactory.Register(Assembly.GetExecutingAssembly());
            return iJT809Builder;
        }
    }
}
