using System;
using CleverCrow.Fluid.Dialogues;

namespace Dialogue
{
    [Serializable]
    public struct ActorSpeak : IEquatable<ActorSpeak>
    {
        public IActor Actor;
        public string text;

        public ActorSpeak(IActor actor, string text)
        {
            Actor = actor;
            this.text = text;
        }
        
        public bool Equals(ActorSpeak other)
        {
            return Actor == other.Actor && text == other.text;
        }

        public override bool Equals(object obj)
        {
            return obj is ActorSpeak other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Actor, text);
        }
    }
}