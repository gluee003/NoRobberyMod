using HarmonyLib;

namespace NoRobberyMod
{
    public static class PInfo
    {
        // each loaded plugin needs to have a unique GUID. usually author+generalCategory+Name is good enough
        public const string GUID = "gluee.lbol.norobbery";
        public const string Name = "No Robbery Mod";
        public const string version = "1.0.1";
        public static readonly Harmony harmony = new Harmony(GUID);

    }
}
