using System;
using System.Collections.Generic;
using System.Text;

namespace JT1078.FMp4.Enums
{
    public enum HandlerType
    {
        /// <summary>
        /// null
        /// </summary>
        none,
        /// <summary>
        /// Video track
        /// </summary>
        vide,
        /// <summary>
        /// Audio track
        /// </summary>
        soun,
        /// <summary>
        /// Hint track
        /// </summary>
        hint,
        /// <summary>
        /// Timed Metadata track 
        /// </summary>
        meta,
        /// <summary>
        /// Auxiliary Video track 
        /// </summary>
        auxv
    }
}
