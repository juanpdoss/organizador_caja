using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace lunaplena_caja
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // PATH TO ALL REQUIRED FILES
            string BRANDS_PATH_FIlE = "marcas.txt";
            string SALES_PATH_FILE = "ventas.txt";
            string SORTED_SALES_PATH_FILE = "ventas_ordenadas.txt";
            string WITHOUT_REQUIRED_FILE_ERROR_MESSAGE = "Error!, faltan archivos requeridos.";

            try
            {
                if (!File.Exists(BRANDS_PATH_FIlE))
                {
                    Console.WriteLine(WITHOUT_REQUIRED_FILE_ERROR_MESSAGE);
                    return;
                }

                if (!File.Exists(SALES_PATH_FILE))
                {
                    Console.WriteLine(WITHOUT_REQUIRED_FILE_ERROR_MESSAGE);
                    return;
                }

                if (!File.Exists(SORTED_SALES_PATH_FILE))
                {
                    Console.WriteLine(WITHOUT_REQUIRED_FILE_ERROR_MESSAGE);
                    return;
                }


                // EXTRACT EVERY LINE OF DATA FROM .TXT FILES 
                string[] salesData = File.ReadAllLines(SALES_PATH_FILE);
                string[] brandsData = File.ReadAllLines(BRANDS_PATH_FIlE);

                // MAKE LISTS FROM THE READED LINES
                List<Sale> sales = Sale.GetSalesFromStringArray(salesData);
                List<Marca> marcas = Marca.GetMarcasFromStringArray(brandsData);

                // me paro en cada marca 
                foreach (var marca in marcas)
                {
                    // si una venta tiene el nombre de la marca en ella, se agrega a la lista "ventas" de la marca
                    foreach (var sale in sales)
                    {
                        if (sale.name.Contains(marca.name))
                        {
                            sale.hasMatch = true;
                            marca.addSale(sale);
                        }
                    }
                }

                foreach (var marca in marcas)
                {

                    marca.saveSalesReport(SORTED_SALES_PATH_FILE);
                }

                Sale.saveWithoutMatchsReport(sales, SORTED_SALES_PATH_FILE);
                Marca.SaveTopSalesReport(SORTED_SALES_PATH_FILE, marcas);
                Console.WriteLine("Ordenamiento de ventas guardado.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }

}
