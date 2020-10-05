using System;
using System.Reflection;

namespace ZEROWORLD.Files
{
    /// <summary>
    /// 这里的每个方法都与 <see cref="Call(object[])"/>（即 <see cref="ZEROWORLD.Call(object[])"/>）作用相符<para></para>
    /// 即方法名为 args[0]，其余参数为 args[1]、args[2]……
    /// </summary>
    public class ZCall : FilesBase
    {
        internal static Type ZCallType;
        internal static MethodInfo[] ZCallMethods;

        public static object Call(params object[] args)
        {
            (bool, MethodInfo) result = ParametersCheck(args);
            if (!result.Item1)
                return null;
            object[] parameters = new object[args.Length - 1];
            for (int i = 1; i < args.Length; i++)
                parameters[i - 1] = args[i];
            return result.Item2.Invoke(null, parameters);
        }

        public override void Load()
        {
            ZCallType = typeof(ZCall);
            ZCallMethods = ZCallType.GetMethods();
        }

        public override void Unload()
        {
            ZCallType = null;
            ZCallMethods = null;
        }

        private static (bool, MethodInfo) ParametersCheck(object[] args)
        {
            if (!(args[0] is string))
                return (false, null);
            string methodName = args[0] as string;
            if (methodName.Is("Load", "Unload"))
                return (false, null);
            MethodInfo info = ZCallType.GetMethod(methodName);
            if (info == null)
                return (false, info);
            ParameterInfo[] parameters = info.GetParameters();
            for (int i = 1; i < args.Length; i++)
            {
                if (!parameters[i - 1].Equals(args[i]))
                    return (false, info);
            }
            return (true, info);
        }
    }
}