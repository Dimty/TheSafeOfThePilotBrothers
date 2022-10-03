using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TheSafeOfThePilotBrothers
{
    internal class VerticalCondition : ICondition
    {
        public VerticalCondition(Lever lever)
        {
            lever.Background = Brushes.DarkRed;
        }
        public void Change(Lever lever)
        {
            lever.condition = new HorizontalCondition(lever);
        }

        public string GetCondition()
        {
            return "--";
        }
    }
}
