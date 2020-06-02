using System;

namespace EventsProject
{
    // An interface can declare an event.
    
    // The following example shows how to handle the less-common situation in which your
    // class inherits from two or more interfaces and each interface has an event with the same name.
    // In this situation, you must provide an explicit interface implementation for at least one of the events.
    // When you write an explicit interface implementation for an event, you must also write the add and remove event accessors.
    // Normally these are provided by the compiler, but in this case the compiler cannot provide them.

    // By providing your own accessors, you can specify whether the two events are represented by the same event in your class,
    // or by different events. For example, if the events should be raised at different times according to the interface specifications,
    // you can associate each event with a separate implementation in your class. In the following example, subscribers
    // determine which OnDraw event they will receive by casting the shape reference to either an IShape or an IDrawingObject.

    public interface IDrawingObject
    {
        // Raise this event before drawing the object.
        event EventHandler OnDraw;
    }
    public interface IShape
    {
        // Raise this event after drawing the shape.
        event EventHandler OnDraw;
    }

    // Base class event publisher inherits two interfaces, each with an OnDraw event
    public class ShapePublisher : IDrawingObject, IShape
    {
        // Create an event for each interface event
        event EventHandler PreDrawEvent;
        event EventHandler PostDrawEvent;

        object objectLock = new Object();

        // An event is a special kind of multicast delegate that can only be invoked from within the class that it is declared in.
        // Client code subscribes to the event by providing a reference to a method that should be invoked when the event is fired.
        // These methods are added to the delegate's invocation list through event accessors, which resemble property accessors,
        // except that event accessors are named add and remove. In most cases, you do not have to supply custom event accessors.
        // When no custom event accessors are supplied in your code, the compiler will add them automatically.
        // However, in some cases you may have to provide custom behavior.
        
        // Explicit interface implementation required.
        // Associate IDrawingObject's event with PreDrawEvent
        #region IDrawingObjectOnDraw
        event EventHandler IDrawingObject.OnDraw
        {
            add
            {
                lock (objectLock)
                {
                    PreDrawEvent += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    PreDrawEvent -= value;
                }
            }
        }
        #endregion
        // Explicit interface implementation required.
        // Associate IShape's event with PostDrawEvent
        event EventHandler IShape.OnDraw
        {
            add
            {
                lock (objectLock)
                {
                    PostDrawEvent += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    PostDrawEvent -= value;
                }
            }
        }

        // For the sake of simplicity this one method implements both interfaces.
        public void Draw()
        {
            // Raise IDrawingObject's event before the object is drawn.
            PreDrawEvent?.Invoke(this, EventArgs.Empty);

            Console.WriteLine("Drawing a shape.");

            // Raise IShape's event after the object is drawn.
            PostDrawEvent?.Invoke(this, EventArgs.Empty);
        }
    }
    public class Subscriber1
    {
        // References the shape object as an IDrawingObject
        public Subscriber1(ShapePublisher shape)
        {
            IDrawingObject d = (IDrawingObject)shape;
            d.OnDraw += d_OnDraw;
        }

        void d_OnDraw(object sender, EventArgs e)
        {
            Console.WriteLine("Sub1 receives the IDrawingObject event.");
        }
    }
    // References the shape object as an IShape
    public class Subscriber2
    {
        public Subscriber2(ShapePublisher shape)
        {
            IShape d = (IShape)shape;
            d.OnDraw += d_OnDraw;
        }

        void d_OnDraw(object sender, EventArgs e)
        {
            Console.WriteLine("Sub2 receives the IShape event.");
        }
    }

    public class InterfaceEvents
    {
        public static void Run()
        {
            ShapePublisher shape = new ShapePublisher();
            Subscriber1 sub = new Subscriber1(shape);
            Subscriber2 sub2 = new Subscriber2(shape);
            shape.Draw();
        }
    }
}