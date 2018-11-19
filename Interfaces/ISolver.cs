using System;
using System.Collections.Generic;
using Mp.Protractors.Test.Core;
using Mp.Protractors.Test.DTOs;

public interface ISolver
{
    Solution Solve(IList<FactDTO> facts);
}