class Program
{   
    
    static void Main(string[] args)
    {
        Cliente cliente = new Cliente("", "","", 0,0);
        //Tiquetera ID = new ID();
        SortedDictionary<int, Cliente> DiccionarioClientes = new SortedDictionary<int, Cliente>();
        int iterador = 0;
        int numMenu = 0;
        do{
            numMenu = IngresarInt("1. Nueva Inscripción | 2. Obtener estadísticas del evento | 3. Buscar Cliente | 4. Cambiar entrada de un cliente | 5. Salir");
            switch(numMenu)
            {
                case 1: 
                iterador++; 
                cliente = ObtenerCliente(cliente);
                Console.WriteLine("ID: " + iterador); //pasarlo a la tiquetera. 
                DiccionarioClientes.Add(iterador, cliente); //CHEQUEAR EN KSA
                break;

                case 2: 
                if(iterador == 0){  
                    Console.WriteLine("Aún no se ingresaron personas en la lista.");
                }else{
                    Console.WriteLine("Estadisticas del Evento: ");
                    Console.WriteLine($"Cantidad de Clientes inscriptos: {iterador}");
                    Console.WriteLine($"");
                }
                break;

                case 3: 
                int idIngresado = IngresarInt("Ingrese el ID de la persona a buscar: ");
                if(DiccionarioClientes.ContainsKey(idIngresado)){
                    Console.WriteLine($"DNI: {cliente.DNI}"); 
                    Console.WriteLine($"Nombre: {cliente.Nombre}");
                    Console.WriteLine($"Apellido: {cliente.Apellido}");
                    Console.WriteLine($"Total abonado: {cliente.TotalAbonado}");
                }else{
                    Console.WriteLine("No se encontró...");
                }
                break;

                case 4: 
                break;

                case 5: 
                break;
            }
        }while(numMenu != 5);
    }
    static string IngresarString(string mensaje){
        string palabra;
        Console.WriteLine(mensaje);
        palabra = Console.ReadLine();
        return palabra;
    }
    static int IngresarInt(string mensaje){
        int num;
        Console.WriteLine(mensaje);
        num = int.Parse(Console.ReadLine());
        return num;
    }
    static double IngresarDouble(string mensaje){
        double num;
        Console.WriteLine(mensaje);
        num = double.Parse(Console.ReadLine());
        return num;
    }
    static void VerificacionTipoEntrada(int tipoEntrada){
        while(tipoEntrada <= 0 || tipoEntrada >= 5){
            tipoEntrada = IngresarInt("El tipo ingresado es imposible. Tipo de entrada: ");
        }
    }
    static void VerificacionTotalAbonado(double totalAbonado, int tipoEntrada, int[] opcion){
        while(totalAbonado < opcion[tipoEntrada-1]){
            totalAbonado = IngresarDouble("El total abonado es menor al precio de la entrada. Total a abonar: ");
        }
        if(totalAbonado > opcion[tipoEntrada-1]){
            double vuelto = totalAbonado - opcion[tipoEntrada-1];
            Console.WriteLine("Vuelto: " + vuelto);
        }
    }

    static Cliente ObtenerCliente(Cliente clienteInfo){
        int[] opcion = new int[4];    
        opcion[0] = 15000;
        opcion[1] = 30000;
        opcion[2] = 10000;
        opcion[3] = 40000;
        string dni = IngresarString("Ingrese el DNI del cliente: ");
        string apellido = IngresarString("Apellido del cliente: ");
        string nombre = IngresarString("Nombre del cliente: ");
        int tipoEntrada = IngresarInt("Tipo de entrada: ");
        VerificacionTipoEntrada(tipoEntrada);
        double totalAbonado = IngresarDouble("Total a abonar: ");
        VerificacionTotalAbonado(totalAbonado, tipoEntrada, opcion);
        clienteInfo = new Cliente(dni, apellido, nombre, tipoEntrada, totalAbonado);
        return clienteInfo;
    }
}