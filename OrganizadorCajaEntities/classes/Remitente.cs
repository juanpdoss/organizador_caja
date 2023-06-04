using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.classes
{
    public class Remitente
    {
        public string name = "";

        public Remitente(string name)
        {
            this.name = name;
        }

        public static List<Remitente> GetAllFromStringArray(string[] data)
        {
            var list = new List<Remitente>();

            foreach (string remitente in data)
            {
                list.Add(new Remitente(remitente));
            }

            return list;
        }
    }
}
