using Assignment.Entity;

namespace Assignment.State
{
    public abstract class BaseState<E>
    {        
        public abstract void Enter(E entity);
        public abstract void Execute(E entity);
        public abstract void Exit(E entity);
    }
}