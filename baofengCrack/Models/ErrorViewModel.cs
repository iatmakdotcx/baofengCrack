using System;

namespace baofengCrack.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Code { get; set; }
        public string Message { get; set; }
    }
}