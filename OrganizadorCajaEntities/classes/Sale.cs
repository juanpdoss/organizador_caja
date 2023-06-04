using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Entities.classes
{
    public class Sale
    {
        public bool hasMatch;
        public string name;
        public static List<Remitente> senders = new List<Remitente>();

        public Sale(string name)
        {
            hasMatch = false;
            this.name = name;
        }

        public static List<Sale> GetAllFromStringArray(string[] data)
        {
            var list = new List<Sale>();

            foreach (var saleString in data)
            {
                list.Add(new Sale(saleString));
            }

            return list;
        }

        public string getFormattedName()
        {
            try
            {
                Regex firstRegex = new Regex(@"\[(.*?)]");
                Match datetimeFromSale = firstRegex.Match(name);

                if (!datetimeFromSale.Success)
                {
                    return name;
                }

                // quita la primer parte de un mensaje de whatsapp, que tiene un formato de -> [ hora - fecha]
                var nameWithoutDatetime = name.Replace(datetimeFromSale.Value, "");


                // quita la parte que sigue, que tiene un  formato de -> [fora y fecha] SENDER:
                // antes de retornarla, tambien quita espacios en blanco al princio y al final de la cadena. 

                const string DEFAULT_SENDER = "Luna Plena";
                string COMPLETE_DEFAULT_PATTERN = $" {DEFAULT_SENDER}:";

                if(senders.Count == 0)
                {
                    return nameWithoutDatetime.Replace(COMPLETE_DEFAULT_PATTERN, "").Trim();
                }


                foreach (var sender in Sale.senders)
                {
                    string COMPLETE_PATTERN = $" {sender.name}:";

                    if (nameWithoutDatetime.Contains(COMPLETE_PATTERN))
                    {
                        string nameWithoutDateTimeAndWithoutSender = nameWithoutDatetime.Replace(COMPLETE_PATTERN, "").Trim();
                        return nameWithoutDateTimeAndWithoutSender;
                    }
                }

                return nameWithoutDatetime;

            }
            catch (Exception e)
            {
                Logger.Log(e);
                return null;
            }

        }

        public static void saveWithoutMatchsReport(List<Sale> sales, string path)
        {
            foreach (var sale in sales)
            {
                if (!sale.hasMatch)
                {
                    using (StreamWriter file = new StreamWriter(path, append: true))
                    {
                        file.WriteLine("Venta sin agrupamiento posible: " + sale.getFormattedName());

                    }
                }
            }
        }

    }

}
