namespace Lab05_CompanyManager
{
    public class WorksOn
    {
        // attribute
        public float Hours { get; set; }
        //owner attributes
        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}