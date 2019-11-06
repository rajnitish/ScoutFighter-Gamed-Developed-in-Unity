static class Globals
{
    // global int 
    public static int counter;
    public static int RHSlandedTrooper;
    public static int LHSlandedTrooper;
    public static int Level;
    public static int counter_plane; 
    public static int counter_paratrooper;
    public static int counter_fighter;
    public static int counter_missile;
    public static int counter_bullet;

    public static int Life;

    static Globals()
    {
        Life = 3;
        Level = 1;
        counter_plane = 0;
        counter_fighter = 0;
        counter_paratrooper = 0;
        counter_missile = 0;
        counter_bullet = 0;
     

    }
    // global int using get/set
    static int _getsetcounter;
    public static int getsetcounter
    {
        set { _getsetcounter = value; }
        get { return _getsetcounter; }
    }
}
