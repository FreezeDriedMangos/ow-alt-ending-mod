using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using NewHorizons;

namespace AltEnding.Utilities
{
    public static class Wrappers
    {
        public static GameObject SpawnGameObject(GameObject planet, Sector sector, string propToCopyPath, Vector3 position, Vector3 eulerAngles, float scale=1, bool alignWithNormal=false)
        {
            return AltEnding.newHorizonsAPI.SpawnObject(planet, sector, propToCopyPath, position, eulerAngles, scale, alignWithNormal);
        }

        public static Texture2D GetTexture(string filename)
        {
            // return NewHorizons.Utility.ImageUtilities.GetTexture(AltEnding.Instance, filename);
            return null;
        }
    }
}
