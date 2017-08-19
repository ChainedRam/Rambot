using System;


namespace Test
{
    partial class Program
    {
     
        static void Main(string[] args)
        {

            Food f = new Food();
            f.Name = "Apple";

            Human o = new Human();

            o.drink();

            o.eat(f); 

            //type name = value; 

            /* int > int 
             * int < int 
             * int <= int 
             * int >= int 
             * 
             * int == int 
             * int != int 
             * 
             */

        

            //break
            Console.ReadLine();
        }
    }

}
