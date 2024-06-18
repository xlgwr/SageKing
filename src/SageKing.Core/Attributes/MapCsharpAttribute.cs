using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class MapCsharpAttribute : Attribute
    {       
        /// <summary>
        /// 映射类型名
        /// </summary>
        public string MapType { get; set; }
        public MapCsharpAttribute(string mapType)
        {
            this.MapType = mapType;
        }
    }
}
