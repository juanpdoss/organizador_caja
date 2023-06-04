using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Entities.classes
{
    public class Marca : IComparer<Marca>
    {
        public string name;
        public List<Sale> sales = new List<Sale>();

        public Marca(string nombre)
        {
            name = nombre;
        }

        public static List<Marca> GetAllFromStringArray(string[] lines)
        {
            List<Marca> parsedData = new List<Marca>();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) { continue; }
                parsedData.Add(new Marca(line));
            }

            return parsedData;
        }

        public void addSale(Sale data)
        {
            sales.Add(data);
        }

        public int Compare(Marca x, Marca y)
        {
            if (x.sales.Count == y.sales.Count)
            {
                return 0;

            }
            else if (x.sales.Count > y.sales.Count)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public void saveSalesReport(string path)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(path, append: true))
                {
                    file.WriteLine("Ventas de marca: " + name);

                    if (sales.Count == 0)
                    {
                        file.WriteLine("No se registraron ventas.");
                    }
                    else
                    {
                        // ITERATE OVER ALL SALES, WRITE THEM TO THE .TXT 
                        foreach (var item in sales)
                        {
                            file.WriteLine(item.getFormattedName());
                        }

                    }

                    file.WriteLine("");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: ", e.Message);
                Logger.Log(e);
            }

        }


        public static void SaveTopSalesReport(string path, List<Marca> marcas)
        {
            try
            {
                var sorteredMarcasBySales = marcas.OrderByDescending(marca => marca.sales.Count);
                using (StreamWriter file = new StreamWriter(path, append: true))
                {
                    file.WriteLine("");
                    file.WriteLine("Top de ventas:");
                    file.WriteLine("");

                    for (int i = 1, len = sorteredMarcasBySales.Count(); i <= len; i++)
                    {
                        file.WriteLine($"{i.ToString()}. {sorteredMarcasBySales.ElementAt(i).name}, con {sorteredMarcasBySales.ElementAt(i).sales.Count} ventas.");
                    }
                    file.WriteLine("");
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
        }
    }
}
