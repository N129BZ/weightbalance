namespace WeightBalance;


public static class Globals
{
    private static bool preselectDone = false;
    public static bool PreSelectDone 
    { 
        get { return preselectDone; } 
        set { preselectDone = value; }
    }
}