namespace CosmosSubCollections
{
    public class CosmosDbPerformanceMonitor
    {
        private readonly object syncRoot = new object();
        private int requests;
        private double charge;

        public int Requests
        {
            get
            {
                lock (this.syncRoot)
                {
                    return this.requests;
                }
            }
        }

        public double Charge
        {
            get
            {
                lock (this.syncRoot)
                {
                    return this.charge;
                }
            }
        }

        public void Increment(double charge)
        {
            lock (this.syncRoot)
            {
                this.requests++;
                this.charge += charge;
            }
        }
    }
}