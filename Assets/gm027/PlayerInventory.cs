using System.Collections.Generic;
using UnityEngine;

namespace GM027
{
    public class PlayerInventory : MonoBehaviour
    {
        public List<EnergyCoil> coils = new List<EnergyCoil>();

        public void Add(EnergyCoil coil)
        {
            coils.Add(coil);
        }

        public EnergyCoil UseCoil()
        {
            if (coils.Count == 0) return null;

            EnergyCoil coil = coils[0];
            coils.RemoveAt(0);
            return coil;
        }
    }
}
