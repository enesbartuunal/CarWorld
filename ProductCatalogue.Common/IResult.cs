using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalogue.Common
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}
