using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata
{
    public interface ICheckout
    {
        void Scan(string item);
        float Total();

    }
}
