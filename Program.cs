class Program
{   
    const int TOTAL_ENTRADAS = 4;
    int[] opcion = {15000, 30000, 10000, 40000};  
    static void Main(string[] args)
    {
        Cliente cliente = new Cliente("", "","", 0,0);
        SortedDictionary<int, Cliente> DiccionarioClientes = new SortedDictionary<int, Cliente>();
        int numMenu = 0;
        int idIngresado = 0;
        int tipoEntradaNueva = 0;
        int tipoEntrada = 0;
        double totalAbonado = 0;
        double totalAbonadoNuevo = 0;
        int ultimoID = 0;
        int[] opcion = {15000, 30000, 10000, 40000};    
        do{
            numMenu = IngresarInt("1. Nueva Inscripción | 2. Obtener estadísticas del evento | 3. Buscar Cliente | 4. Cambiar entrada de un cliente | 5. Salir");
            switch(numMenu)
            {
                case 1: 
                ultimoID = Tiquetera.DevolverUltimoID();
                cliente = ObtenerCliente(cliente, opcion, tipoEntrada, totalAbonado);
                Console.WriteLine("ID: " + ultimoID); 
                DiccionarioClientes.Add(ultimoID, cliente); //CHEQUEAR EN KSA
                
                break;

                case 2: 
                if(ultimoID == 0){  
                    Console.WriteLine("Aún no se ingresaron personas en la lista.");
                }else{
                    double totalConseguido = 0;
                    int totalEntradasVendidas = 0; 
                    int[] entradas = new int[TOTAL_ENTRADAS];
                    Console.WriteLine("Estadisticas del Evento: ");
                    Console.WriteLine($"Cantidad de Clientes inscriptos: {ultimoID}");
                    foreach(int clave in DiccionarioClientes.Keys){
                        totalConseguido += DiccionarioClientes[clave].TotalAbonado;
                        entradas[DiccionarioClientes[clave].TipoEntrada-1]++;
                        totalEntradasVendidas++;
                    }
                    Console.WriteLine($"Porcentaje de entradas diferenciadas por tipo respecto al total: ");
                    for(int i = 0; i < entradas.Length; i++){
                        Console.WriteLine($"Tipo de entrada {i+1}: {(entradas[i]/totalEntradasVendidas)*100}% del total");
                    }                    
                    Console.WriteLine("Recaudacion de cada día:");
                    for(int i = 0; i<entradas.Length;i++){
                        Console.WriteLine($"Día {i+1}: ${entradas[i]*opcion[i]} ");
                    }
                    Console.WriteLine($"Recaudacion total: {totalConseguido}");
                }
                break;

                case 3: 
                idIngresado = IngresarInt("Ingrese el ID de la persona a buscar: ");
                if(DiccionarioClientes.ContainsKey(idIngresado)){
                    cliente = DiccionarioClientes[idIngresado];
                    Console.WriteLine("DNI: " + cliente.DNI);
                    Console.WriteLine($"Nombre: {cliente.Nombre}");
                    Console.WriteLine($"Apellido: {cliente.Apellido}");
                    Console.WriteLine($"Total abonado: {cliente.TotalAbonado}");
                }else{
                    Console.WriteLine("No se encontró...");
                }
                break;

                case 4: 
                idIngresado = IngresarInt("Ingrese su ID:");
                if(DiccionarioClientes.ContainsKey(idIngresado) && cliente.TotalAbonado - opcion[cliente.TipoEntrada-1] > 0){
                    tipoEntradaNueva = IngresarInt("Nuevo tipo de entrada: ");
                    VerificacionTipoEntrada(tipoEntradaNueva);
                    totalAbonadoNuevo = IngresarDouble("Total a abonar: ");
                    VerificacionTotalAbonado(totalAbonadoNuevo, tipoEntradaNueva, opcion);
                    if(totalAbonadoNuevo > cliente.TotalAbonado){
                        cliente.CambiarEntrada(tipoEntradaNueva,totalAbonadoNuevo);
                        Console.WriteLine("Su entrada se ha cambiado exitosamente.");
                    }else{
                        Console.WriteLine("No se puede cambiar su entrada. El nuevo precio abonado debe ser mayor al anterior...");
                    }
                }else{
                    Console.WriteLine("Su ID no fue encontrado o el precio abonado previamente no es mayor al costo de entrada, por lo que no puede cambiarla...");
                }
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
    static double VerificacionTotalAbonado(double totalAbonado, int tipoEntrada, int[] opcion){
        double vuelto = 0;
        while(totalAbonado < opcion[tipoEntrada-1]){
            totalAbonado = IngresarDouble("El total abonado es menor al precio de la entrada. Total a abonar: ");
        }
        if(totalAbonado > opcion[tipoEntrada-1]){
            vuelto = totalAbonado - opcion[tipoEntrada-1];
            Console.WriteLine("Vuelto: " + vuelto);
        }
        return totalAbonado -= vuelto;
    }
    static Cliente ObtenerCliente(Cliente clienteInfo, int[] opcion, int tipoEntrada, double totalAbonado){
        string dni = IngresarString("Ingrese el DNI del cliente: ");
        string apellido = IngresarString("Apellido del cliente: ");
        string nombre = IngresarString("Nombre del cliente: ");
        tipoEntrada = IngresarInt("Tipo de entrada: ");
        VerificacionTipoEntrada(tipoEntrada);
        totalAbonado = IngresarDouble("Total a abonar: ");
        totalAbonado = VerificacionTotalAbonado(totalAbonado, tipoEntrada, opcion);
        clienteInfo = new Cliente(dni, apellido, nombre, tipoEntrada, totalAbonado);
        return clienteInfo;
    }
}