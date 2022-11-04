using ötös.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ötös.Abstarction
{
    public interface IToyFactory
    {
        Toy CreateNew();
    }
}
