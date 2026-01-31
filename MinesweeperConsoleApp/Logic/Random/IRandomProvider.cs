using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Logic.Random
{
    public interface IRandomProvider
    {
        int Next(int minInclusive, int maxExclusive);
    }
}

