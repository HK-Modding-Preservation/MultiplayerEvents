using AbilityChanger.Base;
using Satchel.Futils;
using static AbilityChanger.AbilityChanger;

namespace MultiplayerEvents.JobsModule.Base.AbilityChanger
{
    public class FocusBaseAbility : Focus
    {
        protected IAbilityContainer abilityContainer;
        public FocusBaseAbility(IAbilityContainer parent)
        {
            this.abilityContainer = parent;
            RegisterTrigger(abilityContainer.OnTriggerLocal, true);
            RegisterAbility(this);
        }

        public new void RegisterTrigger(Action triggerFunc, bool shouldContinue)
        {
            OnSelect += () =>
            {
                myFsm.Intercept(new TransitionInterceptor()
                {
                    fromState = "Focus Heal",
                    toStateDefault = states.FullHP,
                    eventName = "WAIT",
                    toStateCustom = shouldContinue ? states.FullHP : states.Cancel,
                    shouldIntercept = () => true,
                    onIntercept = (a, b) => triggerFunc()
                });
                myFsm.Intercept(new TransitionInterceptor()
                {
                    fromState = states.FocusHeal2,
                    toStateDefault = states.FullHP2,
                    eventName = "WAIT",
                    toStateCustom = shouldContinue ? states.FullHP2 : states.Cancel,
                    shouldIntercept = () => true,
                    onIntercept = (a, b) => triggerFunc()
                });
            };
            //base.RegisterTrigger(triggerFunc, shouldContinue);


        }

        public override string name { get => abilityContainer.GetName(); set { } }
        public override string title { get => abilityContainer.GetName(); set { } }
        public override string description { get => abilityContainer.GetDescription(); set { } }
        public override Sprite activeSprite { get => abilityContainer.GetAbilityIcon(); set { } }

    }
}
