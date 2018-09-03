using Db4objects.Db4o;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBLab01
{
    public class Manager
    {
        public static string DbFileName { get; set; }
        public static IObjectContainer Database => Db4oEmbedded.OpenFile(DbFileName);
        public static IObjectContainer OpenDB()
        {
            return Db4oEmbedded.OpenFile(DbFileName);
        }

        public static void StoreObject<T>(T obj)
        {
            Database.Store(obj);
        }

        public static List<T> GetList<T>()
        {
            List<T> results = new List<T>();

            IObjectSet result = Database.QueryByExample(typeof(T));
            foreach(var obj in result)
            {
                results.Add((T)obj);
            }

            return results;
        }
    }
}
