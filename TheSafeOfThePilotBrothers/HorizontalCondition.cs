using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TheSafeOfThePilotBrothers
{
    internal class HorizontalCondition : ICondition
    {
        public HorizontalCondition(Lever lever)
        {
            lever.Background = Brushes.Blue;
        }
        public void Change(Lever lever)
        {
            lever.condition = new VerticalCondition(lever);
        }

        public string GetCondition()
        {
            return "|";
        }
    }
}
