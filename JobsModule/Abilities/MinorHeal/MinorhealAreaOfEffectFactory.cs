using MultiplayerEvents.JobsModule.Objects;

namespace MultiplayerEvents.JobsModule.Abilities.MinorHeal
{
    public class MinorhealAreaOfEffectFactory : BaseAreaOfEffectFactory {
        public override AreaOfEffectConfig Config => new AreaOfEffectConfig { AutoRemove = true, AutoRemoveSeconds = 1f, MultiTrigger = false };

        public MinorhealAreaOfEffectFactory() {
           spawner = new AreaOfEffectObjectManager(ConstructPrefab());
        }
        public GameObject ConstructPrefab()
        {
            var go = new GameObject();
            go.layer = (int)GlobalEnums.PhysLayers.HERO_DETECTOR;
            var col = go.GetAddComponent<CircleCollider2D>();
            var spr = go.GetAddComponent<SpriteRenderer>();
            spr.sprite = AssetManager.MinorHealSprite;
            spr.transform.localScale = Vector3.one * 8;
            col.radius = 8f;
            col.isTrigger = true;
            GameObject.DontDestroyOnLoad(go);
            go.SetActive(false);
            return go;
        }

        public override void OnHeroTrigger()
        {
            HeroController.instance.AddHealth(1);
        }
    }
}
