using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sherlock.Framework.Data
{
    public enum LikeRegion
    {
        /// <summary>
        /// %{}%,无法利用索引
        /// </summary>
        Both,
        /// <summary>
        /// %{},无法利用索引
        /// </summary>
        Left,
        /// <summary>
        /// {}%
        /// </summary>
        Right
    }
}
