using GymManagement.Domain.Subscriptions;
using Throw;

namespace GymManagement.Domain.Admins;

public class Admin
{
    public Guid UserId { get; }
    public Guid? SubscriptionId { get; private set; } = null;
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public Admin(
        Guid userId,
        Guid? subscriptionId = null,
        Guid? id = null,
        string name = "")
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
        Id = id ?? Guid.NewGuid();
        Name = name;
    }

    private Admin() { }

    public void SetSubscription(Subscription subscription)
    {
        SubscriptionId.HasValue.Throw().IfTrue();

        SubscriptionId = subscription.Id;
    }

    public void DeleteSubscription(Guid subscriptionId)
    {
        SubscriptionId.ThrowIfNull().IfNotEquals(subscriptionId);

        SubscriptionId = null;
    }
}