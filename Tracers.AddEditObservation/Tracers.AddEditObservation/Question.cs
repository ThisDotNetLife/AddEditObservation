using System;
using System.Linq;

namespace DynamicTableCreation {
    public class Question {
        public int No               { get; set; }
        public string Text          { get; set; }
        public string EP            { get; set; }
        public bool IsRequired      { get; set; }
        public bool IsNotApplicable { get; set; }
        public string Numerator     { get; set; }
        public string Denominator   { get; set; }
        public string Compliant     { get; set; }
        public string NonCompliant  { get; set; }
    }
}