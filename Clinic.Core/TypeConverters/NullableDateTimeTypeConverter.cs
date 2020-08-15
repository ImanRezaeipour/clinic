using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DNTPersianUtils.Core;

namespace Advertise.Core.TypeConverters
{
    public class NullableDateTimeTypeConverter : ITypeConverter<DateTime?, DateTime?>
    {
        public DateTime? Convert(DateTime? source, DateTime? destination, ResolutionContext context)
        {
            return source ?? new DateTime(1989, 01, 15);
        }
    }
}
