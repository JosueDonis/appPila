using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AppPerson
{
    public class Program
    {
        public enum State
        {
            SAVE,
            GET,
            DELETE,
            ORDER,
            SEARCH
        }

        static int size = 0;
        static string[] persons;

        public static void Main(string[] args)
        {
            showMain();
        }

        public static void AddQuantityPerson(bool addElement = false)
        {
            Console.Clear();
            Console.Write("Ingrese la cantidad de personas: ");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.Clear();
                Console.WriteLine("¡INGRESE UNA CANTIDAD CORRECTA PRESIONE ENTER!");
                Console.ReadKey();
                AddQuantityPerson();
                return;
            }

            size = int.Parse(input);

            if (size < 1)
            {
                Console.Clear();
                Console.WriteLine("¡INGRESE UNA CANTIDAD CORRECTA PRESIONE ENTER!");
                Console.ReadKey();
                AddQuantityPerson();
                return;
            }

            if(!addElement)
            {
                persons = new string[size];
            }else
            {
                addSpaceArray(size);
            }
        }

        public static void showMain()
        {
            AddQuantityPerson();

            string optionSelected = "";
            do
            {
                Console.Clear();
                Console.Write("1. INGRESAR DATOS");
                Console.Write("\n2. MOSTRAR DATOS");
                Console.Write("\n3. SACAR ULTIMO ELEMENTO");
                Console.Write("\n4. ORDENAR EDADES INGRESADAS");
                Console.Write("\n5. SALIR PRESIONE (SI)");
                Console.Write($"\n6. CANTIDAD ELEMENTOS A INGRESAR '{size}'");
                Console.Write("\nSELECCIONE LA OPCIÓN: ");
                optionSelected = Console.ReadLine();

                if (isNumber(optionSelected))
                {
                    typeOptionSelected((State)Convert.ToInt32(optionSelected) - 1);
                }

            } while (!optionSelected.Equals("SI"));

        }

        public static void typeOptionSelected(State option)
        {
            switch (option)
            {
                case State.SAVE:
                    if (size > 0)
                    {
                        savePerson();

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("¡ESTA LLENO LA PILA DE ELEMENTOS SI DESEA INGRESAR MAS DATOS PRESIONE ENTER!");
                        Console.ReadKey();
                        AddQuantityPerson(true);
                    }
                    break;
                case State.GET:
                    if (hasEmptyArray())
                    {
                        Console.Clear();
                        Console.WriteLine("¡LA LISTA ESTA VACIA!");
                        messageBackMain();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("PERSONAS AGREGADAS: ");
                        getPersons(persons);
                    }
                    break;
                case State.ORDER:
                    Console.Clear();
                    Console.WriteLine("LAS EDADES DE LAS PERSONAS ORDENADOS DE FORMA ASCENDENTE: ");
                    getOrderBy();
                    break;
                case State.SEARCH:

                    break;
                case State.DELETE:
                    size = size + deleteLastElment();
                    checkIsEmptyArray();
                    break;
            }
        }

        public static void savePerson()
        {
            Console.Clear();
            Console.Write("INGRESE LA EDAD DE LA PERSONA: ");
            Person person = new Person();
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine($"LA EDAD INGRESADA ES INCORRECTA PRESIONE ENTER!");
                Console.ReadKey();
                savePerson();
            }else
            {
                person.age = Convert.ToInt32(input);

                if (person.age < 1 || person.age > 120)
                {
                    Console.WriteLine($"LA EDAD INGRESADA ES INCORRECTA PRESIONE ENTER!");
                    Console.ReadKey();
                    savePerson();
                }
                else
                {
                    size = size - addElementArray(Convert.ToString(person.age));
                }
            }
        }

        public static bool isNumber(string data)
        {
            return int.TryParse(data, out int res);
        }

        public static int addElementArray(string age)
        {

            persons[counterArray()] = age;
            return 1;
        }

        public static bool hasFullArray()
        {
            int counter = 0;
            for (int i = persons.Length - 1; i >= 0; i--)
            {
                if (persons[i] != null)
                {
                    counter++;
                }
            }
            return counter == persons.Length ? true : false;
        }

        public static void getPersons(string[] arr)
        {
            for (int i = arr.Length - 1; i >= 0; i--)
            {
               if(arr[i] != null)
                {
                    Console.Write($"{i + 1}: {arr[i]}\n");
                }
            }
            messageBackMain();
        }

        public static int deleteLastElment()
        {
            int delete = 0;
            if(!hasEmptyArray())
            {
                for (int i = persons.Length-1; i >=0; i--)
                {
                    if(persons[i] != null && delete == 0)
                    {
                        persons[i] = null;
                        delete = 1;
                    }
                }
            }

            return delete;
        }

        public static void checkIsEmptyArray()
        {
            Console.Clear();
            if (hasEmptyArray())
            {
                Console.WriteLine("LA PILA ESTA VACIA, PUEDE INGRESAR NUEVOS DATOS");
            }
            else
            {
                Console.WriteLine("EL ÚLTIMO ELEMENTO FUE ELIMINADO");
            }

            messageBackMain();
        }

        public static void messageBackMain()
        {
            Console.Write("¡PRESIONE ENTER PARA REGRESAR!");
            Console.ReadKey();
        }

        public static bool hasEmptyArray()
        {
            int counter = 0;
            for (int i = persons.Length - 1; i >= 0; i--)
            {
                if (persons[i] == null)
                {
                    counter++;
                }
            }
            return counter == persons.Length ? true : false;
        }

        public static void addSpaceArray( int size)
        {
            var tempPersons = persons;
            Array.Resize(ref persons, tempPersons.Length + size);
        }

        public static int counterArray()
        {
            int counter = 0;
            for (int i = persons.Length-1; i>=0; i--)
            {
                if(persons[i] != null)
                {
                    counter++;
                }
            }
            return counter;
        }

        public static string[] orderByAsc(string[] arr)
        {
            string found = "";
            for (int i = 1; i < arr.Length; i++)
            {
                for (int j = arr.Length-1; j >=i; j--)
                {
                    if(int.Parse(arr[j-1]) > int.Parse(arr[j]))
                    {
                        found = arr[j - 1];
                        arr[j-1] = arr[j];
                        arr[j] = found;
                    }
                }
            }

            return arr;
        }


        public static void getOrderBy()
        {
            var orderArr = orderByAsc(persons);
            getPersons(orderArr);
        }
        
    }
}
