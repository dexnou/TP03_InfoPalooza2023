class Tiquetera{

    private static int UltimoID = 0; //en realidad en la consigna dice que debe empezar en 1...

    public static int DevolverUltimoID(){
        UltimoID = UltimoID+1;
        return UltimoID;
    }
}