using Castle.Core.Logging;
using log4net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Text;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Felixo.Library.Logging.Aspects
{
    [PSerializable]
    public class InfoAspect:OnMethodBoundaryAspect
    {
        private static ILog logger;
        public InfoAspect()
        {


        }
        public override void RuntimeInitialize(MethodBase method)
        {
            logger = LogManager.GetLogger(method.DeclaringType);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var arguments = args.Arguments;
            var Name= args.Method.Name;
            var str = JsonConvert.SerializeObject(new { Name,Args = arguments});
            logger.Info(str);


        }
    }
}
