using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;

namespace csharp_json_load_write_exercise
{
    class Program
    {
        public static List<Employee> employees = new List<Employee>();
        public static string jsonFileName = "pracownicy.json";

        static void Main(string[] args)
        {
            CheckData("pracownicy.json");

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" JSON - Save and read test");
                Console.WriteLine("1. Load employees from JSON");
                Console.WriteLine("2. Show employess");
                Console.WriteLine("3. Add a person");
                Console.WriteLine("4. Save employess to JSON");
                Console.WriteLine("0. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Odczytujemy");
                        LoadFromJson(jsonFileName);
                        Console.WriteLine("Any key return to menu");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Dane pracowników:");
                        ShowEmployess();
                        Console.WriteLine("Any key return to menu");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Dodajemy");
                        AddEmployee();
                        Console.WriteLine("Any key return to menu");
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Zapisujemy osobe");
                        SaveToJson(jsonFileName);
                        Console.WriteLine("Any key return to menu");
                        Console.ReadKey();
                        break;
                }

                if (choice == "0")
                {
                    break;
                }
            }
        }

        static void CheckData(string filename)
        {
            if (File.Exists(filename))
            {
                if (new FileInfo(filename).Length != 0)
                {
                    Console.WriteLine("Ładuję bazę pracowników");
                    JsonRad(filename);
                }
            }
            else
            {
                Console.WriteLine(" Nie wprowadziłeś danych pracowników\nzacznij od dodania pracownika, następnie zapisz dane do Json");
            }

            Console.ReadKey();
        }

        static void ShowEmployess()
        {
            int i = 1;
            foreach (var employee in employees)
            {
                Console.WriteLine("---Rekord nr {0}-----------------------------\n", i);
                Console.WriteLine("  Email: {0}, \n\n  Imię: {1}, Nazwisko: {2} \n", employee.email, employee.FirstName, employee.LastName);
                Console.WriteLine("  Pseudomin {0} W malzenstwie: {1} \n", employee.Nickname, employee.Spouse);
                Console.WriteLine("  Tel. {0}, Tytul naukowy: {1}, Stanowisko: {2} \n", employee.Phone, employee.title, employee.position);
                Console.WriteLine("  Miasto: {0}, Budynek: {1}, Pokój {2} \n", employee.city, employee.building, employee.room);
                Console.WriteLine("  Wynagrodzenie: {0} zł \n", employee.salary);
                Console.WriteLine("  Użytkownik został dodany w dniu: {0} \n", employee.DateAdded);
                Console.WriteLine("---Koniec rekordu nr {0}-----------------------------\n", i);
                i++;
            }
        }

        static void AddEmployee()
        {
            Employee employee = new Employee();
            Validation validation = new Validation();

            Console.WriteLine(employee.FirstName);

            Console.WriteLine("Dodamy pracownika");
            Console.WriteLine("Wpisz dane i zatwierdź klawiszem ENTER");

            Console.WriteLine("Podaj email");
            while (true)
            {
                employee.email = Console.ReadLine();
                if (validation.IsValidEmail(employee.email))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("To nie jest adres email, sprobuj jeszcze raz");
                }
            }

            Console.WriteLine("Podaj imię");
            while (true)
            {
                employee.FirstName = Console.ReadLine();
                if (String.IsNullOrEmpty(employee.FirstName))
                {
                    Console.WriteLine("Pole imie nie moze byc puste");
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Podaj nazwisko");
            while (true)
            {
                employee.LastName = Console.ReadLine();
                if (String.IsNullOrEmpty(employee.LastName))
                {
                    Console.WriteLine("Pole nazwisko nie moze byc puste");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Pseudonim");
            employee.Nickname = Console.ReadLine();

            Console.WriteLine("Data urodzin w formacie YYYY/MM/DD");

            while (true)
            {
                employee.DateOfBirth = Console.ReadLine();
                if (!validation.IsDateTime(employee.DateOfBirth))
                {
                    Console.WriteLine("Data musi być w formacie YYYY/MM/DD");
                }
                else
                {
                    break;
                }

            }

            Console.WriteLine("Czy jest w malzenstwie?");

            while (true)
            {
                var temp = Console.ReadLine();
                if (temp == "tak")
                {
                    employee.Spouse = true;
                    break;
                }
                else if (temp == "nie")
                {
                    employee.Spouse = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Odpowiedz możesz wpisać tylko tak lub nie");
                }
            }

            Console.WriteLine("Numer Telefonu");
            employee.Phone = Console.ReadLine();
            Console.WriteLine("Tytuł naukowy");
            employee.title = Console.ReadLine();
            Console.WriteLine("Stanowisko");
            employee.position = Console.ReadLine();
            Console.WriteLine("Miasto w którym znajduje się firma");
            employee.city = Console.ReadLine();
            Console.WriteLine("Budynek");
            employee.building = Console.ReadLine();
            Console.WriteLine("Pokój");
            employee.room = Console.ReadLine();

            Console.WriteLine("Wynagrodzenie");
            while (true)
            {
                var temp = Console.ReadLine();
                decimal d;
                if (decimal.TryParse(temp, out d))
                {
                    employee.salary = Convert.ToDecimal(temp);
                    break;
                }
                else
                {
                    Console.WriteLine("Wynagrodzenie musi zostac wpisane w postaci liczby");
                }
                //employee.salary;
            }
            employees.Add(employee);
        }

        private static void JsonWrt(string filename)
        {
            using (StreamWriter swriteJson = new StreamWriter(filename))
            {
                var dataemp = JsonConvert.SerializeObject(employees, Newtonsoft.Json.Formatting.Indented);
                swriteJson.WriteLine(dataemp);
            }
        }

        static void SaveToJson(string filename)
        {
            if (!employees.Any())
            {
                Console.WriteLine(" \n\n Niestety nie mam czego zapisać, \n nie dodałeś żadnego pracownika \n\n");
            }
            else
            {
                if (File.Exists(filename))
                {
                    if (new FileInfo(filename).Length != 0)
                    {
                        Console.WriteLine("Plik z bazą pracowników już\n istnieje i nie jest pusty \n czy na pewno chcesz go nadpisać?");
                        JsonWrt(filename);
                    }
                    else
                    {
                        Console.WriteLine("Plik z bazą pracowników już istnieje, jednak jest pusty \n czy chcesz go nadpisać?");
                    }
                }
                else
                {
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("\nZaczynam zapis pracowników do pliku Json");
                    Console.WriteLine("Tworzymy nowy plik z bazą danych pracowników");
                    Console.WriteLine("Sciezka do pliku: {0}\\{1}", Directory.GetCurrentDirectory(), filename);

                    JsonWrt(filename);
                }

            }
        }

        public static void JsonRad(string filename)
        {
            using (StreamReader sreadJson = new StreamReader(filename))
            {
                var dataemp = sreadJson.ReadToEnd();
                var ob = JsonConvert.DeserializeObject<List<Employee>>(dataemp);

                employees.Clear();
                
                foreach (var osoba in ob)
                {
                    Console.WriteLine(osoba.FirstName.ToString());
                    Employee employee = new Employee();
                    employee.email = osoba.email;
                    employee.FirstName = osoba.FirstName;
                    employee.LastName = osoba.LastName;
                    employee.DateOfBirth = osoba.DateOfBirth;
                    employee.Nickname = osoba.Nickname;
                    var temp = osoba.Spouse.ToString();
                    employee.Spouse = Convert.ToBoolean(temp.ToLower());
                    employee.Phone = osoba.Phone;
                    employee.title = osoba.title;
                    employee.position = osoba.position;
                    employee.city = osoba.city;
                    employee.building = osoba.building;
                    employee.room = osoba.room;
                    employee.salary = Convert.ToDecimal(osoba.salary);
                    employee.DateAdded = osoba.DateAdded;
                    employees.Add(employee);
                }
                
                Console.WriteLine($"Wczytałem dane {ob.Count} pracowników");
            }
        }

        static void LoadFromJson(string filename)
        {
            JsonRad(filename);
        }

    }
}
