using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAWebUI.Models
{
    public class ErrorDisplayModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }

        public bool ShowError => !string.IsNullOrEmpty(Title);
    }
}
