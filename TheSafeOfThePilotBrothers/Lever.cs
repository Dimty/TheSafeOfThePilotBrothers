using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TheSafeOfThePilotBrothers
{
    internal class Lever:Button
    {
        public ICondition condition;
        public int X { get;init; }
        public int Y { get;init; }

        public Lever()
        {
            condition = new VerticalCondition(this);
        }
        public ICondition Flip()
        {
            condition.Change(this);
            this.Content = condition.GetCondition();
            return condition;
        }
    }
}
