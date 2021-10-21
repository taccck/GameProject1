using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public abstract class QTE : MonoBehaviour
    {
        [Tooltip("% / interval")]
        [SerializeField]
        protected float percentage;

        [Tooltip("Padding for red bar, in width/2")]
        [SerializeField]
        protected int padding = 1;

        [SerializeField] protected float interval = 1f;
        [SerializeField] protected float cooldown = 1f;

        protected Transform progress;
        protected Transform bar;
        protected Transform red;
        protected bool started = false;
        protected bool done = false;
        protected Vector2 input = Vector2.zero;
        protected bool cd = false;

        public abstract bool Isfilled();

        public virtual bool Startbar()
        {
            if (!started)
            {
                started = true;
                return true;
            }

            return false;
        }

        public abstract void Interact();

        public virtual Vector2 Checkinput()
        {
            if (input == Vector2.zero)
                Generateinput();
            return input;
        }

        protected virtual IEnumerator Cooldown()
        {
            cd = true;
            yield return new WaitForSeconds(cooldown);
            cd = false;
        }

        protected virtual void Generateinput()
        {
            int dir = Random.Range(0, 4);
            if (dir == 0)
                input.x = 1;
            else if (dir == 1)
                input.x = -1;
            else if (dir == 2)
                input.y = 1;
            else if (dir == 3)
                input.y = -1;
        }

        protected abstract IEnumerator Addprogress();

        protected abstract void Redloc();
    }
}