using System.Collections.Generic;

namespace Lab05_CompanyManager
{
    public class Project
    {
        // attributes
        public int PNumber { get; set; }
        public string PName { get; set; }
        public string Location { get; set; }
        // relationships
        public Department ControlledBy { get; set; }
        public List<WorksOn> WorksOn { get; set; }
    }
}