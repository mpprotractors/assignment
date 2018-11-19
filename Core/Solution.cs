using System.Collections.Generic;
using Mp.Protractors.Test.DTOs;

namespace Mp.Protractors.Test.Core
{
    public class Solution
    {
        public bool Success { get; set; }
        public IList<FactDTO> Result { get; set; }
        public IList<ErrorDTO> Errors { get; set; }

        public Solution () 
        {
            Success = true;
            Result = new List<FactDTO>();
            Errors = new List<ErrorDTO>();
        }
    }
}