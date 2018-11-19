using System.Collections.Generic;

namespace Mp.Protractors.Test.DTOs
{
    public class ErrorDTO
    {
        public string Item { get; set; }
        public List<string> Errors { get; set; }

        public ErrorDTO (string item, List<string> errors) 
        {
            Item = item;
            Errors = errors;
        }
    }
}