using Entities.classes;
using System;
using System.Collections.Generic;
using System.IO;

namespace lunaplena_caja
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // PATH TO ALL REQUIRED FILES, they should be in release folder. 
            string SENDERS_PATH_FILE = "remitentes.txt";
            string BRANDS_PATH_FIlE = "marcas.txt";
            string SALES_PATH_FILE = "ventas.txt";
            string SORTED_SALES_PATH_FILE = "ventas_ordenadas.txt";
            string WITHOUT_REQUIRED_FILE_ERROR_MESSAGE = "Error, falta el siguente archivo requerido : ";


            try
            {
                if (!File.Exists(BRANDS_PATH_FIlE))
                {
                    ConsoleMessageLogger.LogErrorToConsole(WITHOUT_REQUIRED_FILE_ERROR_MESSAGE + BRANDS_PATH_FIlE);
                    return;
                }

                if (!File.Exists(SALES_PATH_FILE))
                {
                    ConsoleMessageLogger.LogErrorToConsole(WITHOUT_REQUIRED_FILE_ERROR_MESSAGE + SALES_PATH_FILE);
                    return;
                }

                if (!File.Exists(SORTED_SALES_PATH_FILE))
                {
                    ConsoleMessageLogger.LogErrorToConsole(WITHOUT_REQUIRED_FILE_ERROR_MESSAGE + SORTED_SALES_PATH_FILE);
                    return;
                }

                if (!File.Exists(SENDERS_PATH_FILE))
                {
                    ConsoleMessageLogger.LogErrorToConsole(WITHOUT_REQUIRED_FILE_ERROR_MESSAGE + SENDERS_PATH_FILE);
                    return;

                }


                // EXTRACT EVERY LINE OF DATA FROM .TXT FILES 
                string[] salesData = File.ReadAllLines(SALES_PATH_FILE);
                string[] brandsData = File.ReadAllLines(BRANDS_PATH_FIlE);
                string[] sendersData = File.ReadAllLines(SENDERS_PATH_FILE);

                // MAKE LISTS FROM THE READED LINES
                List<Remitente> remitentes = Remitente.GetAllFromStringArray(sendersData);
                List<Sale> sales = Sale.GetAllFromStringArray(salesData);
                Sale.senders = remitentes; // SET SENDERS INTO OUR SALE CLASS
                List<Marca> marcas = Marca.GetAllFromStringArray(brandsData);


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
