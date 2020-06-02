using System;

namespace EventsProject
{
    // Events enable a class or object to notify other classes or objects when something of interest occurs. The class that
    // sends (or raises) the event is called the publisher and the classes that receive (or handle) the event are called subscribers.
    
    // The publisher determines when an event is raised; the subscribers determine what action is taken in response to the event.

    // An event can have multiple subscribers. A subscriber can handle multiple events from multiple publishers.

    // Events that have no subscribers are never raised.

    // Events are typically used to signal user actions such as button clicks or menu selections in graphical user interfaces.

    // When an event has multiple subscribers, the event handlers are invoked synchronously when an event is raised.
    // To invoke events asynchronously, see Calling Synchronous Methods Asynchronously.

    // In the .NET Framework class library, events are based on the EventHandler delegate and the EventArgs base class.
    // public delegate void EventHandler(object sender, EventArgs e);
    
    // .NET Framework 2.0 introduces a generic version of this delegate, EventHandler<TEventArgs>.
    
    // Although events in classes that you define can be based on any valid delegate type, even delegates that return a
    // value, it is generally recommended that you base your events on the .NET pattern by using EventHandler.
    
    // The name EventHandler can lead to a bit of confusion as it doesn't actually handle the event. The EventHandler,
    // and generic EventHandler<TEventArgs> are delegate types. A method or lambda expression whose signature matches
    // the delegate definition is the event handler and will be invoked when the event is raised.
    
    // 1. (Skip this step and go to Step 3a if you do not have to send custom data with your event.)
    // Declare the class for your custom data at a scope that is visible to both your publisher and subscriber classes.
    // Then add the required members to hold your custom event data.
    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(string message) => Message = message;
        public string Message { get; set; }
    }
    
    // 2. (Skip this step if you are using the generic version of EventHandler<TEventArgs>.)
    // Declare a delegate in your publishing class. Give it a name that ends with EventHandler.
    // The second parameter specifies your custom EventArgs type.
    public delegate void AnotherCustomEventHandler(object sender, CustomEventArgs args);


    // Class that publishes an event
    class Publisher
    {
        // If you have no custom EventArgs class, your Event type will be the non-generic EventHandler delegate.
        // You do not have to declare the delegate because it is already declared in the System namespace that is included
        // when you create your C# project. Add the following code to your publisher class.
        public event EventHandler RaiseDefaultEvent;
        
        // If you are using the non-generic version of EventHandler and you have a custom class derived from EventArgs,
        // declare your event inside your publishing class and use your delegate from step 2 as the type.
        public event AnotherCustomEventHandler RaiseAnotherCustomEvent;

        // If you are using the generic version, you do not need a custom delegate. Instead, you specify your event type
        // as EventHandler<CustomEventArgs>, substituting the name of your own class between the angle brackets.
        public event EventHandler<CustomEventArgs> RaiseGenericCustomEvent;

        public void DoSomething()
        {
            // Write some code that does something useful here then raise the event.
            // You can also raise an event before you execute a block of code.
            OnRaiseCustomEvent(new CustomEventArgs("Event triggered"));
        }

        // Wrap event invocations inside a protected virtual method
        // to allow derived classes to override the event invocation behavior
        protected virtual void OnRaiseCustomEvent(CustomEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of a race condition if the last
            // subscriber unsubscribes immediately after the null check and before the event is raised.
            EventHandler<CustomEventArgs> raiseEvent = RaiseGenericCustomEvent;

            // Event will be null if there are no subscribers
            if (raiseEvent != null)
            {
                // Format the string to send inside the CustomEventArgs parameter
                e.Message += $" at {DateTime.Now}";
                // Call to raise the event.
                raiseEvent(this, e);
            }
        }
    }

    //Class that subscribes to an event
    class Subscriber
    {
        private readonly string _id;

        public Subscriber(string id, Publisher pub)
        {
            _id = id;
            // Subscribe to the event
            pub.RaiseGenericCustomEvent += HandleGenericCustomEvent;
        }

        // Define what actions to take when the event is raised.
        void HandleGenericCustomEvent(object sender, CustomEventArgs e)
        {
            Console.WriteLine($"{_id} received this message: {e.Message}");
        }
    }

    class Program
    {
        static void Main()
        {
            var pub = new Publisher();
            var sub1 = new Subscriber("sub1", pub);
            var sub2 = new Subscriber("sub2", pub);

            // Call the method that raises the event.
            pub.DoSomething();

            Console.WriteLine();
            RaiseBaseClassEvents.Run();

            Console.WriteLine();
            InterfaceEvents.Run();
        }
    }
}