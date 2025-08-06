using UnityEngine;

namespace dev.nicklaj.clibs.ClassExtensions
{
    public static class FloatExtensions
    {
        public static byte ToByte(this float f) {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }
    }
}