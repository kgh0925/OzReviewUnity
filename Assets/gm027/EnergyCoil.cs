using System;

namespace GM027
{
    [Serializable]
    public class EnergyCoil
    {
        public int powerValue;

        public EnergyCoil(int value)
        {
            powerValue = value;
        }
    }
}
