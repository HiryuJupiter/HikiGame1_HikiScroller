using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Choices;

namespace Dialogue
{
    [Serializable]
    public class ActorSpeakWithChoice : IEquatable<ActorSpeakWithChoice>
    {
        public IActor Actor;
        public string text;
        public List<IChoice> Choices;

        public ActorSpeakWithChoice(IActor actor, string text, List<IChoice> choices)
        {
            Actor = actor;
            this.text = text;
            Choices = choices;
        }

        public bool Equals(ActorSpeakWithChoice other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Actor, other.Actor) && string.Equals(text, other.text) && Equals(Choices, other.Choices);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ActorSpeakWithChoice) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Actor, text, Choices);
        }
    }
}