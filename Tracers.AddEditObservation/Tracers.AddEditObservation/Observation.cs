using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicTableCreation {
    public class Observation {
        public IList<Question> Questions { get; set; }
        public int ID          { get; set; }
        public string Name     { get; set; }
        public string Category { get; set; }
        public Observation() {
            Questions = new List<Question>();
            Questions.Add(new Question { No = 1,  IsRequired = true,  EP = "IC.02.02.01 - 1",   Text = "What types of equipment are used in this department and what is the process for cleaning that equipment?" });
            Questions.Add(new Question { No = 2,  IsRequired = false, EP = "IC.02.02.01 - 1", Text = "Who is responsible to clean the equipment you described?" });
            Questions.Add(new Question { No = 3,  IsRequired = true,  EP = "EC.02.06.01 - 1", Text = "Where is patient care equipment stored?" });
            Questions.Add(new Question { No = 4,  IsRequired = false, EP = "IC.02.02.01 - 1", Text = "How would I know if a piece of equipment not currently in use is clean or contaminated?" });
            Questions.Add(new Question { No = 5,  IsRequired = true,  EP = "HR.01.05.03 - 1", Text = "How did you learn about the process for cleaning patient care equipment?" });
            Questions.Add(new Question { No = 6,  IsRequired = false, EP = "IC.02.01.01 - 1", Text = "Does the process for cleaning patient care equipment for patients in isolation differ in any way?" });
            Questions.Add(new Question { No = 7,  IsRequired = true,  EP = "", Text = "Is any Personal Protective Equipment (PPE) is used when cleaning equipment?" });
            Questions.Add(new Question { No = 8,  IsRequired = false, EP = "EC.02.01.01 - 3", Text = "Where are the supplies used to clean patient care equipment stored?" });
            Questions.Add(new Question { No = 9,  IsRequired = true,  EP = "IC.02.02.01 - 2", Text = "Do you perform any high level disinfection in this department?" });
            Questions.Add(new Question { No = 10, IsRequired = false, EP = "NPSG.07.01.01 - 1", Text = "How does this department perform relative to hand hygiene?" });
            Questions.Add(new Question { No = 11, IsRequired = true,  EP = "IC.02.01.01 - 8", Text = "Are you aware of any hospital acquired infections in this department?" });
            Name = "Medication management Storage and Access 6/17/13";
            Category = "Focused";
            SetupDefaultValues();
        }
        private void SetupDefaultValues() {
            if (ID == 0){
                foreach (var ques in Questions) {
                    ques.Numerator    = string.Empty;
                    ques.Denominator  = "1";
                    ques.Compliant    = "N/A";
                    ques.NonCompliant = "N/A";
                }
            }
        }
    }
}