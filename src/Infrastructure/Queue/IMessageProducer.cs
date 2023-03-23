namespace Infrastructure.Queue;

public interface IMessageProducer
{
    void SendMessage<T>(T message);
}