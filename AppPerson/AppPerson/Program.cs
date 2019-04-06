using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace AppPerson
{
    public class Pila
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
                Console.WriteLine("¡Ingrese una cantidad correcta presione enter!");
                Console.ReadKey();
                AddQuantityPerson();
                return;
            }

            size = int.Parse(input);

            if (size < 1)
            {
                Console.Clear();
                Console.WriteLine("¡Ingrese una cantidad correcta presione enter!");
                Console.ReadKey();
                AddQuantityPerson();
                return;
            }

            if (!addElement)
            {
                persons = new string[size];
            }
            else
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
                Console.Write("1. Ingresar datos");
                Console.Write("\n2. Mostrar datos");
                Console.Write("\n3. Eliminar el ultimo elemento");
                Console.Write("\n4. Ordenar el listado en forma ascendente");
                Console.Write("\n5. Buscar edad especifica");
                Console.Write("\n6. Sí desea salir presione 'SI'");
                Console.Write($"\n7. Cantidad de elementos a ingresar '{size}'");
                Console.Write("\nIngrese la opción: ");
                optionSelected = Console.ReadLine();

                if (isNumber(optionSelected))
                {
                    typeOptionSelected((State)Convert.ToInt32(optionSelected) - 1);
                }

            } while (!optionSelected.Equals("SI"));

        }

        public static void typeOptionSelected(State option)
        {
            if (hasEmptyArray() && option != State.SAVE)
            {
                Console.Clear();
                Console.WriteLine("¡La lista esta vacia!");
                messageBackMain();
                return;
            }
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
                        Console.WriteLine("¡Esta lleno la pila de elementos si desea ingresar más datos presione enter!");
                        Console.ReadKey();
                        AddQuantityPerson(true);
                    }
                    break;
                case State.GET:
                    Console.Clear();
                    Console.WriteLine("Listado de edades de personas: ");
                    getPersons(persons);
                    break;
                case State.ORDER:
                    Console.Clear();
                    Console.WriteLine("Personas ordenadas por edades en forma ascendente: ");
                    getOrderBy();
                    break;
                case State.SEARCH:
                    Console.Clear();
                    search();
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
            Console.Write("Ingrese la cantidad de persona: ");
            Person person = new Person();
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine($"La edad ingresada es incorrecta presione enter!");
                Console.ReadKey();
                savePerson();
            }
            else
            {
                person.age = Convert.ToInt32(input);

                if (person.age < 1 || person.age > 120)
                {
                    Console.WriteLine($"La edad ingresada es incorrecta presione enter!");
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
                if (arr[i] != null)
                {
                    Console.Write($"{i + 1}: {arr[i]}\n");
                }
            }
            messageBackMain();
        }

        public static int deleteLastElment()
        {
            int delete = 0;
            if (!hasEmptyArray())
            {
                for (int i = persons.Length - 1; i >= 0; i--)
                {
                    if (persons[i] != null && delete == 0)
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
                Console.WriteLine("El listado esta vacio!");
            }
            else
            {
                Console.WriteLine("El último elemento fue eliminado con exito!");
            }

            messageBackMain();
        }

        public static void messageBackMain()
        {
            Console.Write("¡Presione enter para regresar!");
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

        public static void addSpaceArray(int size)
        {
            var tempPersons = persons;
            Array.Resize(ref persons, tempPersons.Length + size);
        }

        public static int counterArray()
        {
            int counter = 0;
            for (int i = persons.Length - 1; i >= 0; i--)
            {
                if (persons[i] != null)
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
                for (int j = arr.Length - 1; j >= i; j--)
                {
                    if (int.Parse(arr[j - 1]) > int.Parse(arr[j]))
                    {
                        found = arr[j - 1];
                        arr[j - 1] = arr[j];
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

        public static string[] find(string input)
        {
            string[] found = new string[2];
            found[0] = "";
            found[1] = "";
            for (int i = persons.Length - 1; i >= 0; i--)
            {
                if (persons[i].Equals(input))
                {
                    found[0] = Convert.ToString(i);
                    found[1] = persons[i];
                }
            }
            return found;
        }

        public static void search()
        {
            Console.WriteLine("Ingrese la edad a buscar: ");
            var input = Console.ReadLine();
            if (String.IsNullOrEmpty(input))
            {
                Console.Write($"La edad ingresada es incorrecta presione enter!");
                Console.ReadKey();
                search();
            }
            else
            {
                var person = find(input);
                if (person[1].Equals(""))
                {
                    Console.WriteLine($"No se encontro información de la persona con la edad ingresada: {input}!");
                }
                else
                {
                    Console.WriteLine($"{int.Parse(person[0]) + 1}°. persona  su edad es de: {person[1]}");
                }
                messageBackMain();
            }
        }

        //    class Nodo
        //    {
        //        public int info;
        //        public Nodo sig;
        //    }

        //    private Nodo raiz;

        //    public Pila()
        //    {
        //        raiz = null;
        //    }

        //    public void Insertar(int x)
        //    {
        //        Nodo nuevo;
        //        nuevo = new Nodo();
        //        nuevo.info = x;
        //        if (raiz == null)
        //        {
        //            nuevo.sig = null;
        //            raiz = nuevo;
        //        }
        //        else
        //        {
        //            nuevo.sig = raiz;
        //            raiz = nuevo;
        //        }
        //    }

        //    public int Extraer()
        //    {
        //        if (raiz != null)
        //        {
        //            int informacion = raiz.info;
        //            raiz = raiz.sig;
        //            return informacion;
        //        }
        //        else
        //        {
        //            return int.MaxValue;
        //        }
        //    }

        //    public void Imprimir()
        //    {
        //        Nodo reco = raiz;
        //        Console.WriteLine("Listado de todos los elementos de la pila.");
        //        while (reco != null)
        //        {
        //            Console.Write(reco.info + "-");
        //            reco = reco.sig;
        //        }
        //        Console.WriteLine();
        //    }

        //    static void Main(string[] args)
        //    {
        //        Pila pila1 = new Pila();
        //        pila1.Insertar(12);
        //        pila1.Insertar(10);
        //        pila1.Insertar(1);
        //        pila1.Imprimir();
        //        //Console.WriteLine("Extraemos de la pila:" + pila1.Extraer());
        //        //pila1.Imprimir();
        //        Console.ReadKey();
        //    }
        //}
    }
}
