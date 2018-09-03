
using Db4objects.Db4o;
using System.Collections.Generic;
using System.IO;

namespace Lab05_CompanyManager
{
    public class Database
    {
        public static string DbFileName { get; set; }
        public static IObjectContainer DB => Db4oEmbedded.OpenFile(DbFileName);
        public static void CloseDB(IObjectContainer db)
        {
            db.Close();
        }

        public static void CreateEmployees(IObjectContainer db, string fileName)
        {
            if (File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                StreamReader fin = new StreamReader(fs);
                int nEmps = int.Parse(fin.ReadLine());
                for (int i = 0; i < nEmps; i++)
                {
                    string line = fin.ReadLine();
                    if (line != null)
                    {
                        string[] fields = line.Split(':');
                        string fname = fields[0];
                        char minit = fields[1][0];
                        string lname = fields[2];
                        int ssn = int.Parse(fields[3]);
                        string bdate = fields[4];
                        string address = fields[5];
                        char sex = fields[6][0];
                        float salary = float.Parse(fields[7]);
                        Employee e = new Employee
                        {
                            Ssn = ssn,
                            FName = fname,
                            MInit = minit,
                            LName = lname,
                            Address = address,
                            BirthDate = bdate,
                            Salary = salary,
                            Sex = sex
                        };
                        db.Store(e);
                    }
                }
                fin.Close();
                fs.Close();
            }
        }

        public static void CreateDependents(IObjectContainer db, string fileName)
        {
            // Viết code ở đây ...
        }

        public static void CreateDepartment(IObjectContainer db, string fileName)
        {
            // Viết code ở đây ...
        }

        public static void CreateProject(IObjectContainer db, string fileName)
        {
            // Viết code ở đây ...
        }

        public static void CreateWorkOn(IObjectContainer db, string fileName)
        {
            // Viết code ở đây ...
        }

        public static void SetManagers(IObjectContainer db, string fileName)
        {
            if (File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                StreamReader fin = new StreamReader(fs);
                int nMgrs = int.Parse(fin.ReadLine());
                for (int i = 0; i < nMgrs; i++)
                {
                    string line = fin.ReadLine();
                    string[] fields = line.Split(',');
                    int dno = int.Parse(fields[0]);
                    int essn = int.Parse(fields[1]);
                    string startDate = fields[2];
                    IList<Department> depts = db.Query(delegate (Department dept)
                    {
                        return (dept.DNumber == dno);
                    });
                    Department d = null;
                    if (depts != null)
                        d = depts[0];
                    IList<Employee> emps = db.Query(delegate (Employee emp)
                    {
                        return (emp.Ssn == essn);
                    });
                    Employee e = null;
                    if (emps != null && emps.Count != 0)
                        e = emps[0];
                    if (e != null && d != null)
                    {
                        d.MgrStartDate = startDate;
                        e.Manages = d;
                        d.Manager = e;
                        db.Store(d);
                        db.Store(e);
                    }
                }
            }
        }

        public void ComplexRetrievalExample(IObjectContainer db)
        {
            try
            {
                IList<Department> depts = db.Query(delegate (Department dept)
                {
                    int nEmps = dept.Employees.Count;
                    List<Project> prjs = dept.Projects;
                    bool foundPhoenix = false;
                    for (int i = 0; i < prjs.Count; i++)
                    {
                        Project p = prjs[i];
                        if (p.Location.Equals("Phoenix"))
                        {
                            foundPhoenix = true;
                            break;
                        }
                    }
                    return dept.Locations.Contains("Houston") || (nEmps < 4) || foundPhoenix;
                });
                //for (int i = 0; i < depts.Count; i++)
                //    Console.WriteLine("Department: " + depts[i].DName);
            }
            catch { }
        }
    }
}
