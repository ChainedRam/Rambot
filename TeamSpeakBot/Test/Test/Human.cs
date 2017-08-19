
using System;

namespace Test
{
    class Human
    {
        public void eat(Food food)
        {
            Console.WriteLine("I eat "+ food.Name); 
        }

        public void drink()
        {
            Console.WriteLine("I drink water");
        }


        public virtual void haveMeal()
        {
            drink(); 
        }
    }
}
