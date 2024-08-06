using System;

namespace Assets.CodeBase.Data
{
    [Serializable]
    public class State
    {
        public float MaxHealth;
        public float CurrentHealth;

        public State() { }

        public State(State state)
        {
            MaxHealth = state.MaxHealth;
            CurrentHealth = state.CurrentHealth;
        }

        public void ResetHealth() =>
            CurrentHealth = MaxHealth;
    }
}