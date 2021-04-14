using Injection;

namespace UnityCommonHelpers._Demo._InjectorDemo
{
    public class Calculator: IInjectable
    {
        public int Addition(int x, int y)
        {
            return x + y;
        }

        public int Subtraction(int x, int y)
        {
            return x - y;
        }
    }
}
