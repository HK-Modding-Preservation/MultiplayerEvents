namespace MultiplayerEvents.JobsModule.Objects.Base
{
    public class GameObjectBuilder
    {
        private GameObject current;
        public GameObjectBuilder(string Name)
        {
            current = new GameObject(Name);
            current.SetActive(false);
        }
        public GameObjectBuilder AsPersistent()
        {
            GameObject.DontDestroyOnLoad(current);
            return this;
        }
        public GameObjectBuilder WithCircle2DCollider(float radius, bool isTrigger)
        {
            var col = current.GetAddComponent<CircleCollider2D>();
            col.radius = radius;
            col.isTrigger = isTrigger;
            return this;
        }

        public GameObjectBuilder WithSprite(Sprite sprite)
        {
            var spr = current.GetAddComponent<SpriteRenderer>();
            spr.sprite = sprite;
            return this;
        }

        public GameObject Get() => current;
    }
}
