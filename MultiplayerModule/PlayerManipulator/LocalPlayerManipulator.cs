using GlobalEnums;

namespace MultiplayerEvents.MultiplayerModule.PlayerManipulator
{
    public class LocalPlayerManipulator : ILocalPlayerManipulator
    {
        public HeroController Hc => HeroController.instance;

        public static ILocalPlayerManipulator Instance = new LocalPlayerManipulator();

        private float damageScale = 1.0f;

        public LocalPlayerManipulator()
        {
            On.HeroController.TakeDamage += HeroControllerTakeDamage;
        }

        private void HeroControllerTakeDamage(On.HeroController.orig_TakeDamage orig, HeroController self, GameObject go, CollisionSide damageSide, int damageAmount, int hazardType)
        {
            int scaledDamage = (int)Math.Round(damageAmount * damageScale);
            orig(self, go, damageSide, scaledDamage, hazardType);
        }

        public void AddHealth(int amount)
        {
            Hc.AddHealth(amount);
        }

        public GameObject GetMainObject()
        {
            return Hc.gameObject;
        }

        public void MakeInvisible()
        {
            Hc.gameObject.GetComponent<Renderer>().enabled = false;
        }

        public void MakeVisible()
        {
            Hc.gameObject.GetComponent<Renderer>().enabled = true;
        }

        public void MakeInvulnerable()
        {
            damageScale = 0f;
        }

        public void MakeVulnerable()
        {
            damageScale = 1f;
        }

        public void DamageEvasionEffect()
        {
            Hc.carefreeShield.SetActive(true);
        }

        public void HealEffect()
        {
            Hc.GetComponent<SpriteFlash>().flashFocusHeal();

        }
    }
}
