namespace Toolbox
{
    public static class Debugger
    {
        public static void Launch()
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Launch();
            }
        }
    }
}
