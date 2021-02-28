using System;
using System.Collections.Generic;

#nullable disable

namespace ToB.WebApi.DB
{
    public partial class Registry
    {
        public int Id { get; set; }
        public int? Parent { get; set; }
        public string Label { get; set; }
    }
}
