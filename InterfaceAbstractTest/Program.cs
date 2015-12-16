namespace InterfaceAbstractTest
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            // Class A implemented Interface.
            InterfaceA a = new A();
            a.InterfaceMethod();
            a.InterfaceField = 3;
            Console.WriteLine("a.InterfaceField = " + a.InterfaceField);

            // Class B implemented 2 Interfaces and abstract class.
            InterfaceA b1 = new B();
            b1.InterfaceMethod();
            b1.InterfaceField = 3;
            Console.WriteLine("b1.InterfaceField = " + b1.InterfaceField);

            // Bind OnChange event
            (b1 as InterfaceB).OnChange += () => { Console.WriteLine("(b1 as InterfaceB).OnChange Event"); };
            (b1 as InterfaceB).CallEvent();
            
            (b1 as AbstractClass).AbstractField = 1;
            (b1 as AbstractClass).NormalField = 2;
            (b1 as AbstractClass).VirtualField = 3;

            Console.WriteLine("(b1 as AbstractClass).AbstractField = " + (b1 as AbstractClass).AbstractField);
            Console.WriteLine("(b1 as AbstractClass).NormalField = " + (b1 as AbstractClass).NormalField);
            Console.WriteLine("(b1 as AbstractClass).VirtualField = " + (b1 as AbstractClass).VirtualField);

            (b1 as AbstractClass).AbstractMethod();
            (b1 as AbstractClass).VirtualMethod();
        }
    }

    public class A : InterfaceA
    {
        public void InterfaceMethod()
        {
            Console.WriteLine("A.InterfaceMethod()");
        }

        private int interfaceField;
        public int InterfaceField
        {
            get
            {
                return interfaceField;
            }
            set
            {
                interfaceField = value;
            }
        }
    }

    public class B : AbstractClass, InterfaceA, InterfaceB
    {
        /// <summary>
        /// interface field必须要实现
        /// </summary>
        private int interfaceField;
        public int InterfaceField
        {
            get
            {
                return interfaceField;
            }
            set
            {
                interfaceField = value;
            }
        }

        /// <summary>
        /// interface method必须要实现
        /// </summary>
        public void InterfaceMethod()
        {
            Console.WriteLine("B.InterfaceMethod()");
        }

        /// <summary>
        /// interface method必须要实现
        /// </summary>
        public void CallEvent()
        {
            if (OnChange != null)
            {
                Console.WriteLine("B.AnotherInterfaceMethod()=>");
                OnChange();
            }
        }

        /// <summary>
        /// interface Event必须要包含在本类中
        /// </summary>
        public event Action OnChange;

        /// <summary>
        /// abstract field必须要实现
        /// </summary>
        private int abstractClassAbstractField;
        public override int AbstractField
        {
            get
            {
                return abstractClassAbstractField;
            }
            set
            {
                abstractClassAbstractField = value;
            }
        }

        /// <summary>
        /// abstract method必须要实现
        /// </summary>
        public override void AbstractMethod()
        {
            Console.WriteLine("B.AbstractMethod()");
        }

        /// <summary>
        /// 父类中的virtual method可以选择是否重写
        /// </summary>
        public override void VirtualMethod()
        {
            Console.WriteLine("B.VirtualMethod()");
        }

        /// <summary>
        /// 父类中的virtual field可以选择是否重写
        /// </summary>
        private int virtualField;
        public override int VirtualField
        {
            get
            {
                return virtualField;
            }
            set
            {
                virtualField = value;
            }
        }
    }

    public interface InterfaceA
    {
        /// <summary>
        /// 接口中定义字段需要{ get; set; }
        /// </summary>
        int InterfaceField { get; set; }

        /// <summary>
        /// 接口中定义方法，不可以有实现
        /// </summary>
        void InterfaceMethod();
    }

    public interface InterfaceB
    {
        event Action OnChange;

        /// <summary>
        /// 接口中定义方法，不可以有实现
        /// </summary>
        void CallEvent();
    }

    public abstract class AbstractClass
    {
        // 注意：abstract类中所有属性和方法，定义为public才在类外部访问, 定义为protected只可在继承类中访问. 
        //      意思是在接口或者抽象类中需要暴露出来的方法定义为public, 不想暴露出来的定义为protected, 只想在本类中被访问的定义为private.
        /// <summary>
        /// normal field 可以有，也可以没有{ get; set; }, 
        /// </summary>
        public int NormalField;

        /// <summary>
        /// virtual field 需要{ get; set; }
        /// </summary>
        public virtual int VirtualField { get; set; }

        /// <summary>
        /// abstract field 需要{ get; set; }
        /// </summary>
        public abstract int AbstractField { get; set; }

        /// <summary>
        /// 普通方法必须有实现
        /// </summary>
        public void NormalMethod()
        {
            Console.WriteLine("AbstractClass.NormalMethod().");
        }

        /// <summary>
        /// virtual方法必须有实现
        /// </summary>
        public virtual void VirtualMethod()
        {
            Console.WriteLine("AbstractClass.VirtualMethod().");
        }

        /// <summary>
        /// abstract方法不可以有实现
        /// </summary>
        public abstract void AbstractMethod();
    }
}
