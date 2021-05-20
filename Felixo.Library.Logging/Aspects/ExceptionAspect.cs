using log4net;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Felixo.Library.Logging.Aspects
{
    [PSerializable]
   public class ExceptionAspect: OnExceptionAspect
    {
        private static ILog logger;
        public ExceptionAspect()
        {

        }
        public override void RuntimeInitialize(MethodBase method)
        {
            logger = LogManager.GetLogger(method.DeclaringType);
        }
        public override void OnException(MethodExecutionArgs args)
        {
            var exception= args.Exception;
            var Name = args.Method.Name;
            var str = JsonConvert.SerializeObject(new { Exception = exception, Name });
            logger.Error(str);
        }
    }
}
