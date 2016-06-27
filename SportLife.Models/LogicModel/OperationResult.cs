using System;
using System.Collections.Generic;

namespace SportLife.Models.LogicModel {
    public class OperationResult {
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public List<Exception> Exceptions { get; set; }

        public OperationResult () {
            Exceptions = new List<Exception>();
            Success = false;
        }
    }
}
