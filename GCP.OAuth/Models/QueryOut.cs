using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace GCP.OAuth.Models
{
    public partial class QueryOut
    {
        public List<Sale> grids
        {
            get; set;
        }
    }
}
