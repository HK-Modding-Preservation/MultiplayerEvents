using MultiplayerEvents.JobsModule.Objects.Base;

namespace MultiplayerEvents.JobsModule.Abilities.MinorHeal
{
    internal class PrefabFactory : IPrefabFactory
    {
        public GameObject BuildPersistentPrefab()
        {
            var go = new GameObjectBuilder("Minor Heal Effect")
                .AsPersistent()
                .WithCircle2DCollider(5.8f, true)
                .WithSprite(AssetManager.MinorHealFlashSprite)
                .Get();

            return go;
        }

    }
}
