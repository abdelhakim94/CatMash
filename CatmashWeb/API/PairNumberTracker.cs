namespace Catmash.Web
{
    public class PairNumberTracker
    {
        private int pairNumber = 0;
        private object mutex = new object();

        public int NextPair()
        {
            lock (mutex)
            {
                try
                {
                    checked
                    {
                        return pairNumber++;
                    }
                }
                catch (System.OverflowException)
                {
                    pairNumber = 0;
                    return pairNumber++;
                }
            }
        }
    }
}