using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBD.Utilities
{
    static class ExtensionMethods
    {
        /// <summary>
        /// Pobiera atrybuty do wyświetlenia w customListView'ie.
        /// </summary>
        public static PropertyInfo[] GetPropertiesForListView(this Type type)
        {
            return type.GetProperties()
                .Where( attr => Attribute.IsDefined(attr, typeof(SkipInListViewAttribute)) == false )
                .ToArray();
        }
    }
}
