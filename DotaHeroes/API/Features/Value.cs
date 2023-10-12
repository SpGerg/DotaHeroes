using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHeroes.API.Features
{
    public class Value
    {
        public decimal CoolValue { get; set; }

        public bool IsPercent { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Features.Value" /> class.
        /// </summary>
        /// <param name="owner"><inheritdoc cref="Owner" /></param>
        public Value(decimal coolValue, bool isPercent)
        {
            CoolValue = coolValue;
            IsPercent = isPercent;
        }
    }
}
