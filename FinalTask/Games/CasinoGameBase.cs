using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask.Games
{
    public abstract class CasinoGameBase
    {
        public event Action OnWin;
        public event Action OnLose;
        public event Action OnDraw;

        public abstract void PlayGame();

        protected abstract void FactoryMethod();

        protected virtual void OnWinInvoke()
        {
            OnWin?.Invoke();
        }

        protected virtual void OnLoseInvoke()
        {
            OnLose?.Invoke();
        }

        protected virtual void OnDrawInvoke()
        {
            OnDraw?.Invoke();
        }

        protected CasinoGameBase()
        {
            FactoryMethod();
        }
    }
}
