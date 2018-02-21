using Assignment.Entity;

namespace Assignment.State
{
    abstract class BaseState<E>
    {        
        public abstract void Enter(E entity);
        public abstract void Execute(E entity);
        public abstract void Exit(E entity);
    }
}