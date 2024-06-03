namespace WeightBalance.Data;


public static class WbGlobals
{
    private static bool preselectDone = false;
    public static bool PreSelectDone 
    { 
        get { return preselectDone; } 
        set { preselectDone = value; }
    }
}