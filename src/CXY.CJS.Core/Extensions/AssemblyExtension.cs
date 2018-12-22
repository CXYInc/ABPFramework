using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CXY.CJS.Core.Extension
{
    public static class AssemblyExtension
    {
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        public static IEnumerable<Assembly> LoadReferencedAssemblies(this Assembly assembly)
        {
            return assembly.GetReferencedAssemblies().Select(Assembly.Load);
        }
    }
}