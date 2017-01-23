using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mega_Escritorio
{
    class Program
    {
        static void Main(string[] args)
        {
            int width;
            int length;
            int surfaceArea;
            int numberOfDrawers;
            string surfaceMaterial;
            int rushOrderOption;
            const int baseDeskPrice = 200;
            int surfaceAreaPrice;
            int drawerPrice;
            int materialPrice;
            int rushOrderPrice;
            int totalPrice;
            string jsonOrder = "";

            Console.WriteLine("Welcome to MegaEscritorio - Let's build a desk!");

            width = GetWidth();
            length = GetLength();
            surfaceArea = width * length;
            numberOfDrawers = GetNumberOfDrawers();
            surfaceMaterial = GetSurfaceMaterial();
            rushOrderOption = GetRushOrderOption();

            // ** CALCULATIONS **

            surfaceAreaPrice = CalculateSurfaceAreaPrice(surfaceArea);
            drawerPrice = numberOfDrawers * 50;
            materialPrice = CalculateMaterialPrice(surfaceMaterial);
            rushOrderPrice = CalculateRushOrderPrice(rushOrderOption, surfaceArea);
            totalPrice = baseDeskPrice + surfaceAreaPrice + drawerPrice + materialPrice + rushOrderPrice;

            // Form JSON
            jsonOrder += "{\"deskOrders\":[";
            jsonOrder += "{";
            jsonOrder += "\"width\":\"" + width.ToString() + "\",";
            jsonOrder += "\"length\":\"" + length.ToString() + "\",";
            jsonOrder += "\"surfaceArea\":\"" + surfaceArea.ToString() + "\",";
            jsonOrder += "\"numberOfDrawers\":\"" + numberOfDrawers.ToString() + "\",";
            jsonOrder += "\"surfaceMaterial\":\"" + surfaceMaterial + "\",";
            jsonOrder += "\"rushOrderOption\":\"" + rushOrderOption.ToString() + "\",";
            jsonOrder += "\"baseDeskPrice\":\"" + baseDeskPrice.ToString() + "\",";
            jsonOrder += "\"surfaceAreaPrice\":\"" + surfaceAreaPrice.ToString() + "\",";
            jsonOrder += "\"drawerPrice\":\"" + drawerPrice.ToString() + "\",";
            jsonOrder += "\"materialPrice\":\"" + materialPrice.ToString() + "\",";
            jsonOrder += "\"rushOrderPrice\":\"" + rushOrderPrice.ToString() + "\",";
            jsonOrder += "\"totalPrice\":\"" + totalPrice.ToString() + "\"";
            jsonOrder += "}";
            jsonOrder += "]}";

            //Write JSON to file
            try
            {
                StreamWriter writer = new StreamWriter("orders.txt");
                writer.WriteLine(jsonOrder);
                writer.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //Display order to screen
            Console.WriteLine("\n\nYour order:");
            Console.WriteLine("width: " + width.ToString() + " in.");
            Console.WriteLine("length: " + length.ToString() + " in.");
            Console.WriteLine("surface area: " + surfaceArea.ToString() + " in. squared");
            Console.WriteLine("number of drawers: " + numberOfDrawers.ToString());
            Console.WriteLine("surface material: " + surfaceMaterial);
            Console.WriteLine("rush order option: " + rushOrderOption.ToString() + " days");
            Console.WriteLine("\n--COST BREAKDOWN--");
            Console.WriteLine("base desk price: " + baseDeskPrice.ToString());
            Console.WriteLine("surface area price: " + surfaceAreaPrice.ToString());
            Console.WriteLine("drawer price: " + drawerPrice.ToString());
            Console.WriteLine("material price: " + materialPrice.ToString());
            Console.WriteLine("rush order price: " + rushOrderPrice.ToString());
            Console.WriteLine("---------------");
            Console.WriteLine("TOTAL PRICE: " + totalPrice.ToString());

            Console.WriteLine("\n\nPress 'Enter' to exit...");
            Console.ReadLine();
        }

        static int GetIntValueInRange(int min_value, int max_value, string prompt, string try_again)
        {
            string intString;
            int intValue = 0;
            string displayPrompt = prompt;

            try
            {
                do
                {
                    Console.Write(displayPrompt);
                    intString = Console.ReadLine();
                    intValue = int.Parse(intString);
                    if (intValue < min_value || intValue > max_value)
                    {
                        displayPrompt = try_again;
                    }
                } while (intValue < min_value || intValue > max_value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return intValue;
            }

            return intValue;
        }

        static int GetIntValueInArray(int[] array_values, int default_value, string prompt, string try_again)
        {
            string intString;
            int intValue = 0;
            string displayPrompt = prompt;
            bool entry_okay = false;

            try
            {
                do
                {
                    Console.Write(displayPrompt);
                    intString = Console.ReadLine();
                    if (intString == "") { intString = default_value.ToString(); }
                    intValue = int.Parse(intString);
                    if (!array_values.Contains(intValue) && intValue != default_value)
                    {
                        displayPrompt = try_again;
                    } else
                    {
                        entry_okay = true;
                    }
                } while (!entry_okay);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return intValue;
            }

            return intValue;
        }

        static int GetWidth()
        {
            const int MIN_WIDTH = 1;
            const int MAX_WIDTH = 1200;

            string prompt = "Please enter the width of the desk (" + MIN_WIDTH + " - " + MAX_WIDTH + " inches) : ";
            string try_again = " The width must be in the range of " + MIN_WIDTH + " - " + MAX_WIDTH + ". Please try again : ";

            return GetIntValueInRange(MIN_WIDTH, MAX_WIDTH, prompt, try_again);
        }

        static int GetLength()
        {
            const int MIN_LENGTH = 1;
            const int MAX_LENGTH = 1200;

            string prompt = "Please enter the length of the desk (" + MIN_LENGTH + " - " + MAX_LENGTH + " inches) : ";
            string try_again = " The length must be in the range of " + MIN_LENGTH + " - " + MAX_LENGTH + ". Please try again : ";

            return GetIntValueInRange(MIN_LENGTH, MAX_LENGTH, prompt, try_again);
        }

        static int GetNumberOfDrawers()
        {
            const int MIN_DRAWERS = 0;
            const int MAX_DRAWERS = 7;

            string prompt = "Please enter the number of drawers (" + MIN_DRAWERS + " - " + MAX_DRAWERS + ") : ";
            string try_again = " The number of drawers must be in the range of 0 - 7. Please try again : ";

            return GetIntValueInRange(MIN_DRAWERS, MAX_DRAWERS, prompt, try_again);
        }

        static string GetSurfaceMaterial()
        {
            const int MIN_MATERIAL_VALUE = 1;
            const int MAX_MATERIAL_VALUE = 3;

            int surface_material_value;

            string prompt = "Please enter the surface material (1 = laminate, 2 = oak, 3 = pine) : ";
            string try_again = " That choice is not available. Please select 1 for laminate, 2 for oak, or 3 for pine) : ";

            surface_material_value = GetIntValueInRange(MIN_MATERIAL_VALUE, MAX_MATERIAL_VALUE, prompt, try_again);

            switch (surface_material_value)
            {
                case 1:
                    return "laminate";
                case 2:
                    return "oak";
                case 3:
                    return "pine";
                default:
                    return "unknown";

            }

        }

        static int GetRushOrderOption()
        {
            int[] rush_order_values = new int[3] { 3, 5, 7 };
            const int DEFAULT_DAYS = 14;

            string prompt = "Please enter the rush order days (3, 5, or 7 or leave blank for no rush) : ";
            string try_again = " That choice is not available. Please select 3, 5, or 7 (or leave blank for no rush) : ";

            return GetIntValueInArray(rush_order_values, DEFAULT_DAYS, prompt, try_again);

        }

        static int CalculateSurfaceAreaPrice(int surfaceArea) {
            if(surfaceArea > 1000)
            {
                return surfaceArea * 5;
            }
            else
            {
                return 0;
            }
        }

        static int CalculateMaterialPrice(string material)
        {
            switch (material)
            {
                case "oak":
                    return 200;
                case "laminate":
                    return 100;
                case "pine":
                    return 50;
                default:
                    return 0;
            }
        }

        static int CalculateRushOrderPrice(int rushOrderOption, int surfaceArea)
        {
            int[] rushOrderPrices = new int[9];

            try
            {
                StreamReader reader = new StreamReader("rush_order_prices.txt");

                for (int i = 0; i < 9; i++)
                {
                    rushOrderPrices[i] = int.Parse(reader.ReadLine());
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            switch (rushOrderOption) {
                case 3:
                    if (surfaceArea <= 1000) { return rushOrderPrices[0]; }
                    if (surfaceArea < 2000) { return rushOrderPrices[1]; }
                    return rushOrderPrices[2];
                case 5:
                    if (surfaceArea <= 1000) { return rushOrderPrices[3]; }
                    if (surfaceArea < 2000) { return rushOrderPrices[4]; }
                    return rushOrderPrices[5];
                case 7:
                    if (surfaceArea <= 1000) { return rushOrderPrices[6]; }
                    if (surfaceArea < 2000) { return rushOrderPrices[7]; }
                    return rushOrderPrices[8];
                default:
                    return 0;
            }
        }

    }
}
