using System;

public enum EventType
{
    Default = 0,
    ValueChange = 1,
    OnClick = 2,
}
public class GlobalEvent
{
    private static OneSender oneSender = new OneSender();
    public static void AddListener(Enum eventType, Action<object> eventHandler)
    {
        oneSender.AddListener(eventType, eventHandler);
    }
    public static void RemoveListener(Enum eventType, Action<object> eventHandler)
    {
        oneSender.RemoveListener(eventType, eventHandler);
    }
    public static void SendMessage(Enum eventType, object pObj)
    {
        oneSender.SendMessage(eventType, pObj);
    }
}
public class StringKeySender
{
    private EventSender<String, object> sender = new EventSender<String, object>();
    public void Clear()
    {
        sender.Clear();
        sender = null;
    }
    public void AddListener(String eventType, Action<object> eventHandler)
    {
        sender.AddListener(eventType, eventHandler);
    }
    public void RemoveListener(String eventType, Action<object> eventHandler)
    {
        sender.RemoveListener(eventType, eventHandler);
    }
    public void SendMessage(String eventType, object pObj)
    {
        sender.SendMessage(eventType, pObj);
    }
}
public class OneSender
{
    private EventSender<Enum, object> sender = new EventSender<Enum, object>();
    public void Clear()
    {
        sender.Clear();
        sender = null;
    }
    public void AddListener(Enum eventType, Action<object> eventHandler)
    {
        sender.AddListener(eventType, eventHandler);
    }
    public void RemoveListener(Enum eventType, Action<object> eventHandler)
    {
        sender.RemoveListener(eventType, eventHandler);
    }
    public void SendMessage(Enum eventType, object pObj)
    {
        sender.SendMessage(eventType, pObj);
    }
}