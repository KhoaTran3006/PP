using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintingBoard
{
    public interface IPainting
    {
        BoardConfig.PaintColor Color { get; }
    }
}
