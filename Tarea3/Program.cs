
using System.Xml;
using System.Xml.Linq;

// Daniel Arturo Amarillo Jorge 1032937350 
// Grupo 213023_120\
// Programación
// Problema: El Guardian
// Código fuente: Autoría propia
// Metodos: Verificar, CambioIngreso, EnlistarLista, AgregarCli

namespace Tarea3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "parqueadero.xml";
            if (File.Exists(path) == false)
            {
                CrearArchivo();
            }

            XmlDocument doc = new XmlDocument();
            pre1:
            doc.Load("parqueadero.xml");
            Console.WriteLine("Bienvenido al Parqueadero El Guardian");
            Console.WriteLine("Ingresa 1, 2, 3 según la opciónm que quieras.");
            Console.WriteLine("1 = Lista de autos | 2 = Ingresar Auto | 3 = Retirar Auto | 4 = Consultar si un auto está en el parqueadero");
            int pr1 = Convert.ToInt32(Console.ReadLine());
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "urn:samples");
            XmlNode root = doc.DocumentElement;
            Console.Clear();
            if (pr1 == 1)
            {
                Console.Clear();
                EnlistarLista();
                Console.WriteLine(" ");
                Console.WriteLine("Da Enter para volver...");
                Console.ReadKey();
                Console.Clear();
                goto pre1;
            }
            else if (pr1 == 2)
            {
                Console.Clear();
                Console.WriteLine("Ingresa la placa del auto: ");
                string pr2 = Convert.ToString(Console.ReadLine());
                string verify = "";
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if(node.Attributes["id"].Value == pr2)
                    {
                        verify = "Si";
                    }
                }

                if (verify == "Si")
                {
                    var xdoc = XDocument.Load("parqueadero.xml");
                    var tgt = xdoc.Root.Descendants("Cliente").FirstOrDefault(x => x.Attribute("id").Value == pr2);
                    var estado = tgt.Descendants("Esta").FirstOrDefault();
                    estado.Value = "Si";
                    xdoc.Save("parqueadero.xml");
                    Console.WriteLine("Auto ingresado");
                    Console.WriteLine("Aprieta cualquier tecla para continuar..");
                    Console.ReadKey();
                    Console.Clear();
                    goto pre1;
                }
                else
                {
                    Console.WriteLine("El auto es nuevo, así que lo crearemos desde 0");
                    Console.WriteLine("Ingresa la marca");
                    string pr3 = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Ingresa la modelo");
                    string pr4 = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Ingresa la nombre del cliente");
                    string pr5 = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Ingresa la celular del cliente");
                    string pr6 = Convert.ToString(Console.ReadLine());
                    AgregarCli(pr2, pr3, pr4, pr5, pr6);
                    Console.WriteLine("Auto Ingresado. Presiona Cualquier tecla para continuar...");
                    pr2 = "";
                    Console.ReadKey();
                    Console.Clear();
                    goto pre1;
                }

                

            }
            else if (pr1 == 3)
            {   

                Console.WriteLine("Ingresa la matricula: ");
                var pr3 = Convert.ToString(Console.ReadLine());
                CambioIngreso(pr3);
                Console.WriteLine("Auto Retirado");
                Console.WriteLine("Aprieta cualquier tecla para continuar..");
                Console.ReadKey();
                Console.Clear();
                goto pre1;
            }else if (pr1 == 4)
            {
                Console.WriteLine("Ingresa la placa");
                var pr4 = Convert.ToString(Console.ReadLine());
                verificar(pr4);
                Console.WriteLine("Aprieta cualquier tecla para continuar..");
                Console.ReadKey();
                Console.Clear();
                goto pre1;
            }
        }

        public static void verificar(string pr4)
        {
            var xdoc = XDocument.Load("parqueadero.xml");
            var tgt = xdoc.Root.Descendants("Cliente").FirstOrDefault(x => x.Attribute("id").Value == pr4);
            var pre = tgt.Descendants("Esta").FirstOrDefault();
            var estado = pre.Value.ToString();
            Console.WriteLine($"El auto {estado} está en el parqueadero");
            Console.WriteLine(" ");
        }
        
        public static void CambioIngreso (string n1)
        {
            var xdoc = XDocument.Load("parqueadero.xml");
            var tgt = xdoc.Root.Descendants("Cliente").FirstOrDefault(x => x.Attribute("id").Value == n1);
            var estado = tgt.Descendants("Esta").FirstOrDefault();
            estado.Value = "No";
            xdoc.Save("parqueadero.xml");

        }
        public static void EnlistarLista()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("parqueadero.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                String id = node.Attributes["id"].Value;
                Console.WriteLine(" ");
                Console.WriteLine(id);

                if (node.HasChildNodes)
                {
                    
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        
                        Console.WriteLine(node.ChildNodes[i].Name + " : " + node.ChildNodes[i].InnerText);
                    }
                }
            }
        }
        public static void AgregarCli(string placaP, string MarcaP, string ModeloP, string NombreP, string CelularP)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("parqueadero.xml");
            XmlNode cliente = doc.CreateElement("Cliente");

            XmlAttribute atr = doc.CreateAttribute("id");
            atr.Value = placaP;
            cliente.Attributes.SetNamedItem(atr);

            XmlNode esta = doc.CreateElement("Esta");
            esta.InnerText = "Si";
            cliente.AppendChild(esta);

            XmlNode marca = doc.CreateElement("Marca");
            marca.InnerText = MarcaP;
            cliente.AppendChild(marca);

            XmlNode modelo = doc.CreateElement("Modelo");
            modelo.InnerText = ModeloP;
            cliente.AppendChild(modelo);

            XmlNode nombre = doc.CreateElement("Nombre");
            nombre.InnerText = NombreP;
            cliente.AppendChild(nombre);

            XmlNode celular = doc.CreateElement("Celular");
            celular.InnerText = CelularP;
            cliente.AppendChild(celular);



            doc.DocumentElement.AppendChild(cliente);
            doc.Save("parqueadero.xml");
            Console.WriteLine("Listo");
        }

        public static void CrearArchivo()
        {

            XmlWriter xmlWriter = XmlWriter.Create("parqueadero.xml");

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Parqueadero");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
    }
}